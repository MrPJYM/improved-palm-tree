using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System.IO;
public class MonsterLoad : MonoBehaviour
{
    GameObject Monster;
   MonsterObj monsterObj;
    public static GameObject[] SnowBalls;

    private void Awake()
    {     
        LoadMonster();
        LoadSnowBall(10);
    }
    private void Start()
    {
        for (int i = 0; i < SnowBalls.Length; i++)
            SnowBalls[i].transform.parent = GameObject.Find("AtkType").transform;
    }
    void LoadState()
    {
        monsterObj = Monster.GetComponent<MonsterObj>();
        string monster_path = Application.dataPath + "/" + "Monster State.csv";
        using (StreamReader sr = new StreamReader(monster_path))
        {
            string[] LoadState;
            string Tmp = sr.ReadLine();
            Tmp = sr.ReadLine();
            LoadState = Tmp.Split(',');
            while (Tmp != null)
            {
                if (Monster.name.Equals(LoadState[0]))
                {
                    monsterObj.m_name = LoadState[0];
                    monsterObj.m_level = int.Parse(LoadState[1]); //����
                    monsterObj.m_exp = int.Parse(LoadState[2]);

                    monsterObj.m_hp = float.Parse(LoadState[3]);
                    monsterObj.m_atkSpeed = float.Parse(LoadState[4]);//�����Ϸ� ���� ���ǵ�
                    monsterObj.m_attackMoveSpeed = float.Parse(LoadState[5]);//���� �ӵ�
                    monsterObj.m_movespeed = float.Parse(LoadState[6]);
                    monsterObj.m_monsterRR = float.Parse(LoadState[7]);//���Ͱ� �÷��̾ �ν��ϴ� ����(monster_recognition_range)
                    monsterObj.m_monsterAR = float.Parse(LoadState[8]);//���� ���� ����(monster_attack_range)

                    monsterObj.m_damage = int.Parse(LoadState[9]); //���� ������
                    monsterObj.m_defens = int.Parse(LoadState[10]); //����
                    monsterObj.m_atkType = LoadState[11];

                    break;
                }
                else 
                {
                    Tmp = sr.ReadLine();
                    LoadState = Tmp.Split(',');
                }
               
            }

        }

    }

    void LoadSnowBall(int _num) {

        SnowBalls = new GameObject[_num];
        for (int i = 0; i < _num; i++) 
        { 
        //GameObject parents = GameObject.Find("AtkType");
        GameObject loadSnowBall = Resources.Load<GameObject>("Monster/SnowBall");
        GameObject snowBall = Instantiate<GameObject>(loadSnowBall, new Vector3(0, 0, 0),Quaternion.identity);
        snowBall.AddComponent<Damege>();
        snowBall.name = "SnowBall" + i;
        snowBall.SetActive(false);
            SnowBalls[i] = snowBall;
        }
    }
    void LoadMonster() {
        string monster_path = Application.dataPath + "/" + "Monster Data.csv";
        //Monster[10]=> [0]: name ,[1]~[3]:positon, [4]~[6]:rotation,[7]~[9]:scale,[10]:parent name,[11]:AtkType

        using (StreamReader sr = new StreamReader(monster_path))
        {
            string[] MonsterLoad = new string[10];
            string Tmp = sr.ReadLine();
            Tmp = sr.ReadLine();
           
             while (Tmp!=null){
                    MonsterLoad = Tmp.Split(',');
             
                string PrefabName;
                if (MonsterLoad[0].Contains("("))
                {
                    //�̸��� �������� ������ �ҷ��ö��� ���� (1),(2),(3) ������ �����ѵ� PrefabName�� ����
                    //�� ����� ����ϱ����ؼ� ���� �̸�=�ش� ���� ������ �̸� ���� �������ֽǼ� �ֳ���?
                    //���� �����ǰ� ������ �������ּ��� �ٸ�������� �ٲ� �ðԿ�~
                    int index = MonsterLoad[0].IndexOf("(")-1;
                    PrefabName = MonsterLoad[0].Substring(0, index);             
                }
                else
                {
                    PrefabName = MonsterLoad[0];
                }

                GameObject obj = Resources.Load<GameObject>("Monster/"+PrefabName);
                //PrefabName�� ����� ���� �������� ���ҽ� �ε� (�̸� ���ؼ� Resources ���� �ȿ� ���͵� �������� �߰����ּ���)
                Monster = Instantiate<GameObject>(obj, new Vector3(float.Parse(MonsterLoad[1]), float.Parse(MonsterLoad[2]), float.Parse(MonsterLoad[3])
                                                            ), Quaternion.EulerRotation(float.Parse(MonsterLoad[4]), float.Parse(MonsterLoad[5]), float.Parse(MonsterLoad[6])));
                Monster.transform.localScale=new Vector3(float.Parse(MonsterLoad[7]), float.Parse(MonsterLoad[8]), float.Parse(MonsterLoad[9]));
                Monster.name = MonsterLoad[0];

                //�о�� ������� ���� ��ġ
                Monster.AddComponent<MonsterControl>();
                Monster.AddComponent<NavMeshAgent>();
                Monster.AddComponent<CapsuleCollider>();

                CapsuleCollider collider =Monster.GetComponent<CapsuleCollider>();

                collider.center = new Vector3(0, 1, 0);
                collider.radius = 0.9f;
                collider.height = 2;
                Monster.tag = "Monster";

                Monster.AddComponent<MonsterObj>();
                LoadState();
        
                if (MonsterLoad[10] != null)
                  {
                    //�θ� �����ϸ� �ڽ����� ����
                    GameObject parent = GameObject.Find(MonsterLoad[10]);
                    Monster.transform.SetParent(parent.transform);
                  }
                Tmp = sr.ReadLine(); //������ ��ġ�� ���� ���� �о����
                   
            }
                
        }
    }

    
}
