using UnityEngine;

public class Mask : MonoBehaviour
{

    public string maskName;
    public int value;
    public bool isSelected;
    public AudioSource audioSource;
    public AudioClip useSound;

    public virtual void UseMask()
    {
        if (isSelected)
        {
            if(audioSource && useSound)
                audioSource.PlayOneShot(useSound);
            gameObject.SetActive(true);
        }
    }

    public virtual void UnUsedMask()
    {
        
    }

}
