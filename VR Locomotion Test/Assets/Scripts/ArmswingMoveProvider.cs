using UnityEngine;

public class ArmswingMoveProvider : MonoBehaviour
{
    // Game objects
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject mainCamera;
    private GameObject _forwardDirection;
    
    // Vector 3 positions
    private Vector3 _positionPreviousFrameLeftHand;
    private Vector3 _positionCurrentFrameLeftHand;
    
    private Vector3 _positionPreviousFrameRightHand;
    private Vector3 _positionCurrentFrameRightHand;
    
    private Vector3 _playerPositionPreviousFrame;
    private Vector3 _playerPositionCurrentFrame;
    
    // Speed
    [SerializeField] private float speed = 70;
    [SerializeField] private float handSpeed = 1;
    
    
    void Start()
    {
        _playerPositionPreviousFrame = transform.position;

        _positionPreviousFrameLeftHand = leftHand.transform.position;
        _positionPreviousFrameRightHand = rightHand.transform.position;
    }
    
    void Update()
    {
        // Get forward direction from the center eye camera and set it to the forward direction object
        var yRotation = mainCamera.transform.eulerAngles.y;
        _forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);
        
        // Get position of hands
        _positionCurrentFrameLeftHand = leftHand.transform.position;
        _positionCurrentFrameRightHand = rightHand.transform.position;

        // Position of player
        _playerPositionCurrentFrame = transform.position;

        // Get distance the hands and player has moved from last frame
        var playerDistanceMoved = Vector3.Distance(_playerPositionCurrentFrame, _playerPositionPreviousFrame);
        var leftHandDistanceMoved = Vector3.Distance(_positionPreviousFrameLeftHand, _positionCurrentFrameLeftHand);
        var rightHandDistanceMoved = Vector3.Distance(_positionPreviousFrameRightHand, _positionCurrentFrameRightHand);

        // Aggregate to get hand speed
        handSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandDistanceMoved - playerDistanceMoved));

        if(Time.timeSinceLevelLoad > 1f)
        {
            transform.position += _forwardDirection.transform.forward * (handSpeed * speed * Time.deltaTime);
        }

        // Set previous position of hands for next frame
        _positionPreviousFrameLeftHand = _positionCurrentFrameLeftHand;
        _positionPreviousFrameRightHand = _positionCurrentFrameRightHand;
        // Set player position previous frame
        _playerPositionPreviousFrame = _playerPositionCurrentFrame;
    }
}
