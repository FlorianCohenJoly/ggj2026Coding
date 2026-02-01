using UnityEngine;

public class Respawnable : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform spawnPoint;

    void OnTriggerStay(Collider other)
    {

        if(other.GetComponent<PlayerManager>() != null)
        {
            audioSource.Play();
            other.GetComponent<PlayerManager>().controller.enabled = false;
            other.transform.position = spawnPoint.position;
            other.GetComponent<PlayerManager>().controller.enabled = true;
        }
    }
}
