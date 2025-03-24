using System.Collections;
using TMPro;
using UnityEngine;

public class S_GameUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeMessageShow;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textPointMovement;
    [SerializeField] private TextMeshProUGUI textCoins;
    [SerializeField] private TextMeshProUGUI textGameTime;
    [SerializeField] private GameObject panelMessage;
    [SerializeField] private TextMeshProUGUI textMessage;

    [Header("Input")]
    [SerializeField] private RSE_UpdateUICoins rseUpdateUICoins;
    [SerializeField] private RSE_NeedCoins rseNeedCoins;
    [SerializeField] private RSE_Win rseWin;
    [SerializeField] private RSE_OnGameTimerChange rseOnGameTimerChange;

    [Header("Output")]
    [SerializeField] private RSO_PointMovement rsoPointMovement;
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_TotalCoins rsoTotalCoins;

    private Coroutine messageCoroutine;

    private void OnEnable()
    {
        rseUpdateUICoins.action += UpdateUI;
        rsoPointMovement.onValueChanged += UpdatePointMovement;
        rseNeedCoins.action += NeedMoreCoins;
        rseWin.action += Win;
        rseOnGameTimerChange.action += UpdateTimer;
    }

    private void OnDisable()
    {
        rseUpdateUICoins.action -= UpdateUI;
        rsoPointMovement.onValueChanged -= UpdatePointMovement;
        rseNeedCoins.action -= NeedMoreCoins;
        rseWin.action -= Win;
        rseOnGameTimerChange.action -= UpdateTimer;
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

    private void UpdateTimer(float time)
    {
        textGameTime.text = $"time left: {time:F1}";
    }

    private IEnumerator MessageShow()
    {
        if(panelMessage.activeInHierarchy)
        {
            panelMessage.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

        panelMessage.SetActive(true);

        yield return new WaitForSeconds(timeMessageShow);

        panelMessage.SetActive(false);
    }

    private void NeedMoreCoins()
    {
        textMessage.color = Color.red;
        textMessage.text = $"You haven't Collected All the Coins!";

        if(messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }

        messageCoroutine = StartCoroutine(MessageShow());
    }

    private void Win()
    {
        textMessage.color = Color.green;
        textMessage.text = $"You Win!";

        panelMessage.SetActive(true);
    }
    
    private void UpdatePointMovement(int value)
    {
        textPointMovement.text = $"{value}";
    }
    
}