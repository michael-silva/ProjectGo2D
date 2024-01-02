using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ProjectGo2D.Shared;

namespace ProjectGo2D.Platformer
{
    public class Character : MonoBehaviour
    {
        private BoxCollider2D boxCollider;
        [SerializeField] private float speed;
        [SerializeField] private float jumpHeight = 1;
        [SerializeField] private float gravityScale = 1;
        [SerializeField] private float fallGravityScale = 2;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask itemsLayer;
        [SerializeField] private LayerMask gameOverLayer;
        [SerializeField] private Transform bottomFront;
        [SerializeField] private Transform bottomBack;
        [SerializeField] private Transform topFront;
        [SerializeField] private Transform topBack;
        [SerializeField] private Animator animator;
        [SerializeField, ReadOnly] private Vector2 velocity;
        [SerializeField, ReadOnly] private bool isDead;
        [SerializeField, ReadOnly] private bool isGrounded;
        [SerializeField, ReadOnly] private bool isOnWall;
        [SerializeField, ReadOnly] private bool isJumping;
        [SerializeField, ReadOnly] private bool isFalling;
        [SerializeField, ReadOnly] private bool isSecondJump;

        // Start is called before the first frame update
        void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            FacingDirection(velocity.x);
            HandleJump();
            HeadChecking();
            GroundChecking();
            WallChecking();
            UpdateAnimations();
            transform.Translate(velocity * Time.deltaTime);
            velocity.x = 0;
            CollisionChecks();
        }

        private void CollisionChecks()
        {
            if (isDead) return;
            var itemHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.right, 0.1f, itemsLayer);
            if (itemHit.collider != null)
            {
                var collectable = itemHit.collider.GetComponent<ICollectable>();
                collectable.Collect();
            }

            var goHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.right, 0.1f, gameOverLayer);
            if (goHit.collider != null)
            {
                animator.SetBool("Hit", true);
                isDead = true;
                GameManager.Instance.ShowGameOver();
                Destroy(gameObject, 0.25f);
            }
        }

        private void UpdateAnimations()
        {
            animator.SetBool("InAir", !isGrounded);
            animator.SetBool("IsJumping", isJumping && !isFalling);
            animator.SetBool("InDoubleJump", isSecondJump && !isFalling);
            animator.SetBool("IsWalking", velocity.x != 0);
        }

        private void HandleJump()
        {
            isFalling = velocity.y < 0;
            if (!isGrounded)
            {
                velocity += GetGravity() * Time.deltaTime;
            }
            else if (velocity.y == 0)
            {
                isJumping = false;
                isSecondJump = false;
            }
            // if (isJumping && isFalling)
            // {
            //     isJumping = false;
            // }
        }

        private Vector2 GetGravity()
        {
            float scale = !isJumping ? fallGravityScale : gravityScale;
            return Physics2D.gravity * scale;
        }

        private void OnDrawGizmos()
        {
            GUI.color = Color.black;
            Handles.Label(transform.position, velocity.ToString());

            float xMovement = Mathf.Abs(velocity.x) * Time.deltaTime;
            var offset = new Vector3(0, 0.1f, 0);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(bottomFront.position + offset, bottomFront.position + offset + new Vector3(xMovement, 0, 0));
        }

        private void HeadChecking()
        {
            if (isGrounded || isFalling)
                return;

            float yMovement = Mathf.Abs(velocity.y) * Time.deltaTime;
            var front = Physics2D.Raycast(topFront.position, Vector2.up, yMovement, groundLayer);
            var back = Physics2D.Raycast(topBack.position, Vector2.up, yMovement, groundLayer);
            var hit = front.collider != null ? front : back;
            if (hit.collider != null)
            {
                velocity.y = 0;
            }
        }


        private void GroundChecking()
        {
            if (!isGrounded && !isFalling)
                return;

            float yMovement = Mathf.Abs(velocity.y) * Time.deltaTime;
            var front = Physics2D.Raycast(bottomFront.position, Vector2.down, yMovement, groundLayer);
            var back = Physics2D.Raycast(bottomBack.position, Vector2.down, yMovement, groundLayer);
            var hit = front.collider != null ? front : back;
            if (isFalling && hit.collider != null)
            {
                isGrounded = true;
                float bottomPoint = transform.position.y - bottomFront.position.y;
                float hitPoint = hit.point.y + bottomPoint;
                transform.position = new Vector2(transform.position.x, hitPoint);
                velocity.y = 0;
            }
            if (isGrounded && hit.collider == null)
            {
                isGrounded = false;
            }
        }

        private void WallChecking()
        {
            float xMovement = Mathf.Abs(velocity.x) * Time.deltaTime;
            var direction = new Vector2(GetFacingDirection() * xMovement, 0);
            var offset = new Vector3(0, 0.1f, 0);
            isOnWall = Physics2D.Raycast(bottomFront.position + offset, direction, xMovement, groundLayer);

            if (isOnWall)
            {
                velocity.x = 0;
            }
            // Wall jump
            // if (!isGrounded && isOnWall && xMovement > 0)
            // {
            //     velocity.y = 0;
            // }
        }

        private void FacingDirection(float direction)
        {
            if (direction == 0) return;
            var currentFacing = GetFacingDirection();
            var newFacingDirection = direction > 0 ? 1 : -1;
            if (currentFacing != newFacingDirection)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }

        public int GetFacingDirection()
        {
            return transform.localScale.x < 0 ? -1 : 1;
        }

        public void Move(Vector2 direction)
        {
            velocity += direction * speed;
        }

        public void Jump(float modifier = 1)
        {
            if (isSecondJump) return;

            // character.OnJumpStart.Invoke(0);
            if (isJumping)
            {
                isSecondJump = true;
            }
            else
            {
                isJumping = true;
            }
            float jumpForce = Mathf.Sqrt(jumpHeight * GetGravity().y * -2);
            velocity.y = jumpForce * modifier;

        }
    }
}
