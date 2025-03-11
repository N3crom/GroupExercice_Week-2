using System.Linq;
using UnityEngine;

public class S_TileManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float sizeCell;

    [Header("References")]
    [SerializeField] private Transform tilemapGround;
    [SerializeField] private Transform tilemapWall;

    [Header("Input")]
    [SerializeField] private RSE_GetCellPos rseGetCellPos;

    [Header("Output")]
    [SerializeField] private RSO_CellPos rsoCellPos;

    private SerializableDictionary<GameObject, Vector3> tileGroundDictionary = new SerializableDictionary<GameObject, Vector3>();
    private SerializableDictionary<GameObject, Vector3> tileWallDictionary = new SerializableDictionary<GameObject, Vector3>();

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
        for (int i = 0; i < tilemapGround.childCount; i++)
        {
            Transform child = tilemapGround.GetChild(i);
            tileGroundDictionary.Dictionary.Add(child.gameObject, child.position);
        }

        for (int i = 0; i < tilemapWall.childCount; i++)
        {
            Transform child = tilemapWall.GetChild(i);
            tileWallDictionary.Dictionary.Add(child.gameObject, child.position);
        }
    }

    private Vector3 GetNearestCell(Vector3 pos)
    {
        float gridX = Mathf.Round(pos.x / sizeCell) * sizeCell;
        float gridZ = Mathf.Round(pos.z / sizeCell) * sizeCell;

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