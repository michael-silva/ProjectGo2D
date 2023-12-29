using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class FallingPlatform : MonoBehaviour
    {
        const int GAME_OVER_LAYER = 9;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float fallingDelay;
        private float fallingTimer;
        private bool fallingActivate = false;
        private BoxCollider2D boxCollider;
        private TargetJoint2D joint2d;

        // Start is called before the first frame update
        void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            joint2d = GetComponent<TargetJoint2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!fallingActivate) return;
            fallingTimer -= Time.deltaTime;
            if (fallingTimer < fallingDelay / 2)
            {
                animator.SetBool("Off", true);
            }
            if (fallingTimer <= 0)
            {
                joint2d.enabled = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                fallingActivate = true;
                fallingTimer = fallingDelay;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == GAME_OVER_LAYER)
            {
                Destroy(gameObject);
            }
        }
    }
}
