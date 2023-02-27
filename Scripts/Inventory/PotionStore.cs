using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PotionStore : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;
    private List<Slot> slots;
    public Store store;
    public UIManager uiManager;
    public System.Action<ItemProperty> onSlotClick;

    void Start()
    {
        slots = new List<Slot>();
        int slotCount = slotRoot.childCount;
        int storeSlotCount = store.slotRoot.childCount;
        for(int i = 0; i < slotCount; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            if(i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i+storeSlotCount]);
            }
            else
            {
                slot.GetComponent<Button>().interactable = false;
            }
            slots.Add(slot);
            this.onSlotClick = BuyItem;
        }
    }
    public void BuyItem(ItemProperty item)
    {
        if (PlayerState.instance.gold >= item.itemCost)
        {
            PlayerState.instance.gold -= item.itemCost;
            if (item.itemName.Equals("HP"))
            {
                ++uiManager.hpItemCount;
            }
            else if (item.itemName.Equals("MP"))
            {
                ++uiManager.mpItemCount;
            }
        }
    }
    public void OnclickSlot(Slot slot)
    {
        if (onSlotClick != null)
        {
            onSlotClick(slot.item);
        }
    }
    void Update()
    {
        
    }
}
