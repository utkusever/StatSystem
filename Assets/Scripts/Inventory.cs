using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    [SerializeField] private int money;

    public int GetMoney()
    {
        return money;
    }

    public void SpendMoney(int value)
    {
        money -= value;
    }
}