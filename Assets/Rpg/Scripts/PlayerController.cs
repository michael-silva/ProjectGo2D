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
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private LayerMask collectableLayer;
        [SerializeField, ReadOnly] private Vector2 direction;

        [SerializeField, ReadOnly] private bool lockControls;
        private CharacterBehaviour character;
        private BoxCollider2D boxCollider;
        private PlayerInputActions inputActions;
        private float attackTimer;
        private IInteractive interactiveFocus;

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
            ApplyMovement(inputVector);
            TestInteraction();
            TestCollectables();
        }

        private void OnDrawGizmos()
        {
            if (!boxCollider) return;
            var position = GetColliderCenter(direction);
            Gizmos.DrawWireCube(position, boxCollider.bounds.size);
        }

        private void TestCollectables()
        {
            if (!IsMoving()) return;
            var hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, direction, collisionDistance, collectableLayer);
            var collectable = hit.collider?.GetComponent<IICollectable>();
            if (collectable == null) return;
            collectable.Collect(character);
        }

        private void TestInteraction()
        {
            if (!IsMoving()) return;
            var hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, direction, collisionDistance, interactionLayer);
            var interactive = hit.collider?.GetComponent<IInteractive>();
            if (interactive == interactiveFocus) return;
            if (interactiveFocus != null) interactiveFocus.Disable();
            if (interactive != null) interactive.Enable();
            interactiveFocus = interactive;
        }

        private void ApplyMovement(Vector2 inputVector)
        {
            var movement = new Vector2(inputVector.normalized.x, inputVector.normalized.y);
            if (movement != Vector2.zero)
            {
                direction = movement;
                bool moved = character.TryMove(boxCollider, direction, collisionDistance, collisionLayer);
                if (!moved)
                {
                    moved = character.TryMove(boxCollider, new Vector2(direction.x, 0), collisionDistance, collisionLayer);
                }
                if (!moved)
                {
                    character.TryMove(boxCollider, new Vector2(0, direction.y), collisionDistance, collisionLayer);
                }
                if (moved && dustParticles.isStopped)
                {
                    dustParticles.Play();
                }
            }
            else
            {
                direction = Vector2.zero;
                dustParticles.Stop();
            }
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
            if (interactiveFocus != null)
            {
                interactiveFocus.Interact();
                return;
            }
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
