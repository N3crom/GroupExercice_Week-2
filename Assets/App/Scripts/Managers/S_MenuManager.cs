using UnityEngine;

public class S_MenuManager : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField,SceneName] private string sceneGame;
    
    [Header("Input")]
    [SerializeField]private RSE_QuitGame rseQuitGame;
    [SerializeField]private RSE_StartGame rseStartGame;
    
    private void OnEnable()
    {
        rseQuitGame.action += Quit;
        rseStartGame.action += StartGame;
    }
    
    private void OnDisable()
    {
        rseQuitGame.action -= Quit;
        rseStartGame.action -= StartGame;
    }

    private void StartGame() => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneGame);

    private void Quit() => Application.Quit();
    
}
