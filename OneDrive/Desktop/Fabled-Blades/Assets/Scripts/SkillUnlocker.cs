// Scripts/SkillSystem/SkillUnlocker.cs
using UnityEngine;

public class SkillUnlocker : MonoBehaviour
{
    public void UnlockFireball()
    {
        SkillManager.Instance.fireballUnlocked = true;
        Debug.Log("Fireball Unlocked!");
    }

    public void UnlockDash()
    {
        SkillManager.Instance.dashUnlocked = true;
        Debug.Log("Dash Unlocked!");
    }
}
