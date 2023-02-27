using UnityEngine;

public class DebugCamera : MonoBehaviour
{
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;

    [SerializeField] private Transform orientation;

    private float _xRotation;
    private float _yRotation;
    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        // Get mouse input
        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        
        // Stop from 'overturning' camera when looking up or down
        _xRotation = Mathf.Clamp(_xRotation, -90.0f, 90.0f);
        
        // Rotate camera and player orientation
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
