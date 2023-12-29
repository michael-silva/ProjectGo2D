using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Player : MonoBehaviour
    {
        const int ITEMS_LAYER = 7;
        const int GROUND_LAYER = 8;
        const int GAME_OVER_LAYER = 9;
        private Rigidbody2D rb;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private int speed;
        [SerializeField]
        private float jumpForce;
        private bool isOnGround = false;
        private bool isAirJumping = false;
        private bool isDead = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead) return;
            var movement = Input.GetAxis("Horizontal");
            if (movement == 0)
            {
                animator.SetBool("IsWalking", false);
            }
            else
            {
                transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
                animator.SetBool("IsWalking", true);
                if (movement > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
            if (!isOnGround)
            {
                bool isFalling = rb.velocity.y < 0.1;
                animator.SetBool("IsJumping", !isFalling);
                if (isFalling) animator.SetBool("InDoubleJump", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (isOnGround)
                {
                    animator.SetBool("IsJumping", true);
                    rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
                }
                else if (!isAirJumping)
                {
                    animator.SetBool("InDoubleJump", true);
                    isAirJumping = true;
                    rb.AddForce(new Vector3(0, jumpForce * 2, 0), ForceMode2D.Impulse);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDead) return;
            if (other.gameObject.layer == ITEMS_LAYER)
            {
                var collectable = other.GetComponent<ICollectable>();
                collectable.Collect();
            }

            if (other.gameObject.layer == GAME_OVER_LAYER)
            {
                animator.SetBool("Hit", true);
                isDead = true;
                GameManager.Instance.ShowGameOver();
                Destroy(gameObject, 0.25f);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (isDead) return;
            if (other.gameObject.layer == GROUND_LAYER)
            {
                isOnGround = true;
                isAirJumping = false;
                animator.SetBool("InAir", false);

            }
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (isDead) return;
            if (other.gameObject.layer == GROUND_LAYER)
            {
                isOnGround = false;
                animator.SetBool("InAir", true);
            }
        }
    }
}
