using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerStats : MonoBehaviour
{
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int maxHealth = 5;

    public Slider xpBar; // Drag your XP UI bar in inspector
    public TMP_Text levelText;


    private WarriorHealth healthScript;

    void Start()
    {
        healthScript = GetComponent<WarriorHealth>();
        UpdateUI();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel += 50; // Increases each level
        maxHealth += 1;

        healthScript.maxHealth = maxHealth;
        healthScript.HealToFull();

        Debug.Log("Leveled up to " + level + "!");
        FindObjectOfType<LevelUpUI>().ShowLevelUpScreen();

        UpdateUI();
    }

    void UpdateUI()
    {
        if (xpBar != null)
        {
            xpBar.maxValue = xpToNextLevel;
            xpBar.value = currentXP;
        }

        if (levelText != null)
        {
            levelText.text = "Level " + level;
        }
    }
}
