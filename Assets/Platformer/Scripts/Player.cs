using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private LayerMask itemsLayer = 7;
        [SerializeField]
        private LayerMask GroundLayer = 8;
        [SerializeField]
        private LayerMask GameOverLayer = 9;
        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float walljumpForce;
        [SerializeField]
        private float walljumpInterval;
        private bool isOnGround = false;
        private bool isOnWall = false;
        private float walljumpCooldown;
        private bool isAirJumping = false;
        private bool isDead = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) return;

            GroundChecking();
            WallChecking();

            if (walljumpCooldown >= walljumpInterval)
            {
                var movement = Input.GetAxis("Horizontal");
                var direction = new Vector2(movement, 0);
                Move(direction);

                if (Input.GetButtonDown("Jump"))
                {
                    if (isOnGround || isOnWall)
                    {
                        Jump(jumpForce);
                    }
                    else if (!isAirJumping)
                    {
                        animator.SetBool("InDoubleJump", true);
                        isAirJumping = true;
                        Jump(jumpForce * 1.5f);
                    }
                }
            }
            else
            {
                walljumpCooldown += Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDead) return;
            if (other.gameObject.layer == itemsLayer)
            {
                var collectable = other.GetComponent<ICollectable>();
                collectable.Collect();
            }

            if (other.gameObject.layer == GameOverLayer)
            {
                animator.SetBool("Hit", true);
                isDead = true;
                GameManager.Instance.GameOver();
                Destroy(gameObject, 0.25f);
            }
        }

        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (isDead) return;
        //     if (other.gameObject.layer == GroundLayer)
        //     {
        //         isOnGround = true;
        //         isAirJumping = false;
        //         animator.SetBool("InAir", false);
        //     }
        // }
        // private void OnCollisionExit2D(Collision2D other)
        // {
        //     if (isDead) return;
        //     if (other.gameObject.layer == GroundLayer)
        //     {
        //         isOnGround = false;
        //         animator.SetBool("InAir", true);
        //     }
        // }

        private void GroundChecking()
        {
            var ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
            isOnGround = ray.collider != null;
            if (isOnGround)
            {
                animator.SetBool("InAir", false);
                animator.SetBool("IsJumping", false);
                animator.SetBool("InDoubleJump", false);
                isAirJumping = false;
            }
            else
            {
                animator.SetBool("InAir", true);
                if (IsFalling())
                {
                    animator.SetBool("IsJumping", false);
                    if (isAirJumping)
                    {
                        animator.SetBool("InDoubleJump", false);
                    }
                }
            }
        }

        private void WallChecking()
        {
            if (isOnGround)
            {
                rb.gravityScale = 4;
                isOnWall = false;
                return;
            }
            var ray = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, IsPointingRight() ? Vector2.right : Vector2.left, 0.1f, GroundLayer);
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
                rb.gravityScale = 4;
            }
        }

        public bool IsFalling()
        {
            return rb.velocity.y < 0;
        }

        public void FacingDirection(float dir)
        {
            if (dir > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        public void Move(Vector2 direction)
        {
            float force = direction.x * speed;
            rb.AddForce(new Vector2(force - rb.velocity.x, 0), ForceMode2D.Impulse);
            bool isWalking = direction.x != 0;
            animator.SetBool("IsWalking", isWalking);
            if (!isWalking) return;
            FacingDirection(direction.x);
        }

        public bool IsPointingRight()
        {
            return transform.eulerAngles == Vector3.zero;
        }

        public void Jump(float force)
        {
            float xForce = 0;
            if (isOnWall)
            {
                xForce = IsPointingRight() ? -1 : 1;
                FacingDirection(xForce);
            }
            rb.AddForce(new Vector2(xForce * walljumpCooldown, force - rb.velocity.y), ForceMode2D.Impulse);
            isOnGround = false;
            animator.SetBool("IsJumping", true);
            animator.SetBool("InAir", true);
            // rb.AddForce(new Vector3(0, force, 0), ForceMode2D.Impulse);
        }
    }
}
