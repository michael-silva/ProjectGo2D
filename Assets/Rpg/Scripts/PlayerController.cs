using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectGo2D.Rpg
{
    public class PlayerController : MonoBehaviour
    {
        public readonly UnityEvent OnAttack = new UnityEvent();
        [SerializeField] private ParticleSystem dustParticles;
        [SerializeField] private BoxCollider2D swordHorizontalHitbox;
        [SerializeField] private BoxCollider2D swordVerticalHitbox;
        [SerializeField, ReadOnly] private Vector2 swordHorizontalHitboxOffset;
        [SerializeField, ReadOnly] private Vector2 swordVerticalHitboxOffset;
        [SerializeField] private float collisionDownOffset;
        [SerializeField] private float collisionUpOffset;
        [SerializeField] private float collisionDistance;
        [SerializeField] private float attackCooldown;
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField, ReadOnly] private Vector2 direction;

        [SerializeField, ReadOnly] private bool lockControls;
        private CharacterBehaviour character;
        private BoxCollider2D boxCollider;
        private PlayerInputActions inputActions;
        private float attackTimer;

        void Start()
        {
            character = GetComponent<CharacterBehaviour>();
            boxCollider = GetComponent<BoxCollider2D>();
            inputActions = new PlayerInputActions();
            inputActions.Player.Attack.performed += context => StartAttack();
            inputActions.Player.Enable();
            swordHorizontalHitboxOffset = swordHorizontalHitbox.transform.localPosition;
            swordVerticalHitboxOffset = swordVerticalHitbox.transform.localPosition;
            character.OnHealthChange.AddListener(HandleHealthChanged);
        }

        void Update()
        {
            if (lockControls) return;
            attackTimer += Time.deltaTime;
            var inputVector = inputActions.Player.Move.ReadValue<Vector2>();
            var movement = new Vector2(inputVector.normalized.x, inputVector.normalized.y);
            if (movement != Vector2.zero)
            {
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
                if (moved)
                {

                    dustParticles.Play();
                }
                else
                {

                    dustParticles.Stop();
                }
            }
            else
            {
                direction = Vector2.zero;
                dustParticles.Stop();
            }
        }

        private void OnDrawGizmos()
        {
            if (!boxCollider) return;
            var position = GetColliderCenter(direction);
            Gizmos.DrawWireCube(position, boxCollider.bounds.size);
        }

        private void HandleHealthChanged(float newHealth, float oldHealth)
        {
            if (newHealth == 0)
            {
                boxCollider.enabled = false;
                LockControls();
            }
        }

        private Vector3 GetColliderCenter(Vector2 direction)
        {
            return boxCollider.bounds.center;
            // if (direction.y > 0)
            // {
            //     position.y += collisionUpOffset;
            // }
            // else if (direction.y < 0)
            // {
            //     position.y += collisionDownOffset;
            // }
            // return position;
        }

        private bool TryMove(Vector2 direction)
        {
            var position = GetColliderCenter(direction);
            var hit = Physics2D.BoxCast(position, boxCollider.bounds.size, 0, direction, collisionDistance, collisionLayer);
            if (hit.collider == null)
            {
                transform.Translate(direction * character.GetSpeed() * Time.deltaTime);
                return true;
            }
            return false;
        }

        private void EnableHitbox()
        {
            var isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
            if (isHorizontal)
            {
                float x = direction.x < 0 ? -swordHorizontalHitboxOffset.x : swordHorizontalHitboxOffset.x;
                swordHorizontalHitbox.transform.localPosition = new Vector2(x, swordHorizontalHitboxOffset.y);
                swordHorizontalHitbox.enabled = true;
            }
            else
            {
                float y = direction.y >= 0 ? -swordVerticalHitboxOffset.y : swordVerticalHitboxOffset.y;
                swordVerticalHitbox.transform.localPosition = new Vector2(swordVerticalHitboxOffset.x, y);
                swordVerticalHitbox.enabled = true;

            }
        }

        private void DisableHitbox()
        {
            swordHorizontalHitbox.enabled = false;
            swordVerticalHitbox.enabled = false;
        }

        public void StartAttack()
        {
            if (lockControls || attackTimer < attackCooldown) return;
            OnAttack.Invoke();
            LockControls();
            EnableHitbox();
        }

        public void StopAttack()
        {
            attackTimer = 0;
            UnlockMovement();
            DisableHitbox();
        }

        public Vector2 GetDirection()
        {
            return direction;
        }

        public bool IsMoving()
        {
            return direction != Vector2.zero && !lockControls;
        }

        public void LockControls()
        {
            lockControls = true;
        }
        public void UnlockMovement()
        {
            lockControls = false;
        }
    }
}
