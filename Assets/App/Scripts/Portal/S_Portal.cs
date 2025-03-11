using UnityEngine;

public class S_Portal : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, TagName] private string tagPlayer;

    [Header("Output")]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_TotalCoins rsoTotalCoins;
    [SerializeField] private RSE_NeedCoins rseNeedCoins;
    [SerializeField] private RSE_Win rseWin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagPlayer))
        {
            if (rsoCoins.Value >= rsoTotalCoins.Value)
            {
                rseWin.Call();

                Destroy(other.gameObject);
            }
            else
            {
                rseNeedCoins.Call();
            }
        }
    }
}