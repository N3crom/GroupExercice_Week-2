using System;
using UnityEngine;

public interface IPowerUp
{
    public bool powerUpEnable { get;}
    public float durationPowerUp { get;}

    public event Action onPowerUpDisable;
    
    public void EnablePowerUp();
    public void DisablePowerUp();
    
}