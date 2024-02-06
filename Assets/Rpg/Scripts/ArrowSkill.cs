using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class ArrowSkill : CharacterSkill
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private float offset;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private ObjectPool<ArrowBullet> arrowsPool;

        [SerializeField, ReadOnly] private bool isPrepared;
        [SerializeField, ReadOnly] private bool isRunning;
        [SerializeField, ReadOnly] private float timer;

        void Start()
        {
            arrowsPool.Prepare();
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (HasFinished()) Finish();
        }

        public override void Prepare()
        {
            if (!CanPrepare()) return;
            // isPrepared = true;
            timer = 0;
        }

        public override bool TryExecute()
        {
            if (!CanPrepare()) return false;
            isRunning = true;
            timer = 0;
            var direction = character.GetDirection();
            var position = transform.position + (new Vector3(direction.x, direction.y, 0) * offset);
            var arrow = arrowsPool.GetElement(position);
            arrow.Shoot(direction);
            return true;
        }
        public override void Release()
        {
            if (!isRunning) return;
            Finish();
        }

        protected override void Finish()
        {
            base.Finish();
            timer = 0;
            isPrepared = false;
            isRunning = false;
        }

        protected bool HasFinished()
        {
            return isRunning && duration > 0 && timer > duration;
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
