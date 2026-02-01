using UnityEngine;

public class JumpAbility : Mask
{
    public float jumpForce = 6f;
    public PlayerManager controller;
    private Vector3 velocity;

    public override void UseMask()
    {
        Debug.Log("jump used mask");
        controller.Jump(jumpForce);
    }
}
