//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Project/Scripts/Gameplay/Input/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerSwipe"",
            ""id"": ""3f4e9f94-29ab-4fe5-9ee2-f7e3a8923e4a"",
            ""actions"": [
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""80d911fb-21d8-40cb-a55b-c915ab95b014"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""a067dfae-926c-42b2-9627-9c4001c4834a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7c8a6ded-27aa-4bdc-9e84-074549f9d2de"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b882d7c7-6fc9-45a6-9059-85867372b831"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerSwipe
        m_PlayerSwipe = asset.FindActionMap("PlayerSwipe", throwIfNotFound: true);
        m_PlayerSwipe_Press = m_PlayerSwipe.FindAction("Press", throwIfNotFound: true);
        m_PlayerSwipe_Position = m_PlayerSwipe.FindAction("Position", throwIfNotFound: true);
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

    // PlayerSwipe
    private readonly InputActionMap m_PlayerSwipe;
    private IPlayerSwipeActions m_PlayerSwipeActionsCallbackInterface;
    private readonly InputAction m_PlayerSwipe_Press;
    private readonly InputAction m_PlayerSwipe_Position;
    public struct PlayerSwipeActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerSwipeActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_PlayerSwipe_Press;
        public InputAction @Position => m_Wrapper.m_PlayerSwipe_Position;
        public InputActionMap Get() { return m_Wrapper.m_PlayerSwipe; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerSwipeActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerSwipeActions instance)
        {
            if (m_Wrapper.m_PlayerSwipeActionsCallbackInterface != null)
            {
                @Press.started -= m_Wrapper.m_PlayerSwipeActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_PlayerSwipeActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_PlayerSwipeActionsCallbackInterface.OnPress;
                @Position.started -= m_Wrapper.m_PlayerSwipeActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_PlayerSwipeActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_PlayerSwipeActionsCallbackInterface.OnPosition;
            }
            m_Wrapper.m_PlayerSwipeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
            }
        }
    }
    public PlayerSwipeActions @PlayerSwipe => new PlayerSwipeActions(this);
    public interface IPlayerSwipeActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
    }
}
