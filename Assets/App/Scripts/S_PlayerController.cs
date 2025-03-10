using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class S_PlayerController : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Header("Input")]

    //[Header("Output")]

    [SerializeField] private RSE_GetCellPos rseGetCellPos;
    [SerializeField] private RSO_CellPos rsoCellPos;

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
        rseGetCellPos.Call(transform.position);

        controls.Main.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        rseGetCellPos.Call(transform.position + new Vector3(direction.x, 0, direction.y) * 2);

        transform.position = new Vector3(rsoCellPos.Value.x, transform.position.y, rsoCellPos.Value.z);
    }
}