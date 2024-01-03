using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectGo2D.Shared;

namespace ProjectGo2D.Platformer
{
    public interface IEnemy
    {
        void Kill();
    }

    public class Enemy : MonoBehaviour, IEnemy
    {
        private BoxCollider2D boxCollider;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask wallsLayer;
        [SerializeField] private float speed;
        [SerializeField] private Animator animator;
        [SerializeField] private float gravityScale = 1;
        [SerializeField] private int direction = 1;
        [SerializeField] private bool isMoving;
        [SerializeField, ReadOnly] private Vector2 velocity;
        [SerializeField, ReadOnly] private bool isGrounded;
        [SerializeField, ReadOnly] private bool isOnWall;
        [SerializeField, ReadOnly] private bool isFalling;
        [SerializeField, ReadOnly] private bool isDead;

        // Start is called before the first frame update
        void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) return;
            if (isMoving)
            {
                velocity.x = speed * direction;
            }
            GroundChecking();
            WallChecking();
            animator.SetBool("IsWalking", velocity.x != 0);
            transform.Translate(velocity * Time.deltaTime);
            velocity.x = 0;
        }

        private void OnDrawGizmos()
        {

            float xMovement = Mathf.Abs(velocity.x) * Time.deltaTime;
            var offset = new Vector3(0, 0.1f, 0);

            if (boxCollider != null)
            {

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size * 0.9f);
            }
        }

        public void Kill()
        {
            isDead = true;
            animator.SetBool("Hit", true);
            boxCollider.enabled = false;
            Destroy(gameObject, 0.25f);
        }

        private void GroundChecking()
        {
            if (!isGrounded)
            {
                velocity.y += Physics2D.gravity.y * gravityScale * Time.deltaTime;
            }
            isFalling = velocity.y < 0;
            if (!isGrounded && !isFalling)
            {
                return;
            }

            float yMovement = Mathf.Abs(velocity.y) * Time.deltaTime;
            var hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, yMovement, groundLayer);

            if (isFalling && hit.collider != null)
            {
                isGrounded = true;
                // float bottomPoint = boxCollider.bounds.center.y - (boxCollider.bounds.size.y / 2);
                // float hitPoint = hit.point.y + (boxCollider.bounds.size.y / 2);
                // transform.position = new Vector2(transform.position.x, hitPoint);
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
            var hits = Physics2D.BoxCastAll(boxCollider.bounds.center, boxCollider.size * 0.8f, 0, direction == 1 ? Vector2.right : Vector2.left, xMovement, wallsLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                isOnWall = hits[i].collider != null && hits[i].collider.gameObject != gameObject;
                if (!isOnWall) continue;
                InvertDirection();
            }
        }

        private void InvertDirection()
        {
            direction *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}
