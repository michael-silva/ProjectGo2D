using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class BarrierSkill : CharacterSkill
    {
        [SerializeField] private GameObject realBarrier;
        [SerializeField] private float cooldown;
        [SerializeField] private float duration;
        [SerializeField, ReadOnly] private bool isPrepared;
        [SerializeField, ReadOnly] private bool isRunning;
        [SerializeField, ReadOnly] private float timer;

        void Start()
        {
            realBarrier.gameObject.SetActive(false);
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (HasFinished()) Finish();
        }

        public override void Prepare()
        {
            if (!CanPrepare()) return;
            timer = 0;
            isRunning = true;
            isPrepared = true;
            realBarrier.gameObject.SetActive(true);
        }

        public override bool TryExecute()
        {
            return isRunning;
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
            isRunning = false;
            isPrepared = false;
            realBarrier.gameObject.SetActive(false);
        }
        protected bool HasFinished()
        {
            return isRunning && timer > duration;
        }
        protected bool CanPrepare()
        {
            return !isPrepared && timer > cooldown;
        }
    }
}
