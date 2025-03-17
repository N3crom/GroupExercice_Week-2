using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class S_TileManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float cellSize;

    [Header("References")]
    [SerializeField] private Transform tilemapGround;
    [SerializeField] private Transform tilemapWall;

    [Header("Input")]
    [SerializeField] private RSE_GetTypeTileLocked rseGetTypeTileLocked;
    [SerializeField] private RSE_GetCellPos rseGetCellPos;

    [Header("Output")]
    [SerializeField] private RSO_CellPos rsoCellPos;
    
    private SerializableDictionary<Vector3, TileData>  tileDataGroundDictionary = new();
    private SerializableDictionary<Vector3, TileData>  tileDataWallDictionary = new();

    private static readonly Vector3[] neighborPosCell =
        { Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.zero, new (1, 0, 1), new (-1, 0, -1), new (1, 0, -1), new (-1, 0, 1) };

    private void OnEnable()
    {
        rseGetCellPos.action += GetCellPosition;
        rseGetTypeTileLocked.action += GetTileType;
    }

    private void OnDisable()
    {
        rseGetCellPos.action -= GetCellPosition;
        rseGetTypeTileLocked.action -= GetTileType;
    }

    private void Awake()
    {
        PopulateDictionary(tilemapGround, tileDataGroundDictionary);
        PopulateDictionary(tilemapWall, tileDataWallDictionary);
    }
    
    private void PopulateDictionary(Transform tilemap, SerializableDictionary<Vector3, TileData> dictionary)
    {
        foreach (Transform child in tilemap)
        {
            TileData tileData = new TileData
            {
                tile = child.gameObject,
                cellDiscovered = false
            };
            dictionary.Dictionary[new Vector3(child.position.x, 0,child.position.z)] = tileData;
            child.gameObject.SetActive(false);
        }
    }

    private Vector3 GetNearestCell(Vector3 pos)
    {
        float gridX = Mathf.Round(pos.x / cellSize) * cellSize;
        float gridZ = Mathf.Round(pos.z / cellSize) * cellSize;

        return new Vector3(gridX, 0, gridZ);
    }

    private void GetCellPosition(Vector3 pos, TileType[] tileCanMove)
    {
        Vector3 cellPos = GetNearestCell(pos);
        
        var isGround = tileDataGroundDictionary.Dictionary.Keys.Any(tile => tile == cellPos);
        var isWall = tileDataWallDictionary.Dictionary.Keys.Any(tile => tile == cellPos);

        // Check current tile type if is in tile accessible
        if (isGround && tileCanMove.Contains(TileType.Ground) && !(isWall && !tileCanMove.Contains(TileType.Wall)))
        {
            DiscoverCell(cellPos);
            rsoCellPos.Value = cellPos;
        }
    }
    
    private void DiscoverCell(Vector3 pos)
    {
        foreach (var neighborPos in neighborPosCell)
        {
            Vector3 neighborCell = pos + neighborPos * cellSize;
            
            if (tileDataGroundDictionary.Dictionary.TryGetValue(neighborCell, out var groundTileData) && !groundTileData.cellDiscovered)
            {
                groundTileData.cellDiscovered = true;
                groundTileData.tile.SetActive(true);
            }
            
            if (tileDataWallDictionary.Dictionary.TryGetValue(neighborCell, out var wallTileData) && !wallTileData.cellDiscovered)
            {
                wallTileData.cellDiscovered = true;
                wallTileData.tile.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Get type of tile at x pos and return callback with the type
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="action"></param>
    private void GetTileType(Vector3 pos, Action<TileType> action)
    {
        Vector3 cellPos = GetNearestCell(pos);
        
        var isGround = tileDataGroundDictionary.Dictionary.Keys.Any(tile => tile == cellPos);
        var isWall = tileDataWallDictionary.Dictionary.Keys.Any(tile => tile == cellPos);
        
        if(isWall){ action?.Invoke(TileType.Wall);}
        else if(isGround) action?.Invoke(TileType.Ground);
    }
    
}

[System.Serializable]
public class TileData
{
    public GameObject tile;
    public bool cellDiscovered;
}

public enum TileType
{
    Ground,
    Wall
}