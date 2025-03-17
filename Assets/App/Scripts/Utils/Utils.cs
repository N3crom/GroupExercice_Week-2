using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utils
{
    
    #region IENUMERABLE
    public static T GetRandom<T>(this IEnumerable<T> elems)
    {
        if (elems.Count() == 0)
        {
            Debug.LogError("Try to get random elem from empty IEnumerable");
        }
        return elems.ElementAt(new System.Random().Next(0, elems.Count()));
    }
    #endregion

    #region COROUTINE
    public static IEnumerator Delay(float delay, Action ev)
    {
        yield return new WaitForSeconds(delay);
        ev?.Invoke();
    }

    public static IEnumerator Delay(YieldInstruction yieldInstruction, Action ev)
    {
        yield return yieldInstruction;
        ev?.Invoke();
    }
    
    public static IEnumerator Delay(IEnumerator coroutine, Action ev)
    {
        yield return coroutine;
        ev?.Invoke();
    }
    
    #endregion
}