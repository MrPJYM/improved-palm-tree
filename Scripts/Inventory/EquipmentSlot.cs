using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipmentSlot : MonoBehaviour
{
    public Image slot;
    public Image itemImage;
    public TextMeshProUGUI nameText;
    [HideInInspector]
    public ItemProperty item;
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
    void Start()
    {
        xMin = transform.position.x - slot.rectTransform.rect.width * 0.5f;
        xMax = transform.position.x + slot.rectTransform.rect.width * 0.5f;
        yMin = transform.position.y - slot.rectTransform.rect.height * 0.5f;
        yMax = transform.position.y + slot.rectTransform.rect.height * 0.5f;
        nameText.text = slot.gameObject.name;
    }
    public bool IsInRect(Vector2 pos)
    {
        if (pos.x >= XMIN && pos.x <= XMAX && pos.y >= YMIN && pos.y <= YMAX)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        
    }
}
