// Scripts/SkillSystem/SkillManager.cs
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    public bool fireballUnlocked = false;
    public bool dashUnlocked = false;

    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (fireballUnlocked && Input.GetKeyDown(KeyCode.F))
        {
            CastFireball();
        }

        if (dashUnlocked && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    void CastFireball()
    {
        Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
    }

    void Dash()
    {
        // Add dash effect
        Debug.Log("Dashed!");
    }
}
