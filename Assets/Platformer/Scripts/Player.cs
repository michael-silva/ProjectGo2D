using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Player : MonoBehaviour, ICharacter
    {
        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;
        private float defaultGravityScale;

        [SerializeField] private LayerMask GroundLayer;
        [SerializeField] private LayerMask WallLayer;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float walljumpForce;
        [SerializeField] private float walljumpInterval;
        private bool isGrounded = false;
        private bool isOnWall = false;
        private float walljumpCooldown;
        private bool isAirJumping = false;
        private bool isDead = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            defaultGravityScale = rb.gravityScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) return;

            GroundChecking();
            WallChecking();

            InputControls();

            UpdateAnimations();
        }

        private void InputControls()
        {
            if (walljumpCooldown >= walljumpInterval)
            {
                var movement = Input.GetAxis("Horizontal");
                var direction = new Vector2(movement, 0);
                Move(direction);

                if (Input.GetButtonDown("Jump"))
                {
                    Jump(jumpForce);
                }
            }
            else
            {
                walljumpCooldown += Time.deltaTime;
            }
        }

        private void GroundChecking()
        {
            var ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
            isGrounded = ray.collider != null;
        }

        private void WallChecking()
        {
            if (isGrounded)
            {
                rb.gravityScale = defaultGravityScale;
                isOnWall = false;
                return;
            }
            var ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, GetFacingDirection(), 0.1f, WallLayer);
            isOnWall = ray.collider != null;
            if (isOnWall)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                walljumpCooldown = 0;
            }
            else
            {
                walljumpCooldown = walljumpInterval;
                rb.gravityScale = defaultGravityScale;
            }
        }

        private void UpdateAnimations()
        {
            animator.SetBool("IsWalking", IsWalking());
            animator.SetBool("IsGrounded", isGrounded);
            // animator.SetBool("IsJumping", true);
            // animator.SetBool("InAir", true);
            // animator.SetBool("InDoubleJump", true);
        }

        public bool CanAttack()
        {
            return !isOnWall;
        }

        public void FacingDirection(float dir)
        {
            if (dir > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void Move(Vector2 direction)
        {
            float force = direction.x * speed;
            rb.velocity = new Vector2(force, rb.velocity.y);
            if (!IsWalking()) return;
            FacingDirection(direction.x);
        }

        public bool IsFalling()
        {
            return rb.velocity.y < 0;
        }

        public bool IsWalking()
        {
            return rb.velocity.x != 0;
        }

        public Vector2 GetFacingDirection()
        {
            return new Vector2(transform.localScale.x, 0);
        }

        public void Jump(float force)
        {
            if (isGrounded)
            {
                isAirJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, force);
            }
            else if (isOnWall)
            {
                walljumpCooldown = 0;
                Move(GetFacingDirection() * -6);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (!isAirJumping)
            {
                isAirJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
}
