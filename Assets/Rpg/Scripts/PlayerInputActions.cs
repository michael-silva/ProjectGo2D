//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Rpg/Scripts/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ProjectGo2D.Rpg
{
    public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""701b4bb2-5546-46fa-99ba-3a002db6aef5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9baf8b87-6580-4d3e-81aa-efbd8a039b3d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MainAction"",
                    ""type"": ""Button"",
                    ""id"": ""e9fb6342-bad1-4f47-b88f-05dd565b2059"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondAction"",
                    ""type"": ""Button"",
                    ""id"": ""03357fe6-2dbb-4f81-a54e-8938e2ee9cc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""67d8f7e8-d8a6-41f7-91ba-412aaf3f61c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AWSD"",
                    ""id"": ""93bb6c3d-5d78-4199-bb30-66f1e4d6625d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f391ea86-9cf1-4cb7-b3aa-d313bd2a0d97"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1f95ff0c-2648-493f-bd68-1841baad0552"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3a14c0d9-7aee-4cf1-954e-3a9e9c6eb2a1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""46dd8d2f-35a8-4a52-a79b-5d96db69f8d2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3050bf96-6d7a-4d6c-a716-2c434f4e85a8"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fc1f2ee-2d3e-43d5-922f-7802f4de48c9"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee64e7f9-579f-45bd-84ed-deef8951ab1e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35e80884-44a8-41eb-8ba0-60163d24b866"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""b9774bba-89c5-4b91-9668-ae9216e81333"",
            ""actions"": [
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""476b5fb5-62d7-4fec-9a5b-9bc8564df74b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""8995dc66-2fad-447b-90f4-9d6f40b98b40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=1)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""59d9229e-5fd4-4c68-94c9-ffdc8706f643"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Prev"",
                    ""type"": ""Button"",
                    ""id"": ""39158a19-e97e-4130-93a6-f26a597b89e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6db1b83f-92a3-4037-93ed-dee9dbd2d126"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""10398198-a4b6-4774-9392-f2672caa1b98"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""AWSD"",
                    ""id"": ""ee3852ff-9abb-433e-a8a5-7903e7620508"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b53beaff-c7cb-4f1d-9fbc-9ac5998c7918"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aa661150-e0af-417c-8cdb-20156416233b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9d314a77-84df-42d6-b67c-7caa07077377"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a8153c94-f499-4a76-bad3-bd082cfdd0b8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0e3ea100-0380-4dbb-a12b-197f0d3d480e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""616fd7df-3854-405d-a041-4108ca9f6080"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58efafbd-05be-438c-bcfb-9a0624f739db"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aeba0016-e519-4d94-b09a-18d2387ffbc3"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
            m_Player_MainAction = m_Player.FindAction("MainAction", throwIfNotFound: true);
            m_Player_SecondAction = m_Player.FindAction("SecondAction", throwIfNotFound: true);
            m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Next = m_UI.FindAction("Next", throwIfNotFound: true);
            m_UI_Confirm = m_UI.FindAction("Confirm", throwIfNotFound: true);
            m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
            m_UI_Prev = m_UI.FindAction("Prev", throwIfNotFound: true);
            m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Player
        private readonly InputActionMap m_Player;
        private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
        private readonly InputAction m_Player_Move;
        private readonly InputAction m_Player_MainAction;
        private readonly InputAction m_Player_SecondAction;
        private readonly InputAction m_Player_Pause;
        public struct PlayerActions
        {
            private @PlayerInputActions m_Wrapper;
            public PlayerActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Player_Move;
            public InputAction @MainAction => m_Wrapper.m_Player_MainAction;
            public InputAction @SecondAction => m_Wrapper.m_Player_SecondAction;
            public InputAction @Pause => m_Wrapper.m_Player_Pause;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void AddCallbacks(IPlayerActions instance)
            {
                if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MainAction.started += instance.OnMainAction;
                @MainAction.performed += instance.OnMainAction;
                @MainAction.canceled += instance.OnMainAction;
                @SecondAction.started += instance.OnSecondAction;
                @SecondAction.performed += instance.OnSecondAction;
                @SecondAction.canceled += instance.OnSecondAction;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }

            private void UnregisterCallbacks(IPlayerActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @MainAction.started -= instance.OnMainAction;
                @MainAction.performed -= instance.OnMainAction;
                @MainAction.canceled -= instance.OnMainAction;
                @SecondAction.started -= instance.OnSecondAction;
                @SecondAction.performed -= instance.OnSecondAction;
                @SecondAction.canceled -= instance.OnSecondAction;
                @Pause.started -= instance.OnPause;
                @Pause.performed -= instance.OnPause;
                @Pause.canceled -= instance.OnPause;
            }

            public void RemoveCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IPlayerActions instance)
            {
                foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
        private readonly InputAction m_UI_Next;
        private readonly InputAction m_UI_Confirm;
        private readonly InputAction m_UI_Pause;
        private readonly InputAction m_UI_Prev;
        private readonly InputAction m_UI_Move;
        public struct UIActions
        {
            private @PlayerInputActions m_Wrapper;
            public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Next => m_Wrapper.m_UI_Next;
            public InputAction @Confirm => m_Wrapper.m_UI_Confirm;
            public InputAction @Pause => m_Wrapper.m_UI_Pause;
            public InputAction @Prev => m_Wrapper.m_UI_Prev;
            public InputAction @Move => m_Wrapper.m_UI_Move;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void AddCallbacks(IUIActions instance)
            {
                if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Prev.started += instance.OnPrev;
                @Prev.performed += instance.OnPrev;
                @Prev.canceled += instance.OnPrev;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }

            private void UnregisterCallbacks(IUIActions instance)
            {
                @Next.started -= instance.OnNext;
                @Next.performed -= instance.OnNext;
                @Next.canceled -= instance.OnNext;
                @Confirm.started -= instance.OnConfirm;
                @Confirm.performed -= instance.OnConfirm;
                @Confirm.canceled -= instance.OnConfirm;
                @Pause.started -= instance.OnPause;
                @Pause.performed -= instance.OnPause;
                @Pause.canceled -= instance.OnPause;
                @Prev.started -= instance.OnPrev;
                @Prev.performed -= instance.OnPrev;
                @Prev.canceled -= instance.OnPrev;
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
            }

            public void RemoveCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IUIActions instance)
            {
                foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IPlayerActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnMainAction(InputAction.CallbackContext context);
            void OnSecondAction(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnNext(InputAction.CallbackContext context);
            void OnConfirm(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
            void OnPrev(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
    }
}
