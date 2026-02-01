using UnityEngine;

public class ColorMask : Mask
{
    public OverlayColorSystem overlayColorSystem;

    public override void UseMask()
    {
        overlayColorSystem.isVisionActive = !overlayColorSystem.isVisionActive;
    }

    public override void UnUsedMask()
    {
        overlayColorSystem.isVisionActive = true;
    }
}
