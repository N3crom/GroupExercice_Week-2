using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float delayMove;
    [SerializeField] private float sizeCell;

    [Header("Input")]
    [SerializeField] private RSE_GetCellPos rseGetCellPos;
    [SerializeField] private RSO_CellPos rsoCellPos;

    [Header("Output")]
    [SerializeField] private RSE_PlayerMove rsePlayerMove;

    bool isPressing = false;

    private void Start()
    {
        rseGetCellPos.Call(transform.position);
    }

    private void Move(Vector2 direction)
    {
        rseGetCellPos.Call(transform.position + new Vector3(direction.x, 0, direction.y) * sizeCell);

        if(transform.position != rsoCellPos.Value)
        {
            transform.position = new Vector3(rsoCellPos.Value.x, transform.position.y, rsoCellPos.Value.z);
            rsePlayerMove.Call();
        }
    }

    private IEnumerator PlayerMove(InputAction.CallbackContext ctx)
    {
        while(isPressing)
        {
            Move(ctx.ReadValue<Vector2>());

            yield return new WaitForSeconds(delayMove);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            isPressing = true;

            StartCoroutine(PlayerMove(ctx));
        }
        else if(ctx.canceled)
        {
            isPressing = false;

            StopAllCoroutines();
        }
    }
}