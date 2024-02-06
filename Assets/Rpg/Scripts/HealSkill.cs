using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class HealSkill : CharacterSkill
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private float power;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterBehaviour character;
        private float timer;
        private bool isPrepared;
        private bool isRunning;

        void Start()
        {
            animator.gameObject.SetActive(false);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (HasFinished()) Finish();
        }

        public override void Prepare()
        {
            if (!CanPrepare()) return;
            isPrepared = true;
            timer = 0;
        }

        public override bool TryExecute()
        {
            if (!CanExecute()) return false;
            isRunning = true;
            timer = 0;
            animator.gameObject.SetActive(true);
            animator.Play("Heal_Default", -1, 0);
            character.HealHealth(power);
            return true;
        }

        protected override void Finish()
        {
            base.Finish();
            timer = 0;
            isRunning = false;
            isPrepared = false;
            animator.gameObject.SetActive(false);
        }
        protected bool HasFinished()
        {
            return isRunning && timer > duration;
        }
        protected bool CanExecute()
        {
            return isPrepared && !isRunning;
        }
        protected bool CanPrepare()
        {
            return !isPrepared && timer > cooldown;
        }
    }
}
