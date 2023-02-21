using UnityEngine;

public class StartOrFinishLine : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Finish"))
        {
            GameManager.instance.EndTest();
        }
        else
        {
            GameManager.instance.StartTest();
        }
    }
}
