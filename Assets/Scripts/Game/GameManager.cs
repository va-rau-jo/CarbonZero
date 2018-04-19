using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public int tickSpeed;

        private TabManager tabManager;
        private CarbonManager carbonManager;
        private EconomyManager economyManager;
        private bool gameStarted;

        private int timeInSeconds;
        private DateTime startTime;

        private void Awake()
        {
            tabManager = GameObject.FindGameObjectWithTag("TabController").GetComponent<TabManager>();
        }

        private void Update()
        {
            if (gameStarted)
            {
                timeInSeconds = (DateTime.Now.Second - startTime.Second);
            }
        }

        public void StartGame(GameObject button)
        {
            Destroy(button);
            gameStarted = true;
            tabManager.ShowEconomyPanel();
        }

        public bool GameHasStarted()
        {
            return gameStarted;
        }
    }
}
