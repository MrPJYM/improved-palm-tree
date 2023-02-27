using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MonsterSave : MonoBehaviour
{
    [MenuItem("MapData/Monster Data Save")]
    //에디터 메뉴에 추가 했으니 거기서 누르면 바로 저장 됩니다. 
    //저장 위치는 Assets에 Monster Data.csv로 저장 됩니다.
    static public void MonsterExport()
    {
        GameObject[] Monster = GameObject.FindGameObjectsWithTag("Monster");

        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + "Monster Data.csv"))
        {
            sw.WriteLine("Name" + "," + "Pos X" + "," + "Pos Y" + "," + "Pos Z" + "," + "Rotation X" + "," + "Rotation Y" + "," + "Rotation Z" + "," + "Scale X" + "," + "Scale Y" + "," + "Scale Z" + "," + "Parent");
            for (int i = 0; i < Monster.Length; i++)
            {
                //Monster[]길이만큼 monster 태그 달린 오브젝트 tranform,부모 오브젝트 이름 저장
                sw.Write(Monster[i].name + ",");
                sw.Write(Monster[i].transform.position.x + "," + Monster[i].transform.position.y + "," + Monster[i].transform.position.z + ",");
                sw.Write(Monster[i].transform.rotation.x + "," + Monster[i].transform.rotation.y + "," + Monster[i].transform.rotation.z + ",");
                sw.Write(Monster[i].transform.localScale.x + "," + Monster[i].transform.localScale.y + "," + Monster[i].transform.localScale.z +
                          "," + Monster[i].transform.parent.name + "\n");


            }
            sw.Close();
        }

    }

}
