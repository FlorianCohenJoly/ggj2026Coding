using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public float dashDistance = 5f;
    public bool enabledAbility;

    void Update()
    {
        if (!enabledAbility) return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position += transform.forward * dashDistance;
        }
    }
}
