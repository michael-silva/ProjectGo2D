using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public enum DamageType
    {
        Melee,
        Shoot,
        Magic,
    }

    public interface ICharacter
    {
        float GetSpeed();
        void HealHealth(float heal);
        void ApplyDamage(ICharacter enemy, DamageType type);
        void TakeDamage(float enemy, DamageType type);
    }

    public abstract class CharacterBehaviour : MonoBehaviour, ICharacter
    {
        public readonly UnityEvent<float, float> OnHealthChange = new UnityEvent<float, float>();
        [SerializeField] private float health;
        [SerializeField] private float mana;
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxMana;
        [SerializeField] private float strength;
        [SerializeField] private float defense;
        [SerializeField] private float speed;
        [SerializeField] private float magic;
        [SerializeField] private float shot;
        [SerializeField] private float criticalChance;
        private bool isInvulnerable;

        public float GetSpeed()
        {
            return speed;
        }

        public void SetInvulnerable(bool value)
        {
            isInvulnerable = value;
        }

        public void HealHealth(float heal)
        {
            float newHealth = Mathf.Min(maxHealth, health + heal);
            OnHealthChange.Invoke(newHealth, health);
            health = newHealth;
        }

        public void TakeDamage(float damage, DamageType type)
        {
            if (isInvulnerable) return;
            float defend = Random.Range(0, defense);
            float realDamage = Mathf.Max(0, damage - defend);
            float newHealth = Mathf.Max(0, health - realDamage);
            OnHealthChange.Invoke(newHealth, health);
            health = newHealth;
        }

        public void ApplyDamage(ICharacter enemy, DamageType type)
        {
            float force = strength;
            switch (type)
            {
                case DamageType.Shoot:
                    force = shot;
                    break;
                case DamageType.Magic:
                    force = magic;
                    break;
            }
            float damage = Random.Range(0, 1) < criticalChance ? force * 2 : force;
            enemy.TakeDamage(damage, type);
        }
    }

    public class Player : CharacterBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
