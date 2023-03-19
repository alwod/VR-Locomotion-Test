using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        
        //transform.parent.gameObject.SetActive(false);
        Destroy(other.gameObject);
        Destroy(transform.parent.gameObject);

        // This should run if the red part of the target is hit, meaning it's an accurate hit
        if (transform.CompareTag("Bullseye"))
        {
            // If the red target was hit, only that part of the target will have been disabled, so find the parent and disable that too to prevent a double hit
            GameManager.instance.RecordTimeBetweenHits(true);
            Debug.Log("Accurate!");
            return;
        }
        
        GameManager.instance.RecordTimeBetweenHits(false);
        Debug.Log("Unaccurate!");
    }
}
