using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace ProjectGo2D.Rpg
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        [SerializeField] private CharacterBehaviour character;
        // [SerializeField] private Image totalHealthbar;
        [SerializeField] private Image currentHealthbar;
        // [SerializeField] private Image totalManabar;
        [SerializeField] private Image currentManabar;
        [SerializeField] private TextMeshProUGUI moneyLabel;

        [SerializeField] private GameObject menuObject;
        [SerializeField] private List<UIButton> tabs;
        private int tabActiveIndex;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            HideInventory();
            UpdateMana();
            UpdateHealth();
            UpdateMoney(character.GetMoney());
            character.OnHealthChange.AddListener((float old, float current) => UpdateHealth());
            character.OnManaChange.AddListener((float old, float current) => UpdateMana());
            character.OnMoneyChange.AddListener((float old, float current) => UpdateMoney(current));
            InputManager.Instance.OnNextUICalled.AddListener(MoveToNextTab);
            InputManager.Instance.OnPrevUICalled.AddListener(MoveToPrevTab);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void MoveToNextTab()
        {
            int currentTab = tabActiveIndex + 1;
            if (currentTab == tabs.Count) currentTab = 0;
            ActiveTab(currentTab);
        }

        private void MoveToPrevTab()
        {
            int currentTab = tabActiveIndex - 1;
            if (currentTab == -1) currentTab = tabs.Count - 1;
            ActiveTab(currentTab);
        }

        private void ActiveTab(int newIndex)
        {
            tabs[tabActiveIndex].Blur();
            tabs[newIndex].Focus();
            tabActiveIndex = newIndex;
        }

        private void UpdateHealth()
        {
            currentHealthbar.fillAmount = character.GetHealthPercent();
        }

        private void UpdateMana()
        {
            currentManabar.fillAmount = character.GetManaPercent();
        }

        private void UpdateMoney(float value)
        {
            moneyLabel.text = $"$ {value}";
        }

        public void ShowInventory()
        {
            menuObject.SetActive(true);
            ActiveTab(0);
        }

        public void HideInventory()
        {
            menuObject.SetActive(false);
        }
    }
}
