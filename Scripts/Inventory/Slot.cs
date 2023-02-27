using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot : MonoBehaviour
{
    public Image slot;
    public GameObject store;
    public Image itemImage;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI itemCountText;
    float xMin;
    public float XMIN{
        get{
            xMin = transform.position.x - slot.rectTransform.rect.width * 0.5f;
            return xMin;
        }
    }
    float xMax;
    public float XMAX{
        get{
            xMax = transform.position.x + slot.rectTransform.rect.width * 0.5f;
            return xMax;
        }
    }
    float yMin;
    public float YMIN{
        get{
            yMin = transform.position.y - slot.rectTransform.rect.height * 0.5f;
            return yMin;
        }
    }
    float yMax;
    public float YMAX{
        get{
            yMax = transform.position.y + slot.rectTransform.rect.height * 0.5f;
            return yMax;
        }
    }
    [HideInInspector]
    public ItemProperty item;
    private void Awake()
    {

    }
    void Start()
    {
        xMin = transform.position.x - slot.rectTransform.rect.width * 0.5f;
        xMax = transform.position.x + slot.rectTransform.rect.width * 0.5f;
        yMin = transform.position.y - slot.rectTransform.rect.height * 0.5f;
        yMax = transform.position.y + slot.rectTransform.rect.height * 0.5f;
        if (priceText != null)
            priceText.text = item.itemCost.ToString();
    }
    void Update()
    {
        if (itemImage.gameObject.activeSelf == true)
        {
            if (itemCountText != null)
            {
                if (item.itemCount > 1)
                {
                    itemCountText.gameObject.SetActive(true);
                    itemCountText.text = item.itemCount.ToString();
                }
                else if (item.itemCount <= 1)
                {
                    itemCountText.gameObject.SetActive(false);
                }
            }
        }
    }
    public void SetItem(ItemProperty item)
    {
        this.item = item;
        if (item == null)
        {
            itemImage.gameObject.SetActive(false);
            itemImage.enabled = false;
            itemImage.sprite = null;
            gameObject.name = "Empty";
        }
        else
        {
            itemImage.gameObject.SetActive(true);
            itemImage.enabled = true;
            itemImage.sprite = item.itemImage;
            gameObject.name = item.itemName;
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
}