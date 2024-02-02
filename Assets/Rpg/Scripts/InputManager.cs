using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class InputManager : MonoBehaviour
    {
        public readonly UnityEvent OnActionCalled = new UnityEvent();
        public static InputManager Instance { get; private set; }
        [SerializeField] private PlayerController mainCharacter;
        private PlayerInputActions inputActions;


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            inputActions = new PlayerInputActions();
            inputActions.Player.Attack.performed += context => HandlePlayerAction();
            inputActions.Player.Enable();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandlePlayerAction()
        {
            OnActionCalled.Invoke();
            if (Time.timeScale == 0) return;
            if (mainCharacter.TryInteract()) return;
            mainCharacter.StartAttack();
        }

        public Vector2 GetMovementVector()
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }
    }
}
