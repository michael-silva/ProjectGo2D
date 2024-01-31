using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class PlayerAnim : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private int flashNumbers;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            character.OnHealthChange.AddListener(HandleHealthChanged);
            player.OnAttack.AddListener(() => animator.SetTrigger("Attack"));
        }

        // Update is called once per frame
        void Update()
        {
            bool isMoving = player.IsMoving();
            var direction = player.GetDirection();
            animator.SetBool("Walking", isMoving);
            if (isMoving)
            {
                animator.SetFloat("DirectionX", direction.x);
                animator.SetFloat("DirectionY", direction.y);
            }
        }

        private void HandleHealthChanged(float newHealth, float oldHealth)
        {
            if (newHealth == 0)
            {
                animator.SetTrigger("Die");
            }
            else if (newHealth < oldHealth)
            {
                StartCoroutine(BlinkSprite());
            }
        }

        private IEnumerator BlinkSprite()
        {
            float interval = character.GetInvulnerableDuration() / (flashNumbers * 2);
            for (int i = 0; i < flashNumbers; i++)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(interval);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(interval);
            }
        }

        public void FinishAttack()
        {
            player.StopAttack();
        }
    }
}
