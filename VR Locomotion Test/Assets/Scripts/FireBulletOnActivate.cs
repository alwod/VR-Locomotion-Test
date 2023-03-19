using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireSpeed = 10;

    private AudioSource _soundEffect;
    
    private void Start()
    {
        var grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);

        _soundEffect = GetComponent<AudioSource>();
    }

    private void FireBullet(ActivateEventArgs arg)
    {
        var spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        _soundEffect.Play();
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);
    }
}
