using UnityEngine;
using UnityEngine.Tilemaps;

public class S_PlayerController : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Header("Input")]

    //[Header("Output")]

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;

    private I_Inputs controls;

    private void Awake()
    {
        controls = new I_Inputs();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Main.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        Debug.Log(GetCellPosition());
    }

    private Vector3Int GetCellPosition()
    {
        Vector3 worldPosition = transform.position;
        Vector3Int cellConversion = groundTilemap.WorldToCell(worldPosition);
        Vector3Int cellPosition = new Vector3Int(cellConversion.x, 0, cellConversion.y);

        return cellPosition;
    }

    private void Move(Vector2 direction)
    {
        Debug.Log(GetCellPosition());
        Vector3 newPosition = new Vector3(direction.x, 0f, direction.y) * 2;

        if (CanMove(newPosition))
        {
            transform.position += newPosition;
            Debug.Log(GetCellPosition());
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int directionConvert = new Vector3Int(Mathf.FloorToInt(direction.x), 0, Mathf.FloorToInt(direction.z));
        Vector3Int nextCellPosition = GetCellPosition() + directionConvert;
        Vector3 worldCellPosition = new Vector3(nextCellPosition.x, transform.position.y, nextCellPosition.z);

        RaycastHit hit;
        if (!Physics.Raycast(worldCellPosition, Vector3.down, out hit, 3f))
        {
            Debug.Log("Hit nothing");
            return false;
        }

        return true;
    }
}