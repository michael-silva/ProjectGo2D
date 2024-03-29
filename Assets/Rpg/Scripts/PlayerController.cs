using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectGo2D.Shared;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectGo2D.Rpg
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dustParticles;
        [SerializeField] private float collisionDownOffset;
        [SerializeField] private float collisionUpOffset;
        [SerializeField] private float collisionDistance;
        [SerializeField] private CharacterSkill mainSkill;
        [SerializeField] private CharacterSkill auxSkill;
        [SerializeField] private List<CharacterSkill> availableSkills;
        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private LayerMask collectableLayer;

        [SerializeField, ReadOnly] private bool lockControls;
        private CharacterBehaviour character;
        private BoxCollider2D boxCollider;
        private IInteractive interactiveFocus;

        void Start()
        {
            character = GetComponent<CharacterBehaviour>();
            boxCollider = GetComponent<BoxCollider2D>();
            mainSkill.OnSkillFinished.AddListener(() => SkillEnded(mainSkill));
            auxSkill.OnSkillFinished.AddListener(() => SkillEnded(auxSkill));
            character.OnHealthChange.AddListener(HandleHealthChanged);
            InputManager.Instance.OnActionStarted.AddListener(MainActionStarted);
            InputManager.Instance.OnActionCalled.AddListener(() => ExecuteSkill(mainSkill));
            InputManager.Instance.OnActionReleased.AddListener(() => ReleaseSkill(mainSkill));
            InputManager.Instance.OnSecondaryActionStarted.AddListener(() => PrepareSkill(auxSkill));
            InputManager.Instance.OnSecondaryActionCalled.AddListener(() => ExecuteSkill(auxSkill));
            InputManager.Instance.OnSecondaryActionReleased.AddListener(() => ReleaseSkill(auxSkill));
        }

        void Update()
        {
            if (GameManager.Instance.IsPaused()) return;
            if (lockControls) return;
            var inputVector = InputManager.Instance.GetPlayerMovementVector();
            ApplyMovement(inputVector);
            TestInteraction();
            TestCollectables();

        }

        private void OnDrawGizmos()
        {
            if (!boxCollider) return;
            Gizmos.DrawWireCube(transform.position, boxCollider.bounds.size);
        }

        private void TestCollectables()
        {
            if (!IsMoving()) return;
            var hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, character.GetDirection(), collisionDistance, collectableLayer);
            var collectable = hit.collider?.GetComponent<IICollectable>();
            if (collectable == null) return;
            collectable.Collect(character);
        }

        private void TestInteraction()
        {
            if (!IsMoving()) return;
            var hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, character.GetDirection(), collisionDistance, interactionLayer);
            var interactive = hit.collider?.GetComponent<IInteractive>();
            if (interactiveFocus != null) interactiveFocus.Disable();
            if (interactive != null) interactive.Enable();
            interactiveFocus = interactive;
        }

        private void ApplyMovement(Vector2 inputVector)
        {
            var movement = character.HasReceivedImpact() ? character.GetReceivedImpact() : new Vector2(inputVector.normalized.x, inputVector.normalized.y);
            if (movement != Vector2.zero)
            {
                character.SetDirection(movement);
                bool moved = character.TryMove(boxCollider, movement, collisionDistance, collisionLayer);
                if (!moved)
                {
                    moved = character.TryMove(boxCollider, new Vector2(movement.x, 0), collisionDistance, collisionLayer);
                }
                if (!moved)
                {
                    character.TryMove(boxCollider, new Vector2(0, movement.y), collisionDistance, collisionLayer);
                }
                if (moved && dustParticles.isStopped)
                {
                    dustParticles.Play();
                }
            }
            else
            {
                // character.SetDirection(Vector2.zero);
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

        private void MainActionStarted()
        {
            if (GameManager.Instance.IsPaused()) return;
            if (TryInteract()) return;
            PrepareSkill(mainSkill);
        }

        public void PrepareSkill(CharacterSkill skill)
        {
            if (GameManager.Instance.IsPaused()) return;
            if (lockControls) return;
            skill.Prepare();
        }

        public void ExecuteSkill(CharacterSkill skill)
        {
            if (GameManager.Instance.IsPaused()) return;
            if (!skill.TryExecute()) return;
            if (!skill.NeedToLockControls()) return;
            LockControls();
        }

        public void ReleaseSkill(CharacterSkill skill)
        {
            if (GameManager.Instance.IsPaused()) return;
            if (lockControls) return;
            skill.Release();
        }

        public void SkillEnded(CharacterSkill skill)
        {
            if (!skill.NeedToLockControls()) return;
            UnlockControls();
        }

        public bool TryInteract()
        {
            if (interactiveFocus == null) return false;
            interactiveFocus.Interact();
            return true;
        }

        public bool IsMoving()
        {
            return character.GetDirection() != Vector2.zero && !lockControls;
        }

        public void LockControls()
        {
            lockControls = true;
        }
        public void UnlockControls()
        {
            lockControls = false;
        }

        public CharacterSkill GetMainSkill()
        {
            return mainSkill;
        }
        public CharacterSkill GetAuxiliarySkill()
        {
            return auxSkill;
        }
        public CharacterSkill GetAvailableSkillAt(int index)
        {
            return availableSkills[index];
        }
        public List<CharacterSkill> GetAvailableSkills()
        {
            return availableSkills;
        }
        public int GetAvailableSkillIndex(CharacterSkill skill)
        {
            return availableSkills.IndexOf(skill);
        }

        public void SetMainSkill(CharacterSkill skill)
        {
            mainSkill = skill;
        }
        public void SetAuxiliarySkill(CharacterSkill skill)
        {
            auxSkill = skill;
        }
    }
}
