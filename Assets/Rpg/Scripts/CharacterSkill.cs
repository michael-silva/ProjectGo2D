using System;
using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

namespace ProjectGo2D.Rpg
{


    public abstract class CharacterSkill : MonoBehaviour
    {
        public readonly UnityEvent OnSkillFinished = new UnityEvent();
        [SerializeField] private string skillName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string description;
        [SerializeField] private bool lockControls;

        // [SerializeField, ReadOnly] private float timer;
        // [SerializeField, ReadOnly] private bool running;

        // protected void Update()
        // {
        //     timer += Time.deltaTime;
        //     if (HasFinished())
        //     {
        //         Finish();
        //     }
        // }

        public virtual void Prepare() { }
        public abstract bool TryExecute();
        public virtual void Release() { }
        protected virtual void Finish()
        {
            // running = false;
            // ResetTimer();
            OnSkillFinished.Invoke();
        }

        public bool NeedToLockControls()
        {
            return lockControls;
        }
        // protected bool CanExecute()
        // {
        //     return !running && timer > cooldown;
        // }
        // protected bool HasFinished()
        // {
        //     return running && timer > duration;
        // }
        // protected void StartTimer()
        // {
        //     running = true;
        //     ResetTimer();
        // }
        // private void ResetTimer()
        // {
        //     timer = 0;
        // }
        public string GetName()
        {
            return skillName;
        }
        public string GetDescription()
        {
            return description;
        }
        public Sprite GetSprite()
        {
            return sprite;
        }

    }
}
