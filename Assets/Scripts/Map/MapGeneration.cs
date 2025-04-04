using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MapGeneration : MonoBehaviour
{
    float tileWidth;
    float tileHeight;

    [Header("References")]
    [SerializeField] public GameObject tileParent;
    [SerializeField] public GameObject tilePrefab;
    [SerializeField] public MapPlayerBehavior mapPlayerBehavior;

    [Header("Margins")]
    [SerializeField] public float xMargin = .1f;
    [SerializeField] public float yMargin = .1f;


    void OnEnable()
    {
        tileWidth = (1-(xMargin*2)) / GameManager.Instance.MapManager.map.GetLength(1);
        tileHeight = (1-(yMargin*2)) / GameManager.Instance.MapManager.map.GetLength(0);

        GameManager.Instance.MapManager.tiles = new GameObject[GameManager.Instance.MapManager.mapHeight, GameManager.Instance.MapManager.mapWidth];

        GenerateMap();
    }

    void GenerateMap()
    {
        for (int i = 0; i < GameManager.Instance.MapManager.mapHeight; i++)
        {
            for (int j = 0; j < GameManager.Instance.MapManager.mapWidth; j++)
            {
                if (GameManager.Instance.MapManager.map[i, j] != MapManager.Tiles.Wall)
                {
                    float xStart = xMargin + (j * tileWidth);
                    float yStart = yMargin + (i * tileHeight);

                    GameObject tileInstance = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    tileInstance.transform.SetParent(tileParent.transform);
                    tileInstance.GetComponent<RectTransform>().anchorMin = new Vector2(xStart, yStart);
                    tileInstance.GetComponent<RectTransform>().anchorMax = new Vector2(xStart + tileWidth, yStart + tileHeight);
                    tileInstance.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    tileInstance.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                    tileInstance.name = i + ", " + j;
                    GameManager.Instance.MapManager.tiles[i, j] = tileInstance;

                    MapHelpers.ChangeColor(GameManager.Instance.MapManager.map, GameManager.Instance.MapManager.tiles, new Vector2(i, j));
                }
            }
        }
        Debug.Log("about to call start map");
        mapPlayerBehavior.StartMap();
    }

    
    

}
