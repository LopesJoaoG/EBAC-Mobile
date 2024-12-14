using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.Singleton;

public class ItemManager : MonoBehaviour
{
    public TextMeshProUGUI UICoins;
    public SOInt coins;
    public SOInt purpleCoins;
    public static ItemManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Reset();
    }
    private void Reset()
    {
        coins.value = 0;
        purpleCoins.value = 0;
    }

    public void AddCoins(string type, int amount = 1)
    {
        if(type == "coins")
        {
            coins.value += amount;
        }
        else if (type == "purpleCoins")
        {
            purpleCoins.value += amount;
        }
        UpdateInterfaceUI();
    }

    private void UpdateInterfaceUI()
    {
        //UICoins.text = "x " + coins.value.ToString();
    }
}
