using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private Animator animator;

        [SerializeField] private float invulnerableDuration;
        [SerializeField] private int flashNumbers;
        [SerializeField] private int enemyLayerNumber;
        [SerializeField] private SpriteRenderer sprite;
        private const int minHealthValue = 0;
        private const int maxHealthValue = 10;

        private bool isInvulnerable;
        public void TakeDamage(float damage)
        {
            if (isInvulnerable) return;
            currentHealth = Mathf.Max(minHealthValue, currentHealth - damage);
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
            isInvulnerable = true;
            float interval = invulnerableDuration / (flashNumbers * 2);
            for (int i = 0; i < flashNumbers; i++)
            {
                sprite.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(interval);
                sprite.color = Color.white;
                yield return new WaitForSeconds(interval);
            }
            isInvulnerable = false;
        }


        private IEnumerator EndGame()
        {
            yield return new WaitForSeconds(0.8f);
            Destroy(gameObject);
        }

        public void Heal(float health)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + health);
        }

        public void IncreaseMaxHealth(float value = 1)
        {
            maxHealth = Mathf.Min(maxHealthValue, maxHealth + value);
            Heal(maxHealth);
        }
    }

}
