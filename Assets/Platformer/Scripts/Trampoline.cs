using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{

    public interface IActionable
    {
        void Active(Character character);
    }

    public class Trampoline : MonoBehaviour, IActionable
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
        public void Active(Character character)
        {
            animator.SetTrigger("Jump");
            character.Jump(jumpForce);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("Jump");
                var player = other.gameObject.GetComponent<Movement>();
                player.Jump(jumpForce);
            }
        }
    }
}
