using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveDelay;
    [SerializeField] private float cellSize;

    [Header("Input")]
    [SerializeField] private RSE_GetCellPos rseGetCellPos;
    [SerializeField] private RSO_CellPos rsoCellPos;

    [Header("Output")]
    [SerializeField] private RSE_PlayerMove rsePlayerMove;

    bool isMoving = false;
    private Coroutine moveCoroutine;

    private void Start()
    {
        rseGetCellPos.Call(transform.position);
    }

    private void Move(Vector2 direction)
    {
        Vector3 targetPos = transform.position + new Vector3(direction.x, 0, direction.y) * cellSize;
        rseGetCellPos.Call(targetPos);

        if (transform.position != rsoCellPos.Value)
        {
            transform.position = new Vector3(rsoCellPos.Value.x, transform.position.y, rsoCellPos.Value.z);
            rsePlayerMove.Call();
        }
    }

    private IEnumerator MoveRoutine(Vector2 direction)
    {
        while (isMoving)
        {
            Move(direction);

            yield return new WaitForSeconds(moveDelay);
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isMoving = true;

            moveCoroutine = StartCoroutine(MoveRoutine(ctx.ReadValue<Vector2>()));
        }
        else if (ctx.canceled && moveCoroutine != null)
        {
            isMoving = false;

            StopCoroutine(moveCoroutine);
        }
    }
}