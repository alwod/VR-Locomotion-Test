using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayWalkingSoundEffect : MonoBehaviour
{
    private AudioSource _soundEffect;

    [SerializeField] private InputActionProperty leftJoystick;
    [SerializeField] private InputActionProperty rightJoystick;

    [SerializeField] private InputActionProperty leftActivate;
    [SerializeField] private InputActionProperty rightActivate;
    
    [SerializeField] private InputActionProperty leftCancel;
    [SerializeField] private InputActionProperty rightCancel;
    
    private enum LocomotionType
    {
        Teleport,
        Joystick,
        Armswing
    }
    
    private LocomotionType _locomotion;
    
    private void Start()
    {
        _soundEffect = GetComponent<AudioSource>();
        
        if (gameObject.GetComponent<ActionBasedContinuousMoveProvider>().enabled)
        {
            _locomotion = LocomotionType.Joystick;
        }
        else if (gameObject.GetComponent<TeleportationProvider>().enabled)
        {
            _locomotion = LocomotionType.Teleport;
        }
        else if (gameObject.GetComponent<ArmswingMoveProvider>().enabled)
        {
            _locomotion = LocomotionType.Armswing;
        }
    }
    
    private void Update()
    {
        switch (_locomotion)
        {
            case LocomotionType.Joystick when leftJoystick.action.ReadValue<Vector2>() != Vector2.zero || rightJoystick.action.ReadValue<Vector2>() != Vector2.zero:
                _soundEffect.enabled = true;
                GameManager.instance.CalculatePlayerSpeed();
                break;
            case LocomotionType.Joystick:
                _soundEffect.enabled = false;
                break;
            case LocomotionType.Teleport when leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f || rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f:
                _soundEffect.enabled = true;
                GameManager.instance.CalculatePlayerSpeed();
                break;
            case LocomotionType.Teleport:
                _soundEffect.enabled = false;
                break;
            case LocomotionType.Armswing when leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f || rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f:
                _soundEffect.enabled = true;
                GameManager.instance.CalculatePlayerSpeed();
                break;
            case LocomotionType.Armswing:
                _soundEffect.enabled = false;
                break;
            default: 
                _soundEffect.enabled = false;
                break;
        }
    }
}
