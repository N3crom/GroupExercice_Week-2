using System.Collections;
using TMPro;
using UnityEngine;

public class S_GameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textCoins;

    [Header("Input")]
    [SerializeField] private RSE_UpdateUICoins rseUpdateUICoins;

    [Header("Output")]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_TotalCoins rsoTotalCoins;

    private void OnEnable()
    {
        rseUpdateUICoins.action += UpdateUI;
    }

    private void OnDisable()
    {
        rseUpdateUICoins.action -= UpdateUI;
    }

    private IEnumerator LateStart()
    {
        yield return null;

        UpdateUI();
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    private void UpdateUI()
    {
        textCoins.text = $"{rsoCoins.Value} / {rsoTotalCoins.Value}";
    }
}