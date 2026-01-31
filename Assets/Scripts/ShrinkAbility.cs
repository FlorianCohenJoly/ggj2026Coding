using UnityEngine;

public class ShrinkAbility : MonoBehaviour
{
    public float shrinkScale = 0.5f;
    public bool enabledAbility;

    void Update()
    {
        if (!enabledAbility) return;

        transform.localScale = Vector3.one * shrinkScale;
    }

    public void Disable()
    {
        transform.localScale = Vector3.one;
    }
}
