using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmMenuOpener : MonoBehaviour
{
    [SerializeField] private Button openButton;
    [SerializeField] private ArmUpgrades myArm;

    private void Start()
    {
        openButton.onClick.AddListener(OpenArmUI);
    }

    private void OpenArmUI()
    {
        myArm.OpenUpgradeUI();
    }
}