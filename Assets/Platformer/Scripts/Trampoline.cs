using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float jumpForce;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("Jump");
                var player = other.gameObject.GetComponent<Player>();
                player.Jump(jumpForce);
            }
        }
    }
}
