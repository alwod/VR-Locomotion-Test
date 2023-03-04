using UnityEngine;

public class PlayWalkingSoundEffect : MonoBehaviour
{
    private Vector3 _previousPlayerPosition;
    
    private AudioSource _soundEffect;
    private void Start()
    {
        _previousPlayerPosition = transform.position;
        _soundEffect = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        // Calculate speed of the player
        var position = transform.position;
        var speed = (position - _previousPlayerPosition).magnitude / Time.deltaTime;
        _previousPlayerPosition = position;
        // Dont bother storing minuscule speeds
        if (speed > 1.0f)
        {
            _soundEffect.Play();
        }
    }
}
