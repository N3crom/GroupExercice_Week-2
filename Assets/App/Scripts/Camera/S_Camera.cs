using UnityEngine;

public class S_Camera : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_PlayerMove rsePlayerMove;

    [Header("Output")]
    [SerializeField] private RSO_CellPos rsoCellPos;

    private Vector3 pos = Vector3.zero;

    private void OnEnable()
    {
        rsePlayerMove.action += UpdateCamera;
    }

    private void OnDisable()
    {
        rsePlayerMove.action -= UpdateCamera;
    }

    private void Start()
    {
        pos = transform.position;
    }

    private void UpdateCamera()
    {
        transform.position = pos + rsoCellPos.Value;
    }
}