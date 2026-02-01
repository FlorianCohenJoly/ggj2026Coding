using UnityEngine;

public class OverlayColorSystem : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] public Material visionMaterial;
    [SerializeField] public bool isVisionActive;

    void Update()
    {
        if (visionMaterial != null)
        {

            float intensity = isVisionActive ? 1f : 0f;

            visionMaterial.SetFloat("_Intensity", intensity);
        }
    }
}
