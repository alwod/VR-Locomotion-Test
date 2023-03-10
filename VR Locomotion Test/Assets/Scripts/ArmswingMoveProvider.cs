using UnityEngine;
using UnityEngine.InputSystem;

public class ArmswingMoveProvider : MonoBehaviour
{
    // Game objects
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    //[SerializeField] private GameObject mainCamera;
    //private GameObject _forwardDirection;
    
    // Vector 3 positions
    private Vector3 _positionPreviousFrameLeftHand;
    private Vector3 _positionCurrentFrameLeftHand;
    
    private Vector3 _positionPreviousFrameRightHand;
    private Vector3 _positionCurrentFrameRightHand;
    
    private Vector3 _playerPositionPreviousFrame;
    private Vector3 _playerPositionCurrentFrame;

    [SerializeField] private Transform playerCamera;
    
    // Speed
    [SerializeField] private float speedBoost = 1;
    private float _handSpeed;
    
    // Input actions to activate arm swinger
    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;
    
    [SerializeField] private InputActionProperty leftCancel;
    [SerializeField] private InputActionProperty rightCancel;
    
    //private Rigidbody _rigidbody;


    private void Start()
    {
        _playerPositionPreviousFrame = transform.position;

        _positionPreviousFrameLeftHand = leftHand.transform.position;
        _positionPreviousFrameRightHand = rightHand.transform.position;
        
        //_rigidbody = GetComponent<Rigidbody>();
        // Stop rigid body from falling over
        //_rigidbody.freezeRotation = true;
        // Set drag to stop sliding
        //_rigidbody.drag = 5;
    }
    
    private void Update()
    {
        // Position of player
        _playerPositionCurrentFrame = transform.position;
        
        // Get distance the player has moved since last frame
        var playerDistanceMoved = Vector3.Distance(_playerPositionCurrentFrame, _playerPositionPreviousFrame);

        float leftHandSpeed = 0;
        float rightHandSpeed = 0;
        
        // Get position of hands
        _positionCurrentFrameLeftHand = leftHand.transform.position;
        _positionCurrentFrameRightHand = rightHand.transform.position;
        
        if (leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f)
        {
            leftHandSpeed = LeftHandSwing(playerDistanceMoved);
        }
        if (rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f)
        {
            rightHandSpeed = RightHandSwing(playerDistanceMoved);
        }
        
        // Aggregate hand speed
        _handSpeed = leftHandSpeed + rightHandSpeed;
        
        //TODO FIGURE OUT HOW TO STOP HAVING PLAYER MOVE THROUGH WALLS, MAYBE USE RIGIDBODY MOVEMENT INSTEAD?
        // Move the player
        //transform.position += playerCamera.forward * (_handSpeed * speedBoost * Time.deltaTime);
        var targetPosition = transform.position + playerCamera.forward;
        transform.position =
            Vector3.Lerp(transform.position, targetPosition, (_handSpeed * speedBoost * Time.deltaTime));
        // Fix Y position
        transform.position =  new Vector3(transform.position.x, _playerPositionCurrentFrame.y, transform.position.z);
        
        //_rigidbody.AddForce(playerCamera.forward * (_handSpeed * speedBoost * Time.deltaTime), ForceMode.Force);
        
        
        
        // Set previous position of hands for next frame
        _positionPreviousFrameLeftHand = _positionCurrentFrameLeftHand;
        _positionPreviousFrameRightHand = _positionCurrentFrameRightHand;
        // Set player position previous frame
        _playerPositionPreviousFrame = _playerPositionCurrentFrame;
    }
    
    private float LeftHandSwing(float playerDistanceMoved)
    {
        // Get distance hand and player has moved since last frame
        var leftHandDistanceMoved = Vector3.Distance(_positionPreviousFrameLeftHand, _positionCurrentFrameLeftHand);
        
        // Get hand speed
        var leftHandSpeed = leftHandDistanceMoved - playerDistanceMoved;
        return leftHandSpeed;
    }
    
    private float RightHandSwing(float playerDistanceMoved)
    {
        // Get distance hand has moved since last frame
        var rightHandDistanceMoved = Vector3.Distance(_positionPreviousFrameRightHand, _positionCurrentFrameRightHand);
        
        // Get hand speed
        var rightHandSpeed = rightHandDistanceMoved - playerDistanceMoved;
        return rightHandSpeed;
    }
}
