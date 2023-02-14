using UnityEngine;

public class StartingLine : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.StartTest();
    }
}
