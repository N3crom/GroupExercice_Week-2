using UnityEngine;

public class S_CollectManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_Collecte rseCollecte;

    [Header("Output")]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSE_UpdateUICoins rseUpdateUICoins;

    private void OnEnable()
    {
        rseCollecte.action += AddCoins;
    }

    private void OnDisable()
    {
        rseCollecte.action -= AddCoins;
    }

    private void AddCoins(int quantity)
    {
        rsoCoins.Value += quantity;
        rseUpdateUICoins.Call();
    }
}