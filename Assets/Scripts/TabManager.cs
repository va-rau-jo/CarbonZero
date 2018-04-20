using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TabManager : MonoBehaviour
    {
        private static float d = 255.0f;
        public readonly Color SHOWN_TAB_COLOR = new Color(17/d, 53/d, 88/d, 1);
        public readonly Color HIDDEN_TAB_COLOR = new Color(17/d, 53/d, 88/d, 100/d);

        private GameManager gameManager;
        private GameObject economyPanel;
        private GameObject carbonPanel;

        private void Awake()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            economyPanel = GameObject.Find("EconomyPanel");
            carbonPanel = GameObject.Find("CarbonPanel");

            ToggleButtons(economyPanel, false);
            economyPanel.transform.GetChild(0).GetComponent<Image>().color = SHOWN_TAB_COLOR; // do this again so tab is visible
            ToggleButtons(carbonPanel, false);
        }
        
        public void SwitchTab(string newTabName)
        {
            if (!gameManager.GameHasStarted())
                return;
            
            GameObject newPanel = (newTabName == economyPanel.name ? economyPanel : newTabName == carbonPanel.name ? carbonPanel : null);
            GameObject otherPanel = newPanel.Equals(economyPanel) ? carbonPanel : economyPanel;

            if (newPanel != null && otherPanel != null)
            {
                ToggleButtons(newPanel, true);
                ToggleButtons(otherPanel, false);
            }
        }

        public void ToggleButtons(GameObject panel, bool isVisible)
        {
            string nameToIgnore = "Tab";
            panel.transform.GetChild(0).GetComponent<Image>().color = (isVisible) ? SHOWN_TAB_COLOR : HIDDEN_TAB_COLOR;

            foreach (Transform child in panel.transform)
            {
                if (child.name != nameToIgnore)
                {
                    GameObject go = child.gameObject;
                    go.SetActive(isVisible);
                }
            }
        }

        public void ShowEconomyPanel()
        {
            SwitchTab(economyPanel.name);
        }

        public void ShowCarbonPanel()
        {
            SwitchTab(carbonPanel.name);
        }

    }
}