using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectGo2D.Platformer
{
    public interface IHealth
    {
        void TakeDamage(float damage);
        void Heal(float health);
        void IncreaseMaxHealth(float value = 1);

    }

    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private Animator animator;

        [SerializeField] private float invulnerableDuration;
        [SerializeField] private int flashNumbers;
        [SerializeField] private int enemyLayerNumber;
        [SerializeField] private SpriteRenderer sprite;
        private Respawn respawn;
        private const int minHealthValue = 0;
        private const int maxHealthValue = 10;
        public readonly UnityEvent<float> OnMaxHealthChange = new UnityEvent<float>();
        public readonly UnityEvent<float> OnHealthChange = new UnityEvent<float>();

        private void Start()
        {
            respawn = GetComponent<Respawn>();
            OnMaxHealthChange.Invoke(maxHealth);
            OnHealthChange.Invoke(currentHealth);
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Math.Max(minHealthValue, currentHealth - damage);
            OnHealthChange.Invoke(currentHealth);
            animator.SetTrigger("Hit");
            if (currentHealth > 0)
            {
                StartCoroutine(Invulnerability());
            }
            else
            {
                animator.SetBool("Dead", true);
                StartCoroutine(EndGame());
            }
        }

        private IEnumerator Invulnerability()
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerNumber, true);
            float interval = invulnerableDuration / (flashNumbers * 2);
            for (int i = 0; i < flashNumbers; i++)
            {
                sprite.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(interval);
                sprite.color = Color.white;
                yield return new WaitForSeconds(interval);
            }
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerNumber, false);
        }

        public void Respawn()
        {
            animator.SetBool("Dead", false);
            Heal(maxHealth);
            StartCoroutine(Invulnerability());
        }


        private IEnumerator EndGame()
        {
            yield return new WaitForSeconds(0.8f);
            respawn.Spawn();
        }

        public void Heal(float health)
        {
            currentHealth = Math.Min(maxHealth, currentHealth + health);
            OnHealthChange.Invoke(currentHealth);
        }

        public void IncreaseMaxHealth(float value = 1)
        {

            maxHealth = Math.Min(maxHealthValue, maxHealth + value);
            OnMaxHealthChange.Invoke(maxHealth);
            Heal(maxHealth);
        }
    }

}
