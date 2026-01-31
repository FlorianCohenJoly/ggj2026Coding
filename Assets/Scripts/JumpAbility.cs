using UnityEngine;

public class JumpAbility : MonoBehaviour
{
    public float jumpForce = 6f;
    private CharacterController controller;
    private Vector3 velocity;
    public bool enabledAbility;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!enabledAbility) return;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
