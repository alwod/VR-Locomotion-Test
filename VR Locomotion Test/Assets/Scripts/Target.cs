using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager _gameManager;
    
    [SerializeField] private Vector3 position;
    
    void Start()
    {
        position = transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        
        Debug.Log("Hit Target!");
        gameObject.SetActive(false);
        _gameManager.numberOfHitTargets++;
    }
}
