using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MonsterSave : MonoBehaviour
{
    [MenuItem("MapData/Monster Data Save")]
    //������ �޴��� �߰� ������ �ű⼭ ������ �ٷ� ���� �˴ϴ�. 
    //���� ��ġ�� Assets�� Monster Data.csv�� ���� �˴ϴ�.
    static public void MonsterExport()
    {
        GameObject[] Monster = GameObject.FindGameObjectsWithTag("Monster");

        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + "Monster Data.csv"))
        {
            sw.WriteLine("Name" + "," + "Pos X" + "," + "Pos Y" + "," + "Pos Z" + "," + "Rotation X" + "," + "Rotation Y" + "," + "Rotation Z" + "," + "Scale X" + "," + "Scale Y" + "," + "Scale Z" + "," + "Parent");
            for (int i = 0; i < Monster.Length; i++)
            {
                //Monster[]���̸�ŭ monster �±� �޸� ������Ʈ tranform,�θ� ������Ʈ �̸� ����
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
