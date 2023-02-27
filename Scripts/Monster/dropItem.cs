using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class dropItem : MonoBehaviour
{
    // Start is called before the first frame update
    int randomRotate;
    float time;
    float i = 0;

    public ItemProperty myItem;
    Inventory inventory;
    BoxCollider box;
    Sprite img;
    GameObject Player;
    GameObject monterUI;
    GameObject itemUi;
    Image uiImg;
    Color c;
   
    void Start()
    {
        randomRotate = Random.Range(0, 360);
        transform.rotation= Quaternion.Euler(0,randomRotate,0);
        box = GetComponent<BoxCollider>();
        img = GetComponent<SpriteRenderer>().sprite;
        monterUI = GameObject.Find("Canvas/MonsterUI");

        Player = GameObject.FindGameObjectWithTag("Player");
        box.size = new Vector3(2.5f, 2.5f, 0.2f);
        box.isTrigger = true;

    }
    
    public void getItem(ItemProperty _item) {
        
        
        //var slots = inventory._slots;
      
        var emptySlot = Inventory._slots.Find(o =>
        {
            return o.itemImage.sprite == null || o.gameObject.name == "Empty";
        });
        var filledSlot = Inventory._slots.Find(o =>
        {
            return o.gameObject.name == _item.itemName;
        });
        //빈칸 찾기?
        if (emptySlot != null)
        {
            if (filledSlot != null)
            {
                ++filledSlot.item.itemCount;
            }
            else
            {
                emptySlot.SetItem(_item);
            }
        }
        else
        {
            if(filledSlot != null)
            {
                ++filledSlot.item.itemCount;
            }
            else
            {
                Debug.Log("빈 슬롯이 없습니다.");
            }
        }
        //빈칸에 아이템 대입
    }
       
    void getitemUI() { 

        itemUi = new GameObject("get_"+gameObject.name);
        itemUi.AddComponent<Image>();
        itemUi.transform.SetParent(monterUI.transform);

        uiImg = itemUi.GetComponent<Image>();
        uiImg.sprite = img;
        uiImg.rectTransform.sizeDelta = new Vector2(50f, 50f);
       
        itemUi.transform.position =  Camera.main.WorldToScreenPoint(Player.transform.position+new Vector3(0,2,0));
        c = uiImg.color;
        time = 0;
        FaidIN();
        i = 0;
    }
    void FaidIN() {
            time += Time.deltaTime / 4f;
            c.a =c.a  - time;
            uiImg.color = c;
            itemUi.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(0, 2+i, 0));
            i =i+ 0.1f;
        if (c.a >= 0.01f)
        {
            Invoke("FaidIN", 0.05f);

        }
        else if (c.a <= 0.01f) 
        {
            itemUi.SetActive(false);
            //나중에 몬스터 많아지거나해서 드랍 할 아이템 많아지면 비활성화 하지말고 위치만 바꾸면서 돌려쓰기
        }
    }
    
    private void OnTriggerEnter(Collider _colider)
    {
        if (_colider.tag == "Player") {
            
            getitemUI();
            getItem(myItem);

            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime, 1f);
    }
}
