using System;
using UnityEngine;

public class S_PlayerGhost : MonoBehaviour, IPowerUp
{
    [Header("Settings")]
    [field:SerializeField] public float durationPowerUp { get; set; }

    public event Action onPowerUpDisable;
    
    public static readonly TileType[] TileCanMove = {TileType.Ground,TileType.Wall};
    
    public bool powerUpEnable { get; set; }
    
    private void Awake() => powerUpEnable = false;

    public void EnablePowerUp()
    {
        if (powerUpEnable) return;
        powerUpEnable = true;
        StartCoroutine(Utils.Delay(durationPowerUp, DisablePowerUp));
    }

    public void DisablePowerUp()
    {
        onPowerUpDisable?.Invoke();
        powerUpEnable = false;
    }
}
