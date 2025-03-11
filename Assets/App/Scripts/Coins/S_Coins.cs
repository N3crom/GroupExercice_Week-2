using UnityEngine;

public class S_Coins : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, TagName] private string tagPlayer;
    [SerializeField] private int quantity;

    [Header("Output")]
    [SerializeField] private RSE_Collecte rseCollecte;
    [SerializeField] private RSO_TotalCoins rsoTotalCoins;

    private void Start()
    {
        rsoTotalCoins.Value += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagPlayer))
        {
            rseCollecte.Call(quantity);
            Destroy(gameObject);
        }
    }
}