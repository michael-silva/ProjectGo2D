using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public interface IICollectable
    {
        void Collect(ICharacter character);
    }

    public abstract class CollectableBehaviour : MonoBehaviour, IICollectable
    {

        [SerializeField] protected float friction;
        [SerializeField] protected float dismissTimer;
        [SerializeField] protected ParticleSystem particles;
        [SerializeField] protected float collisionDistance;
        [SerializeField] protected LayerMask collisionLayer;
        protected BoxCollider2D boxCollider;
        [SerializeField] protected Animator animator;
        [SerializeField, ReadOnly] protected Vector2 movement;

        public abstract void Collect(ICharacter character);

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            if (particles) particles.Stop();
        }

        private void Update()
        {
            if (movement == Vector2.zero) return;
            ApplyMovement();
        }

        protected void Collect()
        {
            if (!enabled) return;
            enabled = false;
            if (particles) particles.Play();
            StartCoroutine(Dismiss());
        }

        private IEnumerator Dismiss()
        {
            yield return new WaitForSeconds(dismissTimer);
            gameObject.SetActive(false);
        }

        public void Push(Vector2 direction, float force)
        {
            animator.SetTrigger("Jump");
            movement = direction * force;
        }

        private void ApplyMovement()
        {
            var position = boxCollider.bounds.center;
            var hit = Physics2D.BoxCast(position, boxCollider.bounds.size, 0, movement.normalized, collisionDistance, collisionLayer);
            if (hit.collider != null)
            {
                movement *= -1;
                return;
            }
            transform.Translate(movement * Time.deltaTime);
            movement *= friction;
            if (Mathf.Abs(movement.x) < 0.01) movement.x = 0;
            if (Mathf.Abs(movement.y) < 0.01) movement.y = 0;
        }
    }

    public class Coin : CollectableBehaviour
    {
        [SerializeField] private float value;

        public override void Collect(ICharacter character)
        {
            if (!enabled) return;
            character.AddMoney(value);
            base.Collect();
        }
    }
}
