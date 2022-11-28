//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputActions/PlayerMovement.inputactions
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

public partial class @PlayerMovement : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerMovement()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerMovement"",
    ""maps"": [
        {
            ""name"": ""Player_Action"",
            ""id"": ""61bcc0fa-bd4b-48ad-a35b-65bb58306d29"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f5f96583-3810-425a-821a-fde38c32f02c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""21182778-765e-4307-b336-2935c5b72130"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BlowUp"",
                    ""type"": ""Button"",
                    ""id"": ""ee8ce3b9-de6c-4395-a4ce-bd3f22af36a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""cc6773be-f86c-4207-955e-1bf00f5321cc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2d258b74-587a-4fef-8874-829b8711c4cd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d0fca478-2540-4965-994d-03eadb66d2e5"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9a903e49-a4a0-4d3b-8af7-071b76c938d3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f00cab4b-6c85-4267-a684-2e5bbb9ba99a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""620b9242-4c5c-4dd0-ae46-8d79e5256e9b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""65b9eb66-84ff-45ab-a982-8283e0285796"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""10a6eec3-9e3e-40bf-aab1-29eb871bc0f7"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BlowUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_Action
        m_Player_Action = asset.FindActionMap("Player_Action", throwIfNotFound: true);
        m_Player_Action_Movement = m_Player_Action.FindAction("Movement", throwIfNotFound: true);
        m_Player_Action_Jump = m_Player_Action.FindAction("Jump", throwIfNotFound: true);
        m_Player_Action_BlowUp = m_Player_Action.FindAction("BlowUp", throwIfNotFound: true);
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

    // Player_Action
    private readonly InputActionMap m_Player_Action;
    private IPlayer_ActionActions m_Player_ActionActionsCallbackInterface;
    private readonly InputAction m_Player_Action_Movement;
    private readonly InputAction m_Player_Action_Jump;
    private readonly InputAction m_Player_Action_BlowUp;
    public struct Player_ActionActions
    {
        private @PlayerMovement m_Wrapper;
        public Player_ActionActions(@PlayerMovement wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Action_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Action_Jump;
        public InputAction @BlowUp => m_Wrapper.m_Player_Action_BlowUp;
        public InputActionMap Get() { return m_Wrapper.m_Player_Action; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_ActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_ActionActions instance)
        {
            if (m_Wrapper.m_Player_ActionActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnJump;
                @BlowUp.started -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnBlowUp;
                @BlowUp.performed -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnBlowUp;
                @BlowUp.canceled -= m_Wrapper.m_Player_ActionActionsCallbackInterface.OnBlowUp;
            }
            m_Wrapper.m_Player_ActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @BlowUp.started += instance.OnBlowUp;
                @BlowUp.performed += instance.OnBlowUp;
                @BlowUp.canceled += instance.OnBlowUp;
            }
        }
    }
    public Player_ActionActions @Player_Action => new Player_ActionActions(this);
    public interface IPlayer_ActionActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnBlowUp(InputAction.CallbackContext context);
    }
}
