using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public JumpAbility jump;
    public DashAbility dash;
    public ShrinkAbility shrink;

    public void EquipMask(MaskType mask)
    {
        // Tout désactiver
        jump.enabledAbility = false;
        dash.enabledAbility = false;
        shrink.enabledAbility = false;
        shrink.Disable();

        // Activer le bon
        switch (mask)
        {
            case MaskType.Jump:
                jump.enabledAbility = true;
                break;
            case MaskType.Dash:
                dash.enabledAbility = true;
                break;
            case MaskType.Shrink:
                shrink.enabledAbility = true;
                break;
        }
    }
}
