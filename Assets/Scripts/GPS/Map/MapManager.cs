using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

using UnproductiveProductions.StadsBingo.Global;

namespace UnproductiveProductions.StadsBingo.GPS.Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("Map Settings")]
        [SerializeField] private GameObject mapContainer;
        [SerializeField] private GameObject tilePrefab;

        [field: Space]
        [Tooltip("Zoom Level: 1-20, higher values = more detail.")]
        [field: SerializeField, Range(1, 19)] public int Zoom { get; private set; } = 16;

        [Tooltip("Amount of tiles around the player (3x3 grid if this is set to 1).")]
        [SerializeField] private int tilesPerSide = 3;

        [Space]
        [SerializeField] private string tileServerURL = "https://tile.openstreetmap.org/{0}/{1}/{2}.png";

        [Header("References")]
        [SerializeField] private Transform playerMarker;

        private Dictionary<string, GameObject> _activeTiles = new Dictionary<string, GameObject>();
        private float _tileSize = 256f;  // Standard size of OSM tiles is 256x256 pixels

        public int CurrentCenterX { get; private set; }
        public int CurrentCenterY { get; private set; }

        #region Singleton

        public static MapManager Singleton { get; private set; }

        private void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Debug.LogWarning($"[{GetType().Name}] - Another Singleton instance found, destroying other Singleton...");
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log($"[{GetType().Name}] - Created a Singleton successfully.");
        }

        #endregion

        private void OnEnable()
        {
            EventSystem.Singleton.OnLocationUpdated += UpdateMap;
        }

        private void OnDisable()
        {
            EventSystem.Singleton.OnLocationUpdated -= UpdateMap;
        }

        private void UpdateMap(double latitude, double longitude)
        {
            // calculate tile coordinates based on lat/lon
            int tileX = LongitudeToTileX(longitude, Zoom);
            int tileY = LatitudeToTileY(latitude, Zoom);

            // if we have changed tile
            if (tileX != CurrentCenterX || tileY != CurrentCenterY)
            {
                CurrentCenterX = tileX;
                CurrentCenterY = tileY;

                // update tiles
                LoadTilesAroundCenter();
            }

            // update player marker position
            UpdatePlayerPosition(latitude, longitude);
        }

        private void LoadTilesAroundCenter()
        {
            // keep track of which tiles we still need
            HashSet<string> neededTiles = new HashSet<string>();

            // loop through tiles around center
            for (int x = -tilesPerSide; x <= tilesPerSide; x++)
            {
                for (int y = -tilesPerSide; y <= tilesPerSide; y++)
                {
                    int tileX = CurrentCenterX + x;
                    int tileY = CurrentCenterY + y;
                    string tileKey = $"{Zoom}:{tileX}:{tileY}";

                    neededTiles.Add(tileKey);

                    // if tile isn't loaded yet, load this
                    if (!_activeTiles.ContainsKey(tileKey))
                        StartCoroutine(LoadTile(Zoom, tileX, tileY));
                }
            }

            // delete tiles we don't further need
            List<string> tilesToRemove = new List<string>();
            foreach (var tile in _activeTiles)
                if (!neededTiles.Contains(tile.Key))
                    tilesToRemove.Add(tile.Key);

            foreach(var tileKey in tilesToRemove)
            {
                Destroy(_activeTiles[tileKey]);
                _activeTiles.Remove(tileKey);
            }
        }

        private IEnumerator LoadTile(int zoom, int x, int y)
        {
            string tileKey = $"{zoom}:{x}:{y}";
            string url = string.Format(tileServerURL, zoom, x, y);

            using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError($"[{GetType().Name}] - Error loading tile: {www.error}");
            else
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(www);
                GameObject tile = CreateTile(tex, x, y);
                _activeTiles[tileKey] = tile;
            }
        }

        private GameObject CreateTile(Texture2D tileTex, int x, int y)
        {
            GameObject tile = Instantiate(tilePrefab, mapContainer.transform);

            // caclulate position relative to the center
            float posX = (x - CurrentCenterX) * _tileSize;
            float posY = -1 * (y - CurrentCenterY) * _tileSize;  // y-axis is inverted in unity to OSM

            tile.transform.localPosition = new Vector3(posX, posY, 0);

            // set texture
            Renderer renderer = tile.GetComponent<Renderer>();
            if (renderer)
                renderer.material.mainTexture = tileTex;
            else
            {
                // alternatively, look for a rawimage or sprite
                var rawImage = tile.GetComponent<UnityEngine.UI.RawImage>();
                if (rawImage)
                    rawImage.texture = tileTex;
                else
                {
                    var spriteRenderer = tile.GetComponent<SpriteRenderer>();
                    if (spriteRenderer)
                    {
                        Sprite sprite = Sprite.Create(tileTex, new Rect(0, 0, tileTex.width, tileTex.height), new Vector2(0.5f, 0.5f));
                        spriteRenderer.sprite = sprite;
                    }
                }
            }

            return tile;
        }

        private void UpdatePlayerPosition(double latitude, double longitude)
        {
            if (!playerMarker) return;

            // calculate local position on map
            float tileX = (float)((LongitudeToTileX(longitude, Zoom) - CurrentCenterX) * _tileSize);
            float tileY = (float)((LatitudeToTileY(latitude, Zoom) - CurrentCenterY) * _tileSize);

            // calculate fraction within this tile
            float fractionalX = (float)(LongitudeToTileXFloat(longitude, Zoom) - Math.Floor(LongitudeToTileXFloat(longitude, Zoom)));
            float fractionalY = (float)(LatitudeToTileYFloat(latitude, Zoom) - Math.Floor(LatitudeToTileYFloat(latitude, Zoom)));

            tileX += fractionalX * _tileSize;
            tileY += fractionalY * _tileSize;

            playerMarker.localPosition = new Vector3(tileX, tileY, -1);
            Debug.Log($"[{GetType().Name}] - Player Marker position: {playerMarker.localPosition}");
        }

        #region Utility Methods

        private int LongitudeToTileX(double longitude, int zoom)
        {
            return (int)Math.Floor((longitude + 180.0) / 360 * (1 << Zoom));
        }

        private int LatitudeToTileY(double latitude, int zoom)
        {
            return (int)Math.Floor((1 - Math.Log(Math.Tan(latitude * Math.PI / 180.0) + 1 / Math.Cos(latitude * Math.PI / 180.0)) / Math.PI) / 2 * (1 << zoom));
        }

        private double LongitudeToTileXFloat(double longitude, int zoom)
        {
            return (longitude + 180.0) / 360.0 * (1 << zoom);
        }

        private double LatitudeToTileYFloat(double latitude, int zoom)
        {
            return (1 - Math.Log(Math.Tan(latitude * Math.PI / 180.0) + 1 / Math.Cos(latitude * Math.PI / 180.0)) / Math.PI) / 2 * (1 << zoom);
        }

        #endregion
    }
}
