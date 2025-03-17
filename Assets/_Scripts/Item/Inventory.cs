using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public List<Item> items = new List<Item>();
    public int maxSpace = 9;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if(items.Count >= maxSpace)
            {
                // TODO: make a sound or something to indicate that the inventory is full
                Debug.Log("Not enough room.");
                return false;
            }
        
            items.Add(item);
            onItemChangedCallback?.Invoke();
        }

        return true;
            
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback?.Invoke();
    }
}
