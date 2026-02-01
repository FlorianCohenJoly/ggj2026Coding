using UnityEngine;

public class MinimizeAbility : Mask
{
    public Transform model;
    public float normalHeight = 1.8f;
    public float shrinkHeight = 1.0f;

    public Vector3 normalModelScale = Vector3.one;
    public Vector3 shrinkModelScale = Vector3.one * 0.5f;

    public float normalSpeed = 5f;
    public float shrinkSpeed = 3f;

    public Transform cameraPivot;
    public float normalCameraY = 1.6f;
    public float shrinkCameraY = 1.0f;
    public CharacterController controller;

    private bool isShrunk = false;
    public override void UseMask()
    {
        ToggleShrink();
    }

    public void ToggleShrink()
    {
        if (isShrunk)
            ReturnToNormal();
        else
            Shrink();
    }

    void Shrink()
    {
        isShrunk = true;

        model.localScale = shrinkModelScale;
        controller.height = shrinkHeight;
        controller.center = new Vector3(0, shrinkHeight / 2f, 0);

        Vector3 camPos = cameraPivot.localPosition;
        camPos.y = shrinkCameraY;
        cameraPivot.localPosition = camPos;
    }

    void ReturnToNormal()
    {
        isShrunk = false;

        model.localScale = normalModelScale;
        controller.height = normalHeight;
        controller.center = new Vector3(0, normalHeight / 2f, 0);

        Vector3 camPos = cameraPivot.localPosition;
        camPos.y = normalCameraY;
        cameraPivot.localPosition = camPos;
    }

    public override void UnUsedMask()
    {
        if (isShrunk)
            ReturnToNormal();
    }

}
