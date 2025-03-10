using UnityEngine;

public class S_PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_GetCellPos rseGetCellPos;
    [SerializeField] private RSO_CellPos rsoCellPos;

    [Header("Output")]
    [SerializeField] private RSE_PlayerMove rsePlayerMove;

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

        if(transform.position != rsoCellPos.Value)
        {
            transform.position = new Vector3(rsoCellPos.Value.x, transform.position.y, rsoCellPos.Value.z);
            rsePlayerMove.Call();
        }
        
    }
}