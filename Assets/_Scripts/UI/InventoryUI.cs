using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inv;

    InventorySlot[] slots;
    void Start()
    {
        inv = Inventory.instance;    
        inv.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inv.items.Count)
            {
                slots[i].AddItem(inv.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        Debug.Log("Updating UI");
    }
}
