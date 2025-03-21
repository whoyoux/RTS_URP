using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isStackable = false;
    public int maxStackSize = 1;

    public virtual void Use()
    {
        // Use the item
        // Something might happen
        Debug.Log("Using " + name);
    }

}
