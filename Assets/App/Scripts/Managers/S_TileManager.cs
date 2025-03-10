using UnityEngine;

public class S_TileManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform tilemapGround;
    [SerializeField] private Transform tilemapWall;

    [Header("Input")]
    [SerializeField] private RSE_GetCellPos rseGetCellPos;

    [Header("Output")]
    [SerializeField] private RSO_CellPos rsoCellPos;

    private SerializableDictionary<int, Vector3> tileGroundDictionary = new SerializableDictionary<int, Vector3>();
    private SerializableDictionary<int, Vector3> tileWallDictionary = new SerializableDictionary<int, Vector3>();

    private void OnEnable()
    {
        rseGetCellPos.action += GetCellPosition;
    }

    private void OnDisable()
    {
        rseGetCellPos.action -= GetCellPosition;
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        for (int i = 0; i < tilemapGround.childCount; i++)
        {
            Transform child = tilemapGround.GetChild(i);
            tileGroundDictionary.Dictionary.Add(i, child.position);
        }

        for (int i = 0; i < tilemapWall.childCount; i++)
        {
            Transform child = tilemapWall.GetChild(i);
            tileWallDictionary.Dictionary.Add(i, child.position);
        }
    }

    private Vector3 GetNearestCell(Vector3 pos)
    {
        float gridX = Mathf.Round(pos.x / 2) * 2;
        float gridZ = Mathf.Round(pos.z / 2) * 2;

        return new Vector3(gridX, 0, gridZ);
    }

    private void GetCellPosition(Vector3 pos)
    {
        Vector3 cellPos = GetNearestCell(pos);

        if (tileGroundDictionary.Dictionary.ContainsValue(cellPos) && !tileWallDictionary.Dictionary.ContainsValue(cellPos))
        {
            rsoCellPos.Value = cellPos;
        }
    }
}