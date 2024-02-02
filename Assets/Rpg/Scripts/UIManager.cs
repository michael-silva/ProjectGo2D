using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjectGo2D.Rpg
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviour character;
        // [SerializeField] private Image totalHealthbar;
        [SerializeField] private Image currentHealthbar;
        // [SerializeField] private Image totalManabar;
        [SerializeField] private Image currentManabar;
        [SerializeField] private TextMeshProUGUI moneyLabel;

        void Awake()
        {
            UpdateMana();
            UpdateHealth();
            UpdateMoney(character.GetMoney());
            character.OnHealthChange.AddListener((float old, float current) => UpdateHealth());
            character.OnManaChange.AddListener((float old, float current) => UpdateMana());
            character.OnMoneyChange.AddListener((float old, float current) => UpdateMoney(current));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

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
    }
}
