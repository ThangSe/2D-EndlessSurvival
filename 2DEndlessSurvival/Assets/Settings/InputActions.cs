//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/InputActions.inputactions
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

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""4660fbe4-ea30-4bae-ac2c-7831feaf8c09"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""08fe7318-0946-48d6-94ba-d7e49bfc1bd8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""46ca2a50-4af9-4e39-b4ad-96ba2c5f651f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.2,pressPoint=0.2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""99a3d43e-ebd1-4556-8740-775a03199709"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""969f380b-4bdf-440a-ba84-c3d2409bda25"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RangeAttack"",
                    ""type"": ""Button"",
                    ""id"": ""0b3e7545-ff73-4915-bc41-52f4c765a4b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""7843d92d-2d14-400a-88f0-9b5ab91cdc42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CastingAttack"",
                    ""type"": ""Button"",
                    ""id"": ""ace795cb-33eb-4f27-80ee-d26c046b0a08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""919a3e01-6a81-46da-b18b-f441cd429974"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseItem1"",
                    ""type"": ""Button"",
                    ""id"": ""22d03308-2b5b-42ba-8547-e0c1a89a3383"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UseItem2"",
                    ""type"": ""Button"",
                    ""id"": ""18468305-bd78-48bc-bcdc-e20237ed36b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UseItem3"",
                    ""type"": ""Button"",
                    ""id"": ""b844f872-0c95-45aa-a0dc-8e2d03df77df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UseItem4"",
                    ""type"": ""Button"",
                    ""id"": ""73f54e83-e620-484c-853e-4679d1010885"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""UseItem5"",
                    ""type"": ""Button"",
                    ""id"": ""86a7584e-31c7-4207-aa33-1998225606d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""0e1bff6a-cd8a-4e7d-8ebb-b93110909037"",
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
                    ""id"": ""5bde9104-045f-487b-a5b5-02954d096b6b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8bdf0f66-93ba-4894-a742-0e216c0bcc81"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""28cdd8e0-3351-48f6-9598-a6a12283efa1"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c166144b-0e7c-43cf-a307-0f87ccf22067"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c632c9c1-f8eb-4438-956d-3993eeafa393"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d74f5047-93ab-4f36-b77d-a5955ce23ffb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press,MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d117ea6-f6f6-422a-a64f-4b67762da51c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f1f40b9-b230-4e1d-b5ff-6d12e1327b9e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RangeAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01d434d4-7e00-482a-83de-eeee408fb711"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea04d958-8591-4ade-af2f-815f7f5f95b2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastingAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62885133-5d24-49a8-a896-b2c4ff18d0d8"",
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
                    ""id"": ""59a87e7b-4411-4847-a161-fb5dabcd12b2"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f69763b-1db0-4937-b087-44c7bd7d55e4"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""083b2eb7-650d-45b1-b071-c6156de7e480"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6258cbf7-305f-4694-9af9-f4d5041d66ac"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5289dba6-4271-4e3f-8130-307d5feb702e"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem4"",
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
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Attack1 = m_Player.FindAction("Attack1", throwIfNotFound: true);
        m_Player_RangeAttack = m_Player.FindAction("RangeAttack", throwIfNotFound: true);
        m_Player_Attack2 = m_Player.FindAction("Attack2", throwIfNotFound: true);
        m_Player_CastingAttack = m_Player.FindAction("CastingAttack", throwIfNotFound: true);
        m_Player_Pause = m_Player.FindAction("Pause", throwIfNotFound: true);
        m_Player_UseItem1 = m_Player.FindAction("UseItem1", throwIfNotFound: true);
        m_Player_UseItem2 = m_Player.FindAction("UseItem2", throwIfNotFound: true);
        m_Player_UseItem3 = m_Player.FindAction("UseItem3", throwIfNotFound: true);
        m_Player_UseItem4 = m_Player.FindAction("UseItem4", throwIfNotFound: true);
        m_Player_UseItem5 = m_Player.FindAction("UseItem5", throwIfNotFound: true);
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
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Attack1;
    private readonly InputAction m_Player_RangeAttack;
    private readonly InputAction m_Player_Attack2;
    private readonly InputAction m_Player_CastingAttack;
    private readonly InputAction m_Player_Pause;
    private readonly InputAction m_Player_UseItem1;
    private readonly InputAction m_Player_UseItem2;
    private readonly InputAction m_Player_UseItem3;
    private readonly InputAction m_Player_UseItem4;
    private readonly InputAction m_Player_UseItem5;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Attack1 => m_Wrapper.m_Player_Attack1;
        public InputAction @RangeAttack => m_Wrapper.m_Player_RangeAttack;
        public InputAction @Attack2 => m_Wrapper.m_Player_Attack2;
        public InputAction @CastingAttack => m_Wrapper.m_Player_CastingAttack;
        public InputAction @Pause => m_Wrapper.m_Player_Pause;
        public InputAction @UseItem1 => m_Wrapper.m_Player_UseItem1;
        public InputAction @UseItem2 => m_Wrapper.m_Player_UseItem2;
        public InputAction @UseItem3 => m_Wrapper.m_Player_UseItem3;
        public InputAction @UseItem4 => m_Wrapper.m_Player_UseItem4;
        public InputAction @UseItem5 => m_Wrapper.m_Player_UseItem5;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Attack1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack1;
                @RangeAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRangeAttack;
                @RangeAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRangeAttack;
                @RangeAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRangeAttack;
                @Attack2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack2;
                @CastingAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastingAttack;
                @CastingAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastingAttack;
                @CastingAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastingAttack;
                @Pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @UseItem1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem1;
                @UseItem1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem1;
                @UseItem1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem1;
                @UseItem2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem2;
                @UseItem2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem2;
                @UseItem2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem2;
                @UseItem3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem3;
                @UseItem3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem3;
                @UseItem3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem3;
                @UseItem4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem4;
                @UseItem4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem4;
                @UseItem4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem4;
                @UseItem5.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem5;
                @UseItem5.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem5;
                @UseItem5.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUseItem5;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @RangeAttack.started += instance.OnRangeAttack;
                @RangeAttack.performed += instance.OnRangeAttack;
                @RangeAttack.canceled += instance.OnRangeAttack;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @CastingAttack.started += instance.OnCastingAttack;
                @CastingAttack.performed += instance.OnCastingAttack;
                @CastingAttack.canceled += instance.OnCastingAttack;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @UseItem1.started += instance.OnUseItem1;
                @UseItem1.performed += instance.OnUseItem1;
                @UseItem1.canceled += instance.OnUseItem1;
                @UseItem2.started += instance.OnUseItem2;
                @UseItem2.performed += instance.OnUseItem2;
                @UseItem2.canceled += instance.OnUseItem2;
                @UseItem3.started += instance.OnUseItem3;
                @UseItem3.performed += instance.OnUseItem3;
                @UseItem3.canceled += instance.OnUseItem3;
                @UseItem4.started += instance.OnUseItem4;
                @UseItem4.performed += instance.OnUseItem4;
                @UseItem4.canceled += instance.OnUseItem4;
                @UseItem5.started += instance.OnUseItem5;
                @UseItem5.performed += instance.OnUseItem5;
                @UseItem5.canceled += instance.OnUseItem5;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnRangeAttack(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnCastingAttack(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnUseItem1(InputAction.CallbackContext context);
        void OnUseItem2(InputAction.CallbackContext context);
        void OnUseItem3(InputAction.CallbackContext context);
        void OnUseItem4(InputAction.CallbackContext context);
        void OnUseItem5(InputAction.CallbackContext context);
    }
}
