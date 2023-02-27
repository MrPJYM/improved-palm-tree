using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store : MonoBehaviour
{
    public Image store;
    public ItemBuffer itemBuffer;
    public Transform slotRoot;
    private List<Slot> slots;
    public System.Action<ItemProperty> onSlotClick;
    float xMin;
    public float XMIN
    {
        get
        {
            xMin = transform.position.x - store.rectTransform.rect.width * 0.5f;
            return xMin;
        }
    }
    float xMax;
    public float XMAX
    {
        get
        {
            xMax = transform.position.x + store.rectTransform.rect.width * 0.5f;
            return xMax;
        }
    }
    float yMin;
    public float YMIN
    {
        get
        {
            yMin = transform.position.y - store.rectTransform.rect.height * 0.5f;
            return yMin;
        }
    }
    float yMax;
    public float YMAX
    {
        get
        {
            yMax = transform.position.y + store.rectTransform.rect.height * 0.5f;
            return yMax;
        }
    }
    void Start()
    {
        xMin = transform.position.x - store.rectTransform.rect.width * 0.5f;
        xMax = transform.position.x + store.rectTransform.rect.width * 0.5f;
        yMin = transform.position.y - store.rectTransform.rect.height * 0.5f;
        yMax = transform.position.y + store.rectTransform.rect.height * 0.5f;
        slots = new List<Slot>();
        int slotCount = slotRoot.childCount;
        for(int i = 0; i < slotCount; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            if (i < itemBuffer.items.Count)
            {
                slot.SetItem(itemBuffer.items[i]);
            }
            else
            {
                slot.GetComponent<Button>().interactable = false;
            }
            slots.Add(slot);
        }
    }
    public bool IsInRect(Vector2 pos)
    {
        if (pos.x >= XMIN && pos.x <= XMAX && pos.y >= YMIN && pos.y <= YMAX)
        {
            return true;
        }
        return false;
    }
    public void OnclickSlot(Slot slot)
    {
        if(onSlotClick != null)
        {
            onSlotClick(slot.item);
        }
    }
}