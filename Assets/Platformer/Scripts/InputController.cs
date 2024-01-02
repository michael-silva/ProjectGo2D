using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class InputController : MonoBehaviour
    {
        private Character character;
        [SerializeField, ReadOnly] private bool isJumpPressed;
        private float jumpTimer = 0;
        private float doubleJumpDelay = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<Character>();

        }

        // Update is called once per frame
        void Update()
        {
            var xaxis = Input.GetAxis("Horizontal");
            character.Move(new Vector2(xaxis, 0));

            if (Input.GetButton("Jump"))
            {
                if (!isJumpPressed)
                {
                    isJumpPressed = true;
                    jumpTimer = doubleJumpDelay;
                    character.Jump();
                }
            }
            else if (isJumpPressed)
            {
                jumpTimer -= Time.deltaTime;
                if (jumpTimer <= 0)
                {
                    isJumpPressed = false;
                }
            }
        }
    }
}
