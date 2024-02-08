using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class UICharacterStatus : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private UIButtonList statusButtons;
        [SerializeField] private int healthIndex;
        [SerializeField] private int manaIndex;
        [SerializeField] private int strengthIndex;
        [SerializeField] private int defenseIndex;
        [SerializeField] private int magicIndex;
        [SerializeField] private int shotIndex;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private TextMeshProUGUI points;
        [SerializeField] private TextMeshProUGUI exp;

        // Start is called before the first frame update
        void Start()
        {
            InitialSetup();
            UpdateUI();
            statusButtons.OnActivate.AddListener(HandleIncrease);
            statusButtons.OnChangeIndex.AddListener(HandleFocused);

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void InitialSetup()
        {

            var hasPoints = character.GetAvailablePoints() > 0;
            var buttons = statusButtons.GetAllButtons();
            ((UIStatusLabel)buttons[healthIndex]).Setup(character.GetHealth(), hasPoints);
            ((UIStatusLabel)buttons[manaIndex]).Setup(character.GetMana(), hasPoints);
            ((UIStatusLabel)buttons[strengthIndex]).Setup(character.GetStrength(), hasPoints);
            ((UIStatusLabel)buttons[defenseIndex]).Setup(character.GetDefense(), hasPoints);
            ((UIStatusLabel)buttons[magicIndex]).Setup(character.GetMagic(), hasPoints);
            ((UIStatusLabel)buttons[shotIndex]).Setup(character.GetShot(), hasPoints);
        }

        private void HandleFocused(int index)
        {
            if (!gameObject.activeSelf) return;
            var hasPoints = character.GetAvailablePoints() > 0;
            var button = (UIStatusLabel)statusButtons.GetAllButtons()[index];
            if (index == healthIndex)
            {
                button.Setup(character.GetHealth(), hasPoints);
            }
            if (index == manaIndex)
            {
                button.Setup(character.GetMana(), hasPoints);
            }
            if (index == strengthIndex)
            {
                button.Setup(character.GetStrength(), hasPoints);
            }
            if (index == defenseIndex)
            {
                button.Setup(character.GetDefense(), hasPoints);
            }
            if (index == magicIndex)
            {
                button.Setup(character.GetMagic(), hasPoints);
            }
            if (index == shotIndex)
            {
                button.Setup(character.GetShot(), hasPoints);
            }
        }

        private void HandleIncrease(int index)
        {
            if (!gameObject.activeSelf) return;
            var points = character.GetAvailablePoints();
            if (points == 0) return;
            var button = (UIStatusLabel)statusButtons.GetAllButtons()[index];
            if (index == healthIndex)
            {
                character.IncreaseHealth();
            }
            if (index == manaIndex)
            {
                character.IncreaseMana();
            }
            if (index == strengthIndex)
            {
                character.IncreaseStrength();
            }
            if (index == defenseIndex)
            {
                character.IncreaseDefense();
            }
            if (index == magicIndex)
            {
                character.IncreaseMagic();
            }
            if (index == shotIndex)
            {
                character.IncreaseShot();
            }
            bool isAvailable = points > 1;
            button.Increase(isAvailable);
            UpdateUI();
        }

        void UpdateUI()
        {
            level.text = character.GetLevel().ToString();
            points.text = character.GetAvailablePoints().ToString();
            exp.text = $"{character.GetExperience()} / {character.GetNextLevelExp()}";
        }
    }
}
