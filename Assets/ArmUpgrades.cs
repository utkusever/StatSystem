using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmUpgrades : MonoBehaviour
{
    private Action OnUpgradeAction;

    [SerializeField] private UpgradeUI armUpgradesUI;
    [SerializeField] private Stats playerStats;
    [SerializeField] private Inventory playerInventory;


    //private Canvas canvas;


    // public void Initialize(PlayerUpgradesUI playerUpgradesUI)
    // {
    //     this.playerUpgradesUI = playerUpgradesUI;
    //     //canvas = this.playerUpgradesUI.GetComponent<Canvas>();
    //     DisableCanvas();
    // }
    private void Start()
    {
        CloseUpgradeUI();
    }

    public void OpenUpgradeUI()
    {
        //get player money !!
        SubscribeToOnUpgradeAction();
        SubscribeToButtonActions();
        SetUpgradesOnUI();
        EnableUpgradeCanvas();
    }

    private void CloseUpgradeUI()
    {
        UnSubscribeToOnUpgradeAction();
        UnSubscribeToButtonActions();
        DisableUpgradeCanvas();
    }

    private void SetUpgradesOnUI()
    {
        OnUpgradeAction?.Invoke();

        SetPowerOnUI();
        SetFireRangeOnUI();
        SetFireRateOnUI();
    }

    #region Separately Upgrade If U Need

    private void UpgradeSelectedStat(Stat stat)
    {
        if (!IsPurchasable(stat))
            return;
        BuyUpgrade(stat);
        UpgradeStat(stat);
        switch (stat)
        {
            case Stat.FireRange:
                SetFireRangeOnUI();
                break;
            case Stat.FireRate:
                SetFireRateOnUI();
                break;
            case Stat.Power:
                SetPowerOnUI();
                break;
        }

        OnUpgradeAction?.Invoke();
    }

    #endregion

    private void UpgradeAllStats()
    {
        if (!IsPurchasable(Stat.Power))
            return;
        BuyUpgrade(Stat.Power);
        UpgradeStat(Stat.Power);
        SetPowerOnUI();
        BuyUpgrade(Stat.FireRange);
        UpgradeStat(Stat.FireRange);
        SetFireRangeOnUI();
        BuyUpgrade(Stat.FireRate);
        UpgradeStat(Stat.FireRate);
        SetFireRateOnUI();
        OnUpgradeAction?.Invoke();
    }

    private void SetPowerOnUI()
    {
        var cost = playerStats.GetStatCost(Stat.Power).ToString();
        var currentLevelStat = playerStats.GetStat(Stat.Power).ToString(CultureInfo.CurrentCulture);
        if (IsStatOnMaxLevel(Stat.Power))
        {
            armUpgradesUI.SetPower(currentLevelStat);
            return;
        }

        var nextLevelStat =
            playerStats.GetNextLevelStat(Stat.Power).ToString(CultureInfo.CurrentCulture);
        armUpgradesUI.SetPower(cost, currentLevelStat, nextLevelStat);
    }

    private void SetFireRangeOnUI()
    {
        var cost = playerStats.GetStatCost(Stat.FireRange).ToString();
        var currentLevelStat = playerStats.GetStat(Stat.FireRange).ToString(CultureInfo.CurrentCulture);
        if (IsStatOnMaxLevel(Stat.FireRange))
        {
            armUpgradesUI.SetFireRange(currentLevelStat);
            return;
        }

        var nextLevelStat =
            playerStats.GetNextLevelStat(Stat.FireRange).ToString(CultureInfo.CurrentCulture);
        armUpgradesUI.SetFireRange(cost, currentLevelStat, nextLevelStat);
    }

    private void SetFireRateOnUI()
    {
        var cost = playerStats.GetStatCost(Stat.FireRate).ToString();
        var currentLevelStat = playerStats.GetStat(Stat.FireRate).ToString(CultureInfo.CurrentCulture);
        if (IsStatOnMaxLevel(Stat.FireRate))
        {
            armUpgradesUI.SetFireRate(currentLevelStat);
            return;
        }

        var nextLevelStat =
            playerStats.GetNextLevelStat(Stat.FireRate).ToString(CultureInfo.CurrentCulture);
        armUpgradesUI.SetFireRate(cost, currentLevelStat, nextLevelStat);
    }


    private bool IsPurchasable(Stat stat)
    {
        var ownedMoney = playerInventory.GetMoney();
        return playerStats.GetStatLevel(stat) != playerStats.GetStatMaxLevel(stat) && ownedMoney >= GetCost(stat);
    }

    private bool IsStatOnMaxLevel(Stat stat)
    {
        return playerStats.GetStatLevel(stat) == playerStats.GetStatMaxLevel(stat);
    }

    private void BuyUpgrade(Stat stat)
    {
        int cost = playerStats.GetStatCost(stat);
        playerInventory.SpendMoney(cost);
    }

    private int GetCost(Stat stat)
    {
        return playerStats.GetStatCost(stat);
    }

    private void UpgradeStat(Stat stat)
    {
        playerStats.UpgradeStat(stat);
    }

    private void SetPowerUpgradeButtonInteractable()
    {
        armUpgradesUI.SetUpgradeButtonInteractable(IsPurchasable(Stat.Power));
    }

    private void SubscribeToButtonActions()
    {
        // playerUpgradesUI.OnPowerUpgradeRequest += UpgradeSelectedStat;
        armUpgradesUI.OnAllUpgradeRequest += UpgradeAllStats;
    }

    private void UnSubscribeToButtonActions()
    {
        // playerUpgradesUI.OnPowerUpgradeRequest -= UpgradeSelectedStat;
        armUpgradesUI.OnAllUpgradeRequest -= UpgradeAllStats;
    }

    private void SubscribeToOnUpgradeAction()
    {
        OnUpgradeAction += SetPowerUpgradeButtonInteractable;
    }

    private void UnSubscribeToOnUpgradeAction()
    {
        OnUpgradeAction -= SetPowerUpgradeButtonInteractable;
    }

    private void EnableUpgradeCanvas()
    {
        armUpgradesUI.OpenUI();
    }

    private void DisableUpgradeCanvas()
    {
        armUpgradesUI.CloseUI();
    }
}