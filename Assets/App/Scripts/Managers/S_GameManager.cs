using System;
using System.Collections;
using UnityEngine;

public class S_GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, SceneName] private string nextLevelScene;
    [SerializeField, SceneName] private string menuSceneOnDeath;
    [SerializeField] private float delayBeforeSwapLevel;
    
    [Header("Input")]
    [SerializeField] private RSE_Death rseDeath;
    [SerializeField] private RSE_Win rseWin;
    
    [Header("Output")]
    [SerializeField] private RSE_GetTypeTileLocked rseGetTypeTileLocked;
    
    private Action m_DelegateSwapLevel;
    private Action m_DelegateSwapLevelDeath;

    private void Awake()
    {
        m_DelegateSwapLevel = () => StartCoroutine(SwapLevel(nextLevelScene));
        m_DelegateSwapLevelDeath = () => StartCoroutine(SwapLevel(menuSceneOnDeath/*UnityEngine.SceneManagement.SceneManager.GetActiveScene().name*/));
    }

    private void OnEnable()
    {
        rseWin.action += m_DelegateSwapLevel;
        rseDeath.action += m_DelegateSwapLevelDeath;
    }
    
    private void OnDisable()
    {
        rseWin.action -= m_DelegateSwapLevel;
        rseDeath.action -= m_DelegateSwapLevelDeath;
    }
    
    private IEnumerator SwapLevel(string sceneName)
    {
        yield return new WaitForSeconds(delayBeforeSwapLevel);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
