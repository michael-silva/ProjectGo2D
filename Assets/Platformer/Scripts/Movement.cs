using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Movement : MonoBehaviour, ICharacter
    {
        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;
        private float defaultGravityScale;

        [SerializeField] private LayerMask GroundLayer;
        [SerializeField] private LayerMask WallLayer;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;
        [SerializeField] private float wallJumpForceX;
        [SerializeField] private float wallJumpForceY;
        [SerializeField] private float wallJumpDuration;
        [SerializeField, ReadOnly] private float wallJumpTimer;
        [SerializeField] private float jumpForce;
        [SerializeField] private float coyoteTime;
        [SerializeField] private int extraJumps;
        [SerializeField, ReadOnly] private float airTimer;
        [SerializeField, ReadOnly] private bool isGrounded = false;
        [SerializeField, ReadOnly] private bool isOnWall = false;
        [SerializeField, ReadOnly] private int jumpCounter;
        [SerializeField, ReadOnly] private bool isDead = false;

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
            UpdateAnimations();
        }

        private void GroundChecking()
        {
            var ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
            isGrounded = ray.collider != null;

            if (isGrounded)
            {
                airTimer = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
            {
                airTimer -= Time.deltaTime;
            }
        }

        private void WallChecking()
        {
            wallJumpTimer += Time.deltaTime;
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
                rb.gravityScale = defaultGravityScale / 3;
                // rb.velocity = Vector2.zero;
            }
            else
            {
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
            if (wallJumpTimer < wallJumpDuration) return;
            float force = direction.x * speed;
            rb.velocity = new Vector2(force, rb.velocity.y);
            if (!IsWalking()) return;
            FacingDirection(direction.x);
        }

        public bool IsOnWall()
        {
            return isOnWall;
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

        public void Jump(float modifier)
        {
            if (jumpCounter <= 0 && airTimer <= 0 && !isOnWall) return;
            if (isOnWall)
            {
                WallJump();
            }
            else
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, modifier * jumpForce);
                }
                else
                {
                    if (airTimer > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    }
                    else
                    {
                        if (jumpCounter > 0)
                        {
                            jumpCounter--;
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        }
                    }
                }
                airTimer = 0;
            }
        }

        public void WallJump()
        {
            float dirX = GetFacingDirection().x * -1;
            FacingDirection(dirX);
            rb.AddForce(new Vector2(dirX * wallJumpForceX, wallJumpForceY), ForceMode2D.Impulse);
            wallJumpTimer = 0;
        }


        public void EndJump()
        {
            if (rb.velocity.y <= 0 && !isOnWall) return;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
    }
}
