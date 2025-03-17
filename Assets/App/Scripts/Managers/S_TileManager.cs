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
    [SerializeField] private RSE_GetCellPos rseGetCellPos;

    [Header("Output")]
    [SerializeField] private RSO_CellPos rsoCellPos;
    
    private SerializableDictionary<Vector3, TileData>  m_TileDataGroundDictionary = new();
    private SerializableDictionary<Vector3, TileData>  m_TileDataWallDictionary = new();

    private static readonly Vector3[] m_NeighborPosCell =
        { Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.zero, new (1, 0, 1), new (-1, 0, -1), new (1, 0, -1), new (-1, 0, 1) };

    private void OnEnable()
    {
        rseGetCellPos.action += GetCellPosition;
    }

    private void OnDisable()
    {
        rseGetCellPos.action -= GetCellPosition;
    }

    private void Awake()
    {
        PopulateDictionary(tilemapGround, m_TileDataGroundDictionary);
        PopulateDictionary(tilemapWall, m_TileDataWallDictionary);
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

    private void GetCellPosition(Vector3 pos)
    {
        Vector3 cellPos = GetNearestCell(pos);
        DiscoverCell(cellPos);
        
        var isGround = m_TileDataGroundDictionary.Dictionary.Keys.Any(tile => tile == cellPos);
        var isWall = m_TileDataWallDictionary.Dictionary.Keys.Any(tile => tile == cellPos);

        if (isGround && !isWall)
        {
            rsoCellPos.Value = cellPos;
        }
    }
    
    private void DiscoverCell(Vector3 pos)
    {
        foreach (var neighborPos in m_NeighborPosCell)
        {
            Vector3 neighborCell = pos + neighborPos * cellSize;
            
            if (m_TileDataGroundDictionary.Dictionary.TryGetValue(neighborCell, out var groundTileData) && !groundTileData.cellDiscovered)
            {
                groundTileData.cellDiscovered = true;
                groundTileData.tile.SetActive(true);
            }
            
            if (m_TileDataWallDictionary.Dictionary.TryGetValue(neighborCell, out var wallTileData) && !wallTileData.cellDiscovered)
            {
                wallTileData.cellDiscovered = true;
                wallTileData.tile.SetActive(true);
            }
        }
    }
    
}

[System.Serializable]
public class TileData
{
    public GameObject tile;
    public bool cellDiscovered;
}