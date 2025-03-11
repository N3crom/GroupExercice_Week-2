using System.Linq;
using UnityEngine;

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

    private SerializableDictionary<GameObject, Vector3> tileGroundDictionary = new();
    private SerializableDictionary<GameObject, Vector3> tileWallDictionary = new();

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
        PopulateDictionary(tilemapGround, tileGroundDictionary);
        PopulateDictionary(tilemapWall, tileWallDictionary);
    }

    private void PopulateDictionary(Transform tilemap, SerializableDictionary<GameObject, Vector3> dictionary)
    {
        foreach (Transform child in tilemap)
        {
            dictionary.Dictionary[child.gameObject] = child.position;
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

        bool isGround = tileGroundDictionary.Dictionary.Values.Any(tile => Mathf.Approximately(tile.x, cellPos.x) && Mathf.Approximately(tile.z, cellPos.z));
        bool isWall = tileWallDictionary.Dictionary.Values.Any(tile => Mathf.Approximately(tile.x, cellPos.x) && Mathf.Approximately(tile.z, cellPos.z));

        if (isGround && !isWall)
        {
            rsoCellPos.Value = cellPos;
        }
    }
}