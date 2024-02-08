using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class InputManager : MonoBehaviour
    {
        public readonly UnityEvent OnPauseCalled = new UnityEvent();
        public readonly UnityEvent OnNextUICalled = new UnityEvent();
        public readonly UnityEvent OnPrevUICalled = new UnityEvent();
        public readonly UnityEvent OnConfirmUICalled = new UnityEvent();
        public readonly UnityEvent<Vector2> OnMoveUICalled = new UnityEvent<Vector2>();
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
            inputActions.Player.Pause.performed += context => OnPauseCalled.Invoke();
            inputActions.Player.Enable();
            inputActions.UI.Next.performed += context => OnNextUICalled.Invoke();
            inputActions.UI.Prev.performed += context => OnPrevUICalled.Invoke();
            inputActions.UI.Pause.performed += context => OnPauseCalled.Invoke();
            inputActions.UI.Move.performed += context => OnMoveUICalled.Invoke(inputActions.UI.Move.ReadValue<Vector2>());
            inputActions.UI.Confirm.performed += context => OnConfirmUICalled.Invoke();
        }

        void Update()
        {
            if (inputActions.Player.MainAction.WasReleasedThisFrame()) OnActionReleased.Invoke();
            else if (inputActions.Player.MainAction.IsPressed()) OnActionCalled.Invoke();
            if (inputActions.Player.SecondAction.WasReleasedThisFrame()) OnSecondaryActionReleased.Invoke();
            else if (inputActions.Player.SecondAction.IsPressed()) OnSecondaryActionCalled.Invoke();
        }

        public Vector2 GetPlayerMovementVector()
        {
            return inputActions.Player.Move.ReadValue<Vector2>();
        }

        public void EnableUIInputMode()
        {
            inputActions.Player.Disable();
            inputActions.UI.Enable();
        }

        public void EnablePlayerInputMode()
        {
            inputActions.Player.Enable();
            inputActions.UI.Disable();
        }
    }
}
