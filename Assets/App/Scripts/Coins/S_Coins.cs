using UnityEngine;

public class S_Coins : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, TagName] private string tagPlayer;
    [SerializeField] private int quantity;

    //[Header("References")]

    //[Header("Input")]

    [Header("Output")]
    [SerializeField] private RSE_Collecte rseCollecte;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagPlayer))
        {
            rseCollecte.Call(quantity);
            Destroy(gameObject);
        }
    }
}