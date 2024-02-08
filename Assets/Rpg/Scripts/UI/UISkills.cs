using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectGo2D.Rpg
{
    public class UISkills : MonoBehaviour
    {
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private PlayerController player;
        [SerializeField] private UIButtonList currentSkills;
        [SerializeField] private UIButtonList availableSkills;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI skillDescription;

        // Start is called before the first frame update
        void Start()
        {
            SyncSkills();
            currentSkills.OnChangeIndex.AddListener(ShowSelectCurrentSkill);
            currentSkills.OnActivate.AddListener(StartChangingCurrentSkill);
            availableSkills.OnChangeIndex.AddListener(ShowSelectedAvailableSkill);
            availableSkills.OnActivate.AddListener(SetAvailableSkillAsCurrent);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetAvailableSkillAsCurrent(int index)
        {
            if (!gameObject.activeSelf) return;
            var skill = player.GetAvailableSkillAt(index);
            var buttons = currentSkills.GetAllButtons();
            if (currentSkills.GetFocusedIndex() == 0)
            {
                buttons[0].EnableImage(skill.GetSprite());
                player.SetMainSkill(skill);
            }
            else
            {
                buttons[1].EnableImage(skill.GetSprite());
                player.SetAuxiliarySkill(skill);
            }
            availableSkills.enabled = false;
            currentSkills.enabled = true;
        }

        private void StartChangingCurrentSkill(int index)
        {
            if (!gameObject.activeSelf) return;
            availableSkills.enabled = true;
            currentSkills.enabled = false;

        }
        private void ShowSelectCurrentSkill(int index)
        {
            if (!gameObject.activeSelf) return;
            var skill = index == 0 ? player.GetMainSkill() : player.GetAuxiliarySkill();
            var skillIndex = player.GetAvailableSkillIndex(skill);
            availableSkills.FocusInto(skillIndex);
        }

        private void ShowSelectedAvailableSkill(int index)
        {
            if (!gameObject.activeSelf) return;
            var skill = player.GetAvailableSkillAt(index);
            skillName.text = skill.GetName();
            skillDescription.text = skill.GetDescription();
        }



        private void SyncSkills()
        {
            var skills = player.GetAvailableSkills();
            var avButtons = availableSkills.GetAllButtons();
            for (int i = 0; i < avButtons.Count; i++)
            {
                if (i >= skills.Count) avButtons[i].DisableImage();
                else avButtons[i].EnableImage(skills[i].GetSprite());
            }
            var curButtons = currentSkills.GetAllButtons();
            curButtons[0].EnableImage(player.GetMainSkill().GetSprite());
            curButtons[1].EnableImage(player.GetAuxiliarySkill().GetSprite());
        }

        private void OnEnable()
        {
            availableSkills.enabled = false;
            currentSkills.enabled = true;
        }

        private void OnDisable()
        {
            currentSkills.ResetIndex();
        }
    }
}
