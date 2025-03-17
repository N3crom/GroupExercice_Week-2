using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerController : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private float moveDelay;
    [SerializeField] private float cellSize;

    [Header("References")] [SerializeField]
    private S_PlayerGhost playerGhost;

    [Header("Input")] 
    [SerializeField] private RSE_Death rseDeath;
    [SerializeField] private RSE_GetCellPos rseGetCellPos;
    [SerializeField] private RSO_CellPos rsoCellPos;

    [Header("Output")] 
    [SerializeField] private RSE_GetTypeTileLocked rseGetTypeTileLocked;
    [SerializeField] private RSE_PlayerMove rsePlayerMove;

    bool isMoving = false;
    bool canMove = true;
    private Coroutine moveCoroutine;
    private Action delegateMoveDeath;

    private static readonly TileType[] tileCanMove = {TileType.Ground};

    private void Awake() => delegateMoveDeath = () => canMove = true;

    private void OnEnable()
    {
        rseDeath.action += delegateMoveDeath;
        playerGhost.onPowerUpDisable +=PowerUpDisable;
    }

    private void OnDisable()
    {
        rseDeath.action -= delegateMoveDeath;
        playerGhost.onPowerUpDisable -= PowerUpDisable;
    }

    private void PowerUpDisable()
    {
        rseGetTypeTileLocked.Call(transform.position, value =>
        {
            if (value == TileType.Wall)
            {
                rseDeath.Call();
            }
        });
    }

    private void Start()
    {
        rseGetCellPos.Call(transform.position, playerGhost.powerUpEnable ? S_PlayerGhost.TileCanMove : tileCanMove);
    }

    private void Move(Vector2 direction)
    {
        Vector3 targetPos = transform.position + new Vector3(direction.x, 0, direction.y) * cellSize;
        rseGetCellPos.Call(targetPos, playerGhost.powerUpEnable ? S_PlayerGhost.TileCanMove : tileCanMove );

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
            if (!canMove) break;
            
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