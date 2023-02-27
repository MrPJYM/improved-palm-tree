using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ItemLoad : MonoBehaviour
{

    public static List<ItemProperty> itemLIst;
    public static List<ItemProperty> _itemList
    {
        get
        {
            return itemLIst;
        }

        set
        {

            itemLIst = value;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        itemLIst = new List<ItemProperty>();
        LoadItem();
    }
    void LoadItem()
    {
        string itemLIst_path = Application.dataPath + "/" + "item List.csv";
        using (StreamReader sr = new StreamReader(itemLIst_path))
        {
            string[] loadItem;
            string Tmp = sr.ReadLine();
            Tmp = sr.ReadLine();
            

            while (Tmp != null)
            {
                ItemProperty tmpItem = new ItemProperty();
                loadItem = Tmp.Split(',');           //로드 아이템에 분리된 줄 저장
                tmpItem.itemName = loadItem[0];
                //0번은 이름
                tmpItem.itemCost = int.Parse(loadItem[1]);
                if (loadItem[2].Equals("weapon"))
                {
                    tmpItem.itemtype = ItemProperty.ItemType.weapon;
                }
                else if (loadItem[2].Equals("chest"))
                {
                    tmpItem.itemtype = ItemProperty.ItemType.chest;
                }
                else if (loadItem[2].Equals("helm"))
                {
                    tmpItem.itemtype = ItemProperty.ItemType.helm;
                }
                else if (loadItem[2].Equals("gloves"))
                {
                    tmpItem.itemtype = ItemProperty.ItemType.gloves;
                }
                else if (loadItem[2].Equals("boots"))
                {
                    tmpItem.itemtype = ItemProperty.ItemType.boots;
                }
                else if (loadItem[2].Equals("pants"))
                {
                    tmpItem.itemtype = ItemProperty.ItemType.pants;
                }
                else if (loadItem[2] == null)
                {
                    tmpItem.itemtype = ItemProperty.ItemType.pants;
                }
                //임시로 장비외의 것들은 팬츠로 분류

                if (loadItem[2] == "item" )
                {
                    tmpItem.itemImage = AssetDatabase.LoadAssetAtPath("Assets/Resources/Basic_RPG_Icons/Items/Resources" + "/" + loadItem[0] + ".png", typeof(Sprite)) as Sprite;
                }
                else if (loadItem[2] == "weapon")
                {
                    tmpItem.itemImage = AssetDatabase.LoadAssetAtPath("Assets/Resources/Basic_RPG_Icons/Items/Weapons" + "/" + loadItem[0] + ".png", typeof(Sprite)) as Sprite;
                }
                else 
                {
                    tmpItem.itemImage = AssetDatabase.LoadAssetAtPath("Assets/Resources/Basic_RPG_Icons/Items/Armor/Common" + "/" + loadItem[0] + ".png", typeof(Sprite)) as Sprite;
                }
         
                itemLIst.Add(tmpItem);
                //추가하고
               
                Tmp = sr.ReadLine();


                //읽기

            }
           

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
