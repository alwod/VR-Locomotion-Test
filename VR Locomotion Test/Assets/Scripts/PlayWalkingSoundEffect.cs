using UnityEngine;
using UnityEngine.InputSystem;

public class PlayWalkingSoundEffect : MonoBehaviour
{
    private AudioSource _soundEffect;

    [SerializeField] private InputActionProperty leftJoystick;
    [SerializeField] private InputActionProperty rightJoystick;
    
    private void Start()
    {
        _soundEffect = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (leftJoystick.action.ReadValue<Vector2>() != Vector2.zero || rightJoystick.action.ReadValue<Vector2>() != Vector2.zero)
        {
            _soundEffect.enabled = true;
        }
        else
        {
            _soundEffect.enabled = false;
        }
    }
}
