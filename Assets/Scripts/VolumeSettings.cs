using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider slider;

    public void SetMusic()
    {
        float volume = slider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
    }
   
}
