using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Magic Scroll";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.PickItem(itemName);
            Destroy(gameObject);
        }
    }
}
