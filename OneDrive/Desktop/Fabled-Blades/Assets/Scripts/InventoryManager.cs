using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public WarriorHealth warriorHealth;
    [Header("UI References")]
    public GameObject inventoryUI;
    public TextMeshProUGUI inventoryText;

    private List<InventoryItem> items = new List<InventoryItem>();

    void Awake()
    {
        instance = this;
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // Toggle inventory using the "I" key
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    // Add item to inventory
    public static void PickItem(string itemName)
    {
        InventoryItem newItem = new InventoryItem(itemName);
        instance.items.Add(newItem);
        instance.UpdateInventoryUI();
        Debug.Log($"📦 Picked up: {itemName}");
    }

    // Update the inventory UI text
    void UpdateInventoryUI()
    {
        inventoryText.text = "🎒 Inventory:\n";
        foreach (InventoryItem item in items)
        {
            inventoryText.text += "• " + item.itemName + "\n";
        }
    }

    // Toggle inventory UI
    public void ToggleInventory()
    {
        bool isActive = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isActive);
        if (!isActive) UpdateInventoryUI();
    }

    // Close inventory from UI button
    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    // Use a specific item
    /*public void UseItem(string itemName)
    {
        // Find the first matching item
        InventoryItem itemToUse = items.Find(item => item.itemName == itemName);

        if (itemToUse != null)
        {
            if (itemToUse.itemName == "Healing Potion")
            {
                WarriorHealth warrior = FindObjectOfType<WarriorHealth>();
                if (warrior != null && !warrior.IsDead())
                {
                    warrior.HealToFull();
                    items.Remove(itemToUse); // Remove one potion
                    UpdateInventoryUI();
                    Debug.Log("🧪 Used Healing Potion!");
                }
            }

            // 🧪 Add more usable items here later
        }
        else
        {
            Debug.Log("❌ Item not found in inventory.");
        }
    }*/
    public void UseItem()
    {
        Debug.Log("UseItem clicked!");  // Add this
        if (warriorHealth != null)
        {
            Debug.Log("Calling HealToFull");
            warriorHealth.HealToFull();
        }
        else
        {
            Debug.LogWarning("⚠️ WarriorHealth is NULL in UseItemHandler!");
        }
    }

    // Called by the UI Button
    public void OnUseButtonClick()
    {
        UseItem();  // You can change this later to dynamic item selection
    }



}
