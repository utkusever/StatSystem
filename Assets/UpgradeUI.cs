using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Action<Stat> OnPowerUpgradeRequest;
    public Action OnAllUpgradeRequest;
    //[SerializeField] private Button powerUpgradeButton;
    [SerializeField] private Button generalUpgradeButton;
    [SerializeField] private TMP_Text generalCost;
    [SerializeField] private TMP_Text powerCurrentStatText;
    [SerializeField] private TMP_Text powerNextStatText;
    [SerializeField] private TMP_Text fireRateCurrentStatText;
    [SerializeField] private TMP_Text fireRateNextStatText;
    [SerializeField] private TMP_Text fireRangeCurrentStatText;
    [SerializeField] private TMP_Text fireRangeNextStatText;
    [SerializeField] private Canvas myCanvasToOpenClose;

    private void Start()
    {
        //powerUpgradeButton.onClick.AddListener(RequestPowerUpgrade);
        generalUpgradeButton.onClick.AddListener(OnAllUpgrade);
    }


    public void SetPower(string currentLevelStat)
    {
        powerCurrentStatText.text = currentLevelStat;
        powerNextStatText.text = "MAX";
    }

    public void SetPower(string cost, string currentLevelStat, string nextLevelStat)
    {
        generalCost.text = cost;
        powerCurrentStatText.text = currentLevelStat;
        powerNextStatText.text = nextLevelStat;
    }

    public void SetFireRange(string currentLevelStat)
    {
        fireRangeCurrentStatText.text = currentLevelStat;
        fireRangeNextStatText.text = "MAX";
    }

    public void SetFireRange(string cost, string currentLevelStat, string nextLevelStat)
    {
        fireRangeCurrentStatText.text = currentLevelStat;
        fireRangeNextStatText.text = nextLevelStat;
    }

    public void SetFireRate(string currentLevelStat)
    {
        fireRateCurrentStatText.text = currentLevelStat;
        fireRateNextStatText.text = "MAX";
    }

    public void SetFireRate(string cost, string currentLevelStat, string nextLevelStat)
    {
        fireRateCurrentStatText.text = currentLevelStat;
        fireRateNextStatText.text = nextLevelStat;
    }


    public void SetUpgradeButtonInteractable(bool isInteractable)
    {
        if (isInteractable != generalUpgradeButton.interactable)
            generalUpgradeButton.interactable = isInteractable;
    }

    public void OpenUI()
    {
        //TODO POP UP ANIM
        myCanvasToOpenClose.enabled = true;
    }

    public void CloseUI()
    {
        myCanvasToOpenClose.enabled = false;
    }

    private void RequestPowerUpgrade()
    {
        OnPowerUpgradeRequest?.Invoke(Stat.Power);
    }

    private void OnAllUpgrade()
    {
        OnAllUpgradeRequest?.Invoke();
    }
}