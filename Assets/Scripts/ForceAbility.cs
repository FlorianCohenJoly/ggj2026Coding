using UnityEngine;

public class ForceAbility : Mask
{
    public float pushDistance = 2f;
    public float pushForce = 3f;
    public float pushRange = 2f;
    public LayerMask pushableLayer;
    public Transform player;
    public override void UseMask()
    {
        Ray ray = new Ray(player.position + Vector3.up * 0.5f, player.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * pushRange, Color.blue, 0.5f);

        if (Physics.Raycast(ray, out hit, pushRange, pushableLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 pushDir = player.forward;
                rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }
        }
    }
}
