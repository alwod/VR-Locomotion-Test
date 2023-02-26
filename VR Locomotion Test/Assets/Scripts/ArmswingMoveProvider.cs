using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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


    private void Start()
    {
        _playerPositionPreviousFrame = transform.position;

        _positionPreviousFrameLeftHand = leftHand.transform.position;
        _positionPreviousFrameRightHand = rightHand.transform.position;
    }
    
    private void Update()
    {
        // Position of player
        _playerPositionCurrentFrame = transform.position;
        
        // Get distance the player has moved since last frame
        var playerDistanceMoved = Vector3.Distance(_playerPositionCurrentFrame, _playerPositionPreviousFrame);

        float leftHandSpeed = 0;
        float rightHandSpeed = 0;
        
        if (leftActivate.action.ReadValue<float>() > 0.1f)
        {
            leftHandSpeed = LeftHandSwing(playerDistanceMoved);
        }
        if (rightActivate.action.ReadValue<float>() > 0.1f)
        {
            rightHandSpeed = RightHandSwing(playerDistanceMoved);
        }
        
        // Aggregate hand speed
        _handSpeed = leftHandSpeed + rightHandSpeed;
        
        // Move the player
        //transform.position += playerCamera.forward * (_handSpeed * speedBoost * Time.deltaTime);
        var targetPosition = transform.position + playerCamera.forward;
        transform.position =
            Vector3.Lerp(transform.position, targetPosition, (_handSpeed * speedBoost * Time.deltaTime));
        // Fix Y position
        transform.position =  new Vector3(transform.position.x, _playerPositionCurrentFrame.y, transform.position.z);
        
        // Set previous position of hands for next frame
        _positionPreviousFrameLeftHand = _positionCurrentFrameLeftHand;
        _positionPreviousFrameRightHand = _positionCurrentFrameRightHand;
        // Set player position previous frame
        _playerPositionPreviousFrame = _playerPositionCurrentFrame;




        // // Get forward direction from the center eye camera and set it to the forward direction object
        // var yRotation = mainCamera.transform.eulerAngles.y;
        // //_forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);
        //
        // // Get position of hands
        // _positionCurrentFrameLeftHand = leftHand.transform.position;
        // _positionCurrentFrameRightHand = rightHand.transform.position;
        //
        // // Position of player
        // _playerPositionCurrentFrame = transform.position;
        //
        // // Get distance the hands and player has moved from last frame
        // var playerDistanceMoved = Vector3.Distance(_playerPositionCurrentFrame, _playerPositionPreviousFrame);
        // var leftHandDistanceMoved = Vector3.Distance(_positionPreviousFrameLeftHand, _positionCurrentFrameLeftHand);
        // var rightHandDistanceMoved = Vector3.Distance(_positionPreviousFrameRightHand, _positionCurrentFrameRightHand);
        //
        // // Aggregate to get hand speed
        // handSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandDistanceMoved - playerDistanceMoved));
        //
        // if(Time.timeSinceLevelLoad > 1.0f)
        // {
        //     transform.position += transform.forward * (handSpeed * speed * Time.deltaTime);
        // }
        //
        // // Set previous position of hands for next frame
        // _positionPreviousFrameLeftHand = _positionCurrentFrameLeftHand;
        // _positionPreviousFrameRightHand = _positionCurrentFrameRightHand;
        // // Set player position previous frame
        // _playerPositionPreviousFrame = _playerPositionCurrentFrame;
    }
    
    private float LeftHandSwing(float playerDistanceMoved)
    {
        // Get position of hands
        _positionCurrentFrameLeftHand = leftHand.transform.position;
        
        // Get distance hand and player has moved since last frame
        var leftHandDistanceMoved = Vector3.Distance(_positionPreviousFrameLeftHand, _positionCurrentFrameLeftHand);
        
        // Get hand speed
        var leftHandSpeed = leftHandDistanceMoved - playerDistanceMoved;
        return leftHandSpeed;
    }
    
    private float RightHandSwing(float playerDistanceMoved)
    {
        // Get position of hands
        _positionCurrentFrameRightHand = rightHand.transform.position;
        
        // Get distance hand has moved since last frame
        var rightHandDistanceMoved = Vector3.Distance(_positionPreviousFrameRightHand, _positionCurrentFrameRightHand);
        
        // Get hand speed
        var rightHandSpeed = rightHandDistanceMoved - playerDistanceMoved;
        return rightHandSpeed;
    }
}
