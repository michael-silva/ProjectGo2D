using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class SlimeAI : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float actionInterval;
        [SerializeField] private float viewDistance;
        [SerializeField] private float attackDuration;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private List<Transform> patrolPoints;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private BoxCollider2D attackHitbox;
        [SerializeField] private float collisionDistance;
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField, ReadOnly] private Vector2 direction;
        [SerializeField, ReadOnly] private int targetPointIndex;
        private BoxCollider2D boxCollider;
        private float actionTimer;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<CharacterBehaviour>();
            boxCollider = GetComponent<BoxCollider2D>();
            targetPointIndex = 0;
            character.OnHealthChange.AddListener(HandleHealthChanged);
        }

        // Update is called once per frame
        void Update()
        {
            actionTimer += Time.deltaTime;
            if (actionTimer < actionInterval)
            {
                animator.SetBool("Walking", false);
                return;
            }
            var playerTransform = SeePlayerInRange();
            bool isFollowPlayer = playerTransform != null;
            var targetPoint = isFollowPlayer ? playerTransform.position : GetTargetPosition();
            if (Vector2.Distance(targetPoint, transform.position) < 0.05f)
            {
                if (isFollowPlayer)
                {
                    Attack();
                    actionTimer = 0;
                }
                else
                {
                    NextPatrolPoint();
                    actionTimer = 0;
                }
            }
            var inputVector = targetPoint - transform.position;
            var movement = new Vector2(inputVector.normalized.x, inputVector.normalized.y);
            if (movement != Vector2.zero)
            {
                animator.SetBool("Walking", true);
                direction = movement;
                bool moved = TryMove(direction);
                if (!moved)
                {
                    moved = TryMove(new Vector2(direction.x, 0));
                }
                if (!moved)
                {
                    TryMove(new Vector2(0, direction.y));
                }

                if (direction.x < 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
                }
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }

        private void Attack()
        {
            animator.SetTrigger("Attack");
            attackHitbox.enabled = true;
            StartCoroutine(DisableHitbox());
        }

        private IEnumerator DisableHitbox()
        {
            yield return new WaitForSeconds(attackDuration);
            attackHitbox.enabled = false;
        }

        private void NextPatrolPoint()
        {
            targetPointIndex++;
            if (targetPointIndex >= patrolPoints.Count)
            {
                targetPointIndex = 0;
            }
        }

        private Vector3 GetTargetPosition()
        {
            return patrolPoints.ElementAt(targetPointIndex).position;
        }

        private void HandleHealthChanged(float newHealth, float oldHealth)
        {
            if (newHealth == 0)
            {
                animator.SetTrigger("Die");
                boxCollider.enabled = false;
                enabled = false;
            }
            else if (newHealth < oldHealth)
            {
                animator.SetTrigger("Hit");
            }
        }

        private bool TryMove(Vector2 direction)
        {
            var position = boxCollider.bounds.center;
            var hit = Physics2D.BoxCast(position, boxCollider.bounds.size, 0, direction, collisionDistance, collisionLayer);
            if (hit.collider == null)
            {
                transform.Translate(direction * character.GetSpeed() * Time.deltaTime);
                return true;
            }
            return false;
        }

        private Transform SeePlayerInRange()
        {
            var hit = Physics2D.CircleCast(transform.position, viewDistance, Vector2.zero, 0, playerLayer);
            if (hit.collider != null)
            {
                return hit.collider.transform;
            }
            return null;
        }


    }
}