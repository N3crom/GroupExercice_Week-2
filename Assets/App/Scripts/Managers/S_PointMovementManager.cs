using System;
using UnityEngine;

public class S_PointMovementManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxPoints = 10;
    
    [Header("References")]
    [SerializeField] private RSO_PointMovement rsoPointMovement;
    
    [Header("Input")]
    [SerializeField] private RSE_PlayerMove rsePlayerMove;
    
    [Header("Output")]
    [SerializeField] private RSE_Death rseDeath;

    private void Start() => rsoPointMovement.Value = maxPoints;
    
    private void OnEnable() => rsePlayerMove.action += DecreasePoints;
    private void OnDisable() => rsePlayerMove.action -= DecreasePoints;

    private void DecreasePoints()
    {
        rsoPointMovement.Value = Mathf.Clamp(rsoPointMovement.Value - 1,0, maxPoints);
        if (rsoPointMovement.Value <= 0)
        {
            rseDeath.Call();
        }
    }
}
