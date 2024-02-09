using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class SwordSkill : CharacterSkill
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private PlayerAnim characterAnimator;
        [SerializeField] private BoxCollider2D swordHorizontalHitbox;
        [SerializeField] private BoxCollider2D swordVerticalHitbox;
        [SerializeField, ReadOnly] private Vector2 swordHorizontalHitboxOffset;
        [SerializeField, ReadOnly] private Vector2 swordVerticalHitboxOffset;

        [SerializeField, ReadOnly] private bool isPrepared;
        [SerializeField, ReadOnly] private bool isRunning;
        [SerializeField, ReadOnly] private float timer;

        private void Start()
        {
            swordHorizontalHitboxOffset = swordHorizontalHitbox.transform.localPosition;
            swordVerticalHitboxOffset = swordVerticalHitbox.transform.localPosition;
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
            EnableHitbox();
            characterAnimator.RunAttack();
            return true;
        }

        protected override void Finish()
        {
            base.Finish();
            timer = 0;
            isPrepared = false;
            isRunning = false;
            DisableHitbox();
        }

        private void EnableHitbox()
        {
            var direction = character.GetDirection();
            var isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
            if (isHorizontal)
            {
                float x = direction.x < 0 ? -swordHorizontalHitboxOffset.x : swordHorizontalHitboxOffset.x;
                swordHorizontalHitbox.transform.localPosition = new Vector2(x, swordHorizontalHitboxOffset.y);
                swordHorizontalHitbox.enabled = true;
            }
            else
            {
                float y = direction.y < 0 ? -swordVerticalHitboxOffset.y : swordVerticalHitboxOffset.y;
                swordVerticalHitbox.transform.localPosition = new Vector2(swordVerticalHitboxOffset.x, y);
                swordVerticalHitbox.enabled = true;

            }
        }

        private void DisableHitbox()
        {
            swordHorizontalHitbox.enabled = false;
            swordVerticalHitbox.enabled = false;
        }
        protected bool HasFinished()
        {
            return isRunning && timer > duration;
        }
        protected bool CanPrepare()
        {
            return !isPrepared && timer > cooldown;
        }
        protected bool CanExecute()
        {
            return isPrepared && !isRunning;
        }
    }
}
