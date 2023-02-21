using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GetLocomotionType : MonoBehaviour
{
    // Input actions to activate teleportation and armswinger
    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;
    
    // Input actions to activate continuous joystick movement
    public InputActionProperty leftJoystick;
    public InputActionProperty rightJoystick;

    private enum LocomotionType
    {
        Teleport,
        Joystick,
        Armswing
    }

    private LocomotionType _locomotion;
    
    private void Start()
    {
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
        var test = leftActivate.action.ReadValue<float>() > 0.1f;
        // Joystick
        if (leftActivate.action.ReadValue<float>() > 0.1f || 
            rightActivate.action.ReadValue<float>() > 0.1f || 
            leftJoystick.action.triggered || 
            rightJoystick.action.triggered)
        {
            GameManager.instance.CalculatePlayerSpeed();
        }
    }
}
