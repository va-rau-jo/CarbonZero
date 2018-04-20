using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class EconomyManager : MonoBehaviour
    {
        public Dictionary<String, int> resourcePrices = new Dictionary<string, int>()
        {
            {"Solar Panel", 100 },
            {"Wind Turbine", 200 },
        };

        // Name and upgrade level TODO: change int to custom class with thigns
        private List<Resource> assets = new List<Resource>();

        public readonly float startingBalance = 200;

        private float currentBalance;
        private float currentAssets;
        private Text balanceText;
        private Text assetsText;
        private GameObject economyPanel;

        private void Awake()
        {
            balanceText = GameObject.Find("Balance").transform.GetChild(0).GetComponent<Text>();
            assetsText = GameObject.Find("Assets").transform.GetChild(0).GetComponent<Text>();
            currentBalance = startingBalance;
        }

        public void AddAsset(String name)
        {
            assets.Add(new Resource(name, 1));
            currentBalance -= resourcePrices[name];
            currentAssets += resourcePrices[name];
        }

        private void Update()
        {
            balanceText.text = FormatToDollars(currentBalance);
        }

        private string FormatToDollars(object name)
        {
            return "$" + Math.Round(float.Parse(name.ToString()), 2);
        }

        public float GetCurrentBalance()
        {
            return currentBalance;
        }
    }
}
