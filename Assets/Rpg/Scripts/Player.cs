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
        bool ApplyDamage(CharacterBehaviour enemy, DamageType type);
        bool TakeDamage(float enemy, DamageType type);
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
        [SerializeField] private float impact;
        [SerializeField] private float criticalChance;
        [SerializeField] private float invulnerableDuration;
        private bool isInvulnerable;
        private Vector2 receivedImpact;


        public bool IsInvulnerable()
        {
            return isInvulnerable;
        }

        public float GetInvulnerableDuration()
        {
            return invulnerableDuration;
        }

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

        private void ApplyImpact(Vector2 direction, float force)
        {
            bool received = Random.Range(0, 1) < force;
            if (!received) return;
            receivedImpact = direction * force;
        }

        public bool TakeDamage(float damage, DamageType type)
        {
            if (isInvulnerable) return false;

            float defend = Random.Range(0, defense);
            float realDamage = Mathf.Max(0, damage - defend);
            float newHealth = Mathf.Max(0, health - realDamage);
            if (newHealth == health) return false;
            OnHealthChange.Invoke(newHealth, health);
            health = newHealth;
            StartCoroutine(Invulnerability());
            return true;
        }

        protected void Update()
        {
            if (receivedImpact == Vector2.zero) return;
            transform.Translate(receivedImpact * Time.deltaTime);
        }

        private IEnumerator Invulnerability()
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerableDuration);
            receivedImpact = Vector2.zero;
            isInvulnerable = false;
        }

        public bool ApplyDamage(CharacterBehaviour enemy, DamageType type)
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
            if (enemy.TakeDamage(damage, type))
            {
                var dir = enemy.transform.position - transform.position;
                dir.Normalize();
                enemy.ApplyImpact(dir, impact);
            }
            return false;
        }
    }

    public class Player : CharacterBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        private new void Update()
        {
            base.Update();
        }
    }
}
