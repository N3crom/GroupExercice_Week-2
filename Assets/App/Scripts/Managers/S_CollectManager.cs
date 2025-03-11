using UnityEngine;

public class S_CollectManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_Collecte rseCollecte;

    [Header("Output")]
    [SerializeField] private RSO_Coins rsoCoins;

    private void OnEnable()
    {
        rseCollecte.action += AddCoins;
    }

    private void OnDisable()
    {
        rseCollecte.action -= AddCoins;
    }

    private void Start()
    {
        rsoCoins.Value = 0;
    }

    private void AddCoins(int quantity)
    {
        rsoCoins.Value += quantity;
    }
}