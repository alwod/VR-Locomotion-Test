using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeGunHandedness : MonoBehaviour
{
    [SerializeField] private InputActionProperty leftActivate;
    [SerializeField] private InputActionProperty rightActivate;

    private Vector3 _rightHandPosition;
    private Vector3 _leftHandPosition;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _rightHandPosition = transform.position;
        _leftHandPosition = new Vector3(-_rightHandPosition.x, _rightHandPosition.y, _rightHandPosition.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if (leftActivate.action.ReadValue<float>() > 0.1)
        {
            transform.position = _leftHandPosition;
        }

        if (rightActivate.action.ReadValue<float>() > 0.1)
        {
            transform.position = _rightHandPosition;
        }
    }
}
