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

        // This should run if the red part of the target is hit, meaning it's an accurate hit
        if (transform.parent != null)
        {
            // If the red target was hit, only that part of the target will have been disabled, so find the parent and disable that too to prevent a double hit
            transform.parent.gameObject.SetActive(false);
            GameManager.instance.RecordTimeBetweenHits(true);
            return;
        }
        
        GameManager.instance.RecordTimeBetweenHits(false);
    }
}
