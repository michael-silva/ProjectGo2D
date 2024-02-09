using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class FireballSkill : CharacterSkill
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField] private float offset;
        [SerializeField] private float chargeDelay;
        [SerializeField] private ParticleSystem chargingEffect;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private ObjectPool<ArrowBullet> fireballPool;
        [SerializeField] private ObjectPool<ArrowBullet> superPool;
        [SerializeField, ReadOnly] private bool isPrepared;
        [SerializeField, ReadOnly] private bool isRunning;
        [SerializeField, ReadOnly] private float timer;

        private void Start()
        {
            chargingEffect.Stop();
            fireballPool.Prepare();
            superPool.Prepare();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (HasFinished()) Finish();
        }

        public override void Prepare()
        {
            if (!CanPrepare()) return;
            isPrepared = true;
            timer = 0;
            chargingEffect.Play();
        }


        public override bool TryExecute()
        {
            if (!CanExecute()) return false;
            isRunning = true;
            timer = 0;
            return true;
        }

        public override void Release()
        {
            if (!isRunning) return;
            var direction = character.GetDirection();
            var position = transform.position + (new Vector3(direction.x, direction.y, 0) * offset);
            bool isSuperShot = timer > chargeDelay;
            var fire = isSuperShot ? superPool.GetElement(position) : fireballPool.GetElement(position);
            fire.Shoot(direction);
            Finish();
        }

        protected override void Finish()
        {
            base.Finish();
            timer = 0;
            isPrepared = false;
            isRunning = false;
            chargingEffect.Stop();
            chargingEffect.Clear();
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
