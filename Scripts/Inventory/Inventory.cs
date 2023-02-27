using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Inventory : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image moveItem;
    public Transform slotRoot;
    public Transform equipSlotRoot;
    public Store store;
    public Equipment equipment;
    //List<Slot> slots 를 static으로 변경하고 프로퍼티 추가함 (강선화)
    private static List<Slot> slots;
    public static List<Slot> _slots
    {
        get { return slots; }
        set { slots = value; }
    }
    //-------------------------------------
    private List<EquipmentSlot> equipSlots;
    public TextMeshProUGUI goldText;
    int selectedSlot;
    PlayerObj player;
    [HideInInspector]
    public ItemProperty item;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObj>();
        slots = new List<Slot>();
        for (int i = 0; i < slotRoot.childCount; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            slots.Add(slot);
            slots[i].gameObject.name = "Empty";
        }
        equipSlots = new List<EquipmentSlot>();
        for (int i = 0; i < equipSlotRoot.childCount; i++)
        {
            var slot = equipSlotRoot.GetChild(i).GetComponent<EquipmentSlot>();
            equipSlots.Add(slot);
        }
        store.onSlotClick += BuyItem;
    }
    void Update()
    {
        goldText.text = "Money : " + PlayerState.instance.gold.ToString();
    }
    public void BuyItem(ItemProperty item)
    {
        var emptySlot = slots.Find(o =>
        {
            return o.itemImage.sprite == null || o.gameObject.name == "Empty";
        });
        var filledSlot = slots.Find(o =>
        {
            return o.gameObject.name == item.itemName;
        });
        if (emptySlot != null)
        {
            if (PlayerState.instance.gold >= item.itemCost)
            {
                if (filledSlot != null)
                {
                    ++filledSlot.item.itemCount;
                }
                else
                {
                    emptySlot.SetItem(item);
                }
                PlayerState.instance.gold -= item.itemCost;
            }
            else
            {
                Debug.Log("구매불가");
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsInRect(eventData.position))
            {
                if (slots[i].itemImage.gameObject.activeSelf != false)
                {
                    moveItem.transform.position = eventData.position;
                    string itemName = slots[i].itemImage.sprite.name;
                    moveItem.gameObject.SetActive(true);
                    moveItem.sprite = Resources.Load<Sprite>("Equipment/" + itemName);
                    selectedSlot = i;
                }
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        moveItem.transform.position = eventData.position;
        moveItem.sprite = null;
        moveItem.gameObject.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (selectedSlot != -1)
            return;
        moveItem.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (selectedSlot == -1)
            return;
        moveItem.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (selectedSlot == -1)
            return;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsInRect(eventData.position))  // InventorySlot에 놓을때
            {
                if (slots[selectedSlot].itemImage.sprite != null)
                {
                    if (slots[i].itemImage.gameObject.activeSelf == false)  // slot이 비어있을때
                    {
                        ItemProperty selectedItem = slots[selectedSlot].item;
                        slots[i].itemImage.gameObject.SetActive(true);
                        slots[i].itemImage.enabled = true;
                        slots[i].itemImage.sprite = selectedItem.itemImage;
                        slots[i].gameObject.name = selectedItem.itemName;
                        slots[i].item = selectedItem;
                        slots[selectedSlot].itemImage.gameObject.SetActive(false);
                        slots[selectedSlot].itemCountText.gameObject.SetActive(false);
                        slots[selectedSlot].itemImage.enabled = false;
                        slots[selectedSlot].itemImage.sprite = null;
                        slots[selectedSlot].gameObject.name = "Empty";
                        slots[selectedSlot].item = null;
                    }
                    else    // slot에 아이템이 있을때
                    {
                        ItemProperty toItem = slots[i].item;
                        ItemProperty fromItem = slots[selectedSlot].item;
                        //if (fromItem == toItem)
                        //{
                        //    if (i != selectedSlot)
                        //    {
                        //toItem.itemCount += fromItem.itemCount;
                        //slots[selectedSlot].itemImage.gameObject.SetActive(false);
                        //slots[selectedSlot].itemImage.enabled = false;
                        //slots[selectedSlot].itemImage.sprite = null;
                        //slots[selectedSlot].gameObject.name = "Empty";
                        //slots[selectedSlot].item = null;
                        //    }
                        //}
                        slots[i].itemImage.sprite = fromItem.itemImage;
                        slots[i].gameObject.name = fromItem.itemName;
                        slots[i].item = fromItem;
                        slots[selectedSlot].itemImage.sprite = toItem.itemImage;
                        slots[selectedSlot].gameObject.name = toItem.itemName;
                        slots[selectedSlot].item = toItem;
                    }
                    moveItem.transform.position = eventData.position;
                    moveItem.sprite = null;
                    moveItem.gameObject.SetActive(false);
                    selectedSlot = -1;
                }
            }
        }
        if (store.IsInRect(eventData.position)) // store에 놓을때
        {
            if (store.gameObject.activeSelf == true)
            {
                if (slots[selectedSlot].itemImage.sprite != null)
                {
                    ItemProperty selectedItem = slots[selectedSlot].item;
                    if (selectedItem.itemCount > 1)
                    {
                        --selectedItem.itemCount;
                    }
                    else
                    {
                        slots[selectedSlot].itemImage.gameObject.SetActive(false);
                        slots[selectedSlot].itemImage.enabled = false;
                        slots[selectedSlot].itemImage.sprite = null;
                        slots[selectedSlot].gameObject.name = "Empty";
                        slots[selectedSlot].item = null;
                    }
                    PlayerState.instance.gold += (selectedItem.itemCost / 2);
                }
            }
        }
        for (int i = 0; i < equipSlots.Count; i++)
        {
            if (equipSlots[i].IsInRect(eventData.position)) // Equipment에 놓을때
            {
                if (equipment.gameObject.activeSelf == true)
                {
                    if (slots[selectedSlot].itemImage.sprite != null)
                    {
                        if (equipSlots[i].gameObject.name.Equals(slots[selectedSlot].item.itemtype.ToString()) == true)
                        {
                            if (equipSlots[i].itemImage.gameObject.activeSelf == false) // equipmentSlot이 비어있을때
                            {
                                ItemProperty selectedItem = slots[selectedSlot].item;
                                if (selectedItem.itemCount > 1)
                                {
                                    equipSlots[i].itemImage.gameObject.SetActive(true);
                                    equipSlots[i].itemImage.enabled = true;
                                    equipSlots[i].itemImage.sprite = selectedItem.itemImage;
                                    equipSlots[i].item = selectedItem;
                                    equipSlots[i].nameText.text = selectedItem.itemName;
                                    --selectedItem.itemCount;

                                }
                                else
                                {
                                    equipSlots[i].itemImage.gameObject.SetActive(true);
                                    equipSlots[i].itemImage.enabled = true;
                                    equipSlots[i].itemImage.sprite = selectedItem.itemImage;
                                    equipSlots[i].item = selectedItem;
                                    equipSlots[i].nameText.text = selectedItem.itemName;
                                    slots[selectedSlot].itemImage.gameObject.SetActive(false);
                                    slots[selectedSlot].itemImage.enabled = false;
                                    slots[selectedSlot].itemImage.sprite = null;
                                    slots[selectedSlot].gameObject.name = "Empty";
                                    slots[selectedSlot].item = null;
                                }
                                PlayerState.instance.p_hp += selectedItem.itemHp;
                                player.playerMaxHp = PlayerState.instance.p_hp;
                            }
                            else    // equipmentSlot에 아이템이 있을때
                            {
                                ItemProperty toItem = equipSlots[i].item;
                                ItemProperty fromItem = slots[selectedSlot].item;
                                if (toItem != fromItem)
                                {
                                    var emptySlot = slots.Find(o =>
                                    {
                                        return o.itemImage.sprite == null || o.gameObject.name == "Empty";
                                    });
                                    var filledSlot = slots.Find(o =>
                                    {
                                        return o.gameObject.name == toItem.itemName;
                                    });
                                    if (emptySlot != null)
                                    {
                                        if (fromItem.itemCount > 1)
                                        {
                                            if (filledSlot != null)
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                            }
                                            else
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                                emptySlot.itemImage.gameObject.SetActive(true);
                                                emptySlot.itemImage.enabled = true;
                                                emptySlot.itemImage.sprite = toItem.itemImage;
                                                emptySlot.gameObject.name = toItem.itemName;
                                                emptySlot.item = toItem;
                                            }
                                            ++toItem.itemCount;
                                            --fromItem.itemCount;
                                        }
                                        else
                                        {
                                            if (filledSlot != null)
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                                slots[selectedSlot].itemImage.gameObject.SetActive(false);
                                                slots[selectedSlot].itemImage.enabled = false;
                                                slots[selectedSlot].itemImage.sprite = null;
                                                slots[selectedSlot].gameObject.name = "Empty";
                                                slots[selectedSlot].item = null;
                                                --fromItem.itemCount;
                                                ++toItem.itemCount;
                                            }
                                            else
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                                slots[selectedSlot].itemImage.sprite = toItem.itemImage;
                                                slots[selectedSlot].gameObject.name = toItem.itemName;
                                                slots[selectedSlot].item = toItem;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (fromItem.itemCount > 1)
                                        {
                                            if (filledSlot != null)
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                                ++toItem.itemCount;
                                                --fromItem.itemCount;
                                            }
                                            else
                                            {
                                                Debug.Log("빈 슬롯이 없습니다.");
                                            }
                                        }
                                        else
                                        {
                                            if (filledSlot != null)
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                                slots[selectedSlot].itemImage.gameObject.SetActive(false);
                                                slots[selectedSlot].itemImage.enabled = false;
                                                slots[selectedSlot].itemImage.sprite = null;
                                                slots[selectedSlot].gameObject.name = "Empty";
                                                slots[selectedSlot].item = null;
                                                --fromItem.itemCount;
                                                ++toItem.itemCount;
                                            }
                                            else
                                            {
                                                equipSlots[i].itemImage.sprite = fromItem.itemImage;
                                                equipSlots[i].item = fromItem;
                                                equipSlots[i].nameText.text = fromItem.itemName;
                                                slots[selectedSlot].itemImage.sprite = toItem.itemImage;
                                                slots[selectedSlot].gameObject.name = toItem.itemName;
                                                slots[selectedSlot].item = toItem;
                                            }
                                        }
                                    }
                                    PlayerState.instance.p_hp += (fromItem.itemHp - toItem.itemHp);
                                    player.playerMaxHp = PlayerState.instance.p_hp;
                                    if (player.playerCurrentHp >= player.playerMaxHp)
                                        player.playerCurrentHp = player.playerMaxHp;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}