using System.Collections;
using UnityEngine;

public class S_GameTimerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _gameTimeToFinish;

    //[Header("References")]

    //[Header("Input")]

    [Header("Output")]
    [SerializeField] private RSE_OnGameTimerChange _rseOnGameTimerChange;
    [SerializeField] private RSE_Death _rseDeath;

    private void Start()
    {
        StartCoroutine(GameTimerCoroutine());
    }

    IEnumerator GameTimerCoroutine()
    {

        float elapsedTime = 0f;

        while (elapsedTime < _gameTimeToFinish)
        {
            elapsedTime += Time.deltaTime;
            _rseOnGameTimerChange.Call(_gameTimeToFinish - elapsedTime);
            yield return null;
        }

        _rseDeath.Call();
    }

}