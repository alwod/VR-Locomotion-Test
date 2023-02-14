using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        
        //Debug.Log("Hit Target!");
        gameObject.SetActive(false);
        GameManager.instance.numberOfHitTargets++;
    }
}
