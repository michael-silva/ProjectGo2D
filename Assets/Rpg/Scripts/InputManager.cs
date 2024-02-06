using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class InputManager : MonoBehaviour
    {
        public readonly UnityEvent OnSecondaryActionStarted = new UnityEvent();
        public readonly UnityEvent OnSecondaryActionCalled = new UnityEvent();
        public readonly UnityEvent OnSecondaryActionReleased = new UnityEvent();
        public readonly UnityEvent OnActionStarted = new UnityEvent();
        public readonly UnityEvent OnActionCalled = new UnityEvent();
        public readonly UnityEvent OnActionReleased = new UnityEvent();
        public static InputManager Instance { get; private set; }
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
            inputActions.Player.MainAction.started += context => OnActionStarted.Invoke();
            inputActions.Player.SecondAction.started += context => OnSecondaryActionStarted.Invoke();
            inputActions.Player.Enable();
        }

        void Update()
        {
            if (inputActions.Player.MainAction.WasReleasedThisFrame()) OnActionReleased.Invoke();
            else if (inputActions.Player.MainAction.IsPressed()) OnActionCalled.Invoke();
            if (inputActions.Player.SecondAction.WasReleasedThisFrame()) OnSecondaryActionReleased.Invoke();
            else if (inputActions.Player.SecondAction.IsPressed()) OnSecondaryActionCalled.Invoke();
        }

        public Vector2 GetMovementVector()
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }
    }
}
