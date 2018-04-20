using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static float d = 255;
        public readonly Color SELECTED_BUTTON_COLOR = new Color(20 / d, 200 / d, 100 / d, 1);
        public readonly int MAX_TIME = 100;

        private TabManager tabManager;
        private CarbonManager carbonManager;
        private EconomyManager economyManager;
        public Text dayText;

        public int daysPerSecond;
        private bool gameStarted;
        private float dayNum = 1;
        private DateTime startTime;
        private float scaleFactor = 0.01f;

        private void Awake()
        {
            tabManager = GameObject.FindGameObjectWithTag("TabManager").GetComponent<TabManager>();
            economyManager = transform.GetChild(0).gameObject.GetComponent<EconomyManager>();
            carbonManager = transform.GetChild(1).gameObject.GetComponent<CarbonManager>();
        }

        private void Update()
        {
            if (gameStarted)
            {
                float seconds = (daysPerSecond * (DateTime.UtcNow - startTime).Seconds) * scaleFactor;
                dayNum += seconds;
                dayText.text = "Day " + Math.Truncate(dayNum);
                if (seconds > MAX_TIME)
                    EndGame();
            }
            else
            {
                startTime = DateTime.Now;
            }
        }

        public void AddEnergyResource(string resourceName)
        {
            economyManager.AddAsset(resourceName);
        }

        public bool HasSufficientFundsToPlace(string resourceName)
        {
            return economyManager.GetCurrentBalance() >= economyManager.resourcePrices[resourceName];
        }

        public void StartGame(GameObject button)
        {
            Destroy(GameObject.Find("Fade"));
            Destroy(button);
            gameStarted = true;
            tabManager.ShowEconomyPanel();
        }

        public void EndGame()
        {
            gameStarted = false;
        }

        public bool GameHasStarted()
        {
            return gameStarted;
        }

        public void SetDaysPerSecond(GameObject button)
        {
            button.GetComponent<Image>().color = SELECTED_BUTTON_COLOR;
            switch (daysPerSecond)
            {
                case 0:
                    GameObject.Find("Pause").GetComponent<Image>().color = Color.white;
                    break;
                case 1:
                    GameObject.Find("Play").GetComponent<Image>().color = Color.white;
                    break;
                case 2:
                    GameObject.Find("Forward").GetComponent<Image>().color = Color.white;
                    break;
                case 5:
                    GameObject.Find("Skip").GetComponent<Image>().color = Color.white;
                    break;
            }

            switch (button.name)
            {
                case "Pause":
                    daysPerSecond = 0;
                    break;
                case "Play":
                    daysPerSecond = 1;
                    break;
                case "Forward":
                    daysPerSecond = 2;
                    break;
                case "Skip":
                    daysPerSecond = 5;
                    break;
            }
        }

    }
}
