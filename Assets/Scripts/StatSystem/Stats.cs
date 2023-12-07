using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Stats : MonoBehaviour, ISaveable, ISerializationCallbackReceiver
{
    public Action<float> OnPowerChange;
    public Action<float> OnFireRateChange;
    public Action<float> OnFireRangeChange;
    [SerializeField] private Progression progression;

    [SerializeField] private string id;

    private Dictionary<Stat, int> statLevelsDictionary = new Dictionary<Stat, int>();

    public string ID => id;


    public float GetStat(Stat stat)
    {
        if (!HaveStat(stat))
            statLevelsDictionary[stat] = 1;
        return progression.GetStat(stat, statLevelsDictionary[stat]);
    }

    public float GetNextLevelStat(Stat stat)
    {
        if (!HaveStat(stat))
            statLevelsDictionary[stat] = 1;
        return progression.GetStat(stat, statLevelsDictionary[stat] + 1);
    }

    public int GetStatMaxLevel(Stat stat)
    {
        return progression.GetLevels(stat);
    }

    public int GetStatLevel(Stat stat)
    {
        if (!HaveStat(stat))
            statLevelsDictionary[stat] = 1;
        return statLevelsDictionary[stat];
    }

    public int GetStatCost(Stat stat)
    {
        if (!HaveStat(stat))
            statLevelsDictionary[stat] = 1;
        return progression.GetStatCost(stat, statLevelsDictionary[stat]);
    }

    public void UpgradeStat(Stat stat)
    {
        if (!HaveStat(stat))
            statLevelsDictionary[stat] = 2;
        else
            statLevelsDictionary[stat]++;


        InvokeUpdateAction(stat);
    }

    private bool HaveStat(Stat stat)
    {
        return statLevelsDictionary.ContainsKey(stat);
    }

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize()
    {
    }

    public StatData CreateDefaultData()
    {
        var statDataConfigList = progression.GetStatFields().Select(stat => new StatDataConfig(stat, 1)).ToList();
        var statData = new StatData(id, statDataConfigList);
        return statData;
    }

    public bool IsStatOnMaxLevel(Stat stat)
    {
        return !HaveStat(stat) || statLevelsDictionary[stat] == progression.GetLevels(stat);
    }

    private void InvokeUpdateAction(Stat stat)
    {
        switch (stat)
        {
            case Stat.Power:
                OnPowerChange?.Invoke(GetStat(stat));
                break;
            case Stat.FireRange:
                OnFireRangeChange?.Invoke(GetStat(stat));
                break;
            case Stat.FireRate:
                OnFireRateChange?.Invoke(GetStat(stat));
                break;
            default:
                Debug.LogWarning("Stat have no action");
                break;
        }
    }

    public object CaptureState()
    {
        return statLevelsDictionary;
    }

    public void RestoreState(object state)
    {
        statLevelsDictionary = (Dictionary<Stat, int>)state;
    }
}