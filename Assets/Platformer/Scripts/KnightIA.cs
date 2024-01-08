using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public enum KnightIAMode
    {
        Patrol,
        Fight,
    }
    public class KnightIA : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Animator animator;
        [SerializeField] private ActivationArea activationArea;
        [SerializeField] private Transform moveRight;
        [SerializeField] private Transform moveLeft;
        private BoxCollider2D boxCollider;
        private KnightIAMode mode;
        private Vector3 positionTarget;
        private ICharacter character;
        private Shooter shooter;
        private float cooldownTimer;

        private void Awake()
        {
            character = GetComponent<ICharacter>();
            boxCollider = GetComponent<BoxCollider2D>();
            shooter = GetComponent<Shooter>();
        }

        void Start()
        {
            mode = KnightIAMode.Patrol;
            activationArea.OnActivate.AddListener(Activate);
            activationArea.OnDeactivate.AddListener(Deactivate);
            positionTarget = moveRight.position;
        }

        // Update is called once per frame
        void Update()
        {
            switch (mode)
            {
                case KnightIAMode.Patrol:
                    Patrol();
                    break;
                case KnightIAMode.Fight:
                    Fight();
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }

        private void Fight()
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer <= attackCooldown) return;
            cooldownTimer = 0;
            var collider = InAttackRange();
            if (collider != null)
            {
                animator.SetTrigger("Attack");
                var health = collider.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
            else
            {
                if (Random.Range(0, 100) < 30)
                {
                    character.Move(new Vector2(transform.localScale.x, 0));
                }
                else
                {
                    shooter.StartCharging();
                    shooter.ReleaseShoot();
                }
            }
        }

        private Collider2D InAttackRange()
        {
            var hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, new Vector2(transform.localScale.x, 0), 0, playerLayer);
            return hit.collider;
        }

        private void Patrol()
        {
            var direction = positionTarget - transform.position;
            direction.Normalize();
            if (Mathf.Abs(direction.x) < 0.1)
            {
                positionTarget = positionTarget == moveRight.position ? moveLeft.position : moveRight.position;
                direction = positionTarget - transform.position;
                direction.Normalize();
            }
            character.Move(direction);
        }

        private void MoveTo(Vector3 position)
        {

        }

        private void Activate()
        {
            cooldownTimer = 0;
            mode = KnightIAMode.Fight;
        }

        private void Deactivate()
        {
            mode = KnightIAMode.Patrol;
        }
    }
}
