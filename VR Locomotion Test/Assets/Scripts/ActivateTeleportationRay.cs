using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportation;
    [SerializeField] private GameObject rightTeleportation;

    [SerializeField] private InputActionProperty leftActivate;
    [SerializeField] private InputActionProperty rightActivate;

    [SerializeField] private InputActionProperty leftCancel;
    [SerializeField] private InputActionProperty rightCancel;

    private void Update()
    {
        leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
