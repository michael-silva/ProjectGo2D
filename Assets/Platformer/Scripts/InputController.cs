using System.Collections;
using System.Collections.Generic;
using ProjectGo2D.Shared;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class InputController : MonoBehaviour
    {
        private ICharacter character;
        private Shooter shooter;
        [SerializeField] private float walljumpInterval;
        [SerializeField, ReadOnly] private float walljumpCooldown;
        // [SerializeField, ReadOnly] private bool isJumpPressed;
        // private float jumpTimer = 0;
        // private float doubleJumpDelay = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<ICharacter>();
            shooter = GetComponent<Shooter>();
            walljumpCooldown = walljumpInterval;
        }

        // Update is called once per frame
        // void Update()
        // {
        //     var xaxis = Input.GetAxis("Horizontal");
        //     character.Move(new Vector2(xaxis, 0));

        //     if (Input.GetButton("Jump"))
        //     {
        //         if (!isJumpPressed)
        //         {
        //             isJumpPressed = true;
        //             jumpTimer = doubleJumpDelay;
        //             character.Jump();
        //         }
        //     }
        //     else if (isJumpPressed)
        //     {
        //         jumpTimer -= Time.deltaTime;
        //         if (jumpTimer <= 0)
        //         {
        //             isJumpPressed = false;
        //         }
        //     }
        // }

        private void Update()
        {
            if (walljumpCooldown >= walljumpInterval)
            {
                var movement = Input.GetAxis("Horizontal");
                var direction = new Vector2(movement, 0);
                character.Move(direction);

                if (Input.GetButtonDown("Jump"))
                {
                    character.Jump();
                    if (character.IsOnWall())
                    {
                        walljumpCooldown = 0;
                    }
                }
            }
            else
            {
                walljumpCooldown += Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                shooter.StartCharging();
            }
            if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                shooter.ReleaseShoot();
            }
        }
    }
}
