using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayTeleportation()
    {
        Debug.Log("Playing with Teleportation!");
        SceneManager.LoadScene("Teleport Test");
    }

    public void PlayJoystick()
    {
        Debug.Log("Playing with Continuous Joystick!");
        SceneManager.LoadScene("Joystick Test");
    }

    public void PlayArmswinger()
    {
        Debug.Log("Playing with Armswinger!");
        SceneManager.LoadScene("Armswing Test");
    }
}
