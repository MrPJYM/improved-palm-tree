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
                    monsterObj.m_level = int.Parse(LoadState[1]); //레벨
                    monsterObj.m_exp = int.Parse(LoadState[2]);

                    monsterObj.m_hp = float.Parse(LoadState[3]);
                    monsterObj.m_atkSpeed = float.Parse(LoadState[4]);//공격하러 갈때 스피드
                    monsterObj.m_attackMoveSpeed = float.Parse(LoadState[5]);//공격 속도
                    monsterObj.m_movespeed = float.Parse(LoadState[6]);
                    monsterObj.m_monsterRR = float.Parse(LoadState[7]);//몬스터가 플레이어를 인식하는 범위(monster_recognition_range)
                    monsterObj.m_monsterAR = float.Parse(LoadState[8]);//몬스터 공격 범위(monster_attack_range)

                    monsterObj.m_damage = int.Parse(LoadState[9]); //들어가는 데미지
                    monsterObj.m_defens = int.Parse(LoadState[10]); //방어력
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
                    //이름을 기준으로 프리팹 불러올때를 위해 (1),(2),(3) 같은거 제거한뒤 PrefabName에 저장
                    //이 방식을 사용하기위해서 몬스터 이름=해당 몬스터 프래팹 이름 으로 설정해주실수 있나요?
                    //만약 어려우실거 같으면 말씀해주세요 다른방식으로 바꿔 올게요~
                    int index = MonsterLoad[0].IndexOf("(")-1;
                    PrefabName = MonsterLoad[0].Substring(0, index);             
                }
                else
                {
                    PrefabName = MonsterLoad[0];
                }

                GameObject obj = Resources.Load<GameObject>("Monster/"+PrefabName);
                //PrefabName에 저장된 값을 기준으로 리소스 로드 (이를 위해서 Resources 파일 안에 몬스터들 프리팹을 추가해주세요)
                Monster = Instantiate<GameObject>(obj, new Vector3(float.Parse(MonsterLoad[1]), float.Parse(MonsterLoad[2]), float.Parse(MonsterLoad[3])
                                                            ), Quaternion.EulerRotation(float.Parse(MonsterLoad[4]), float.Parse(MonsterLoad[5]), float.Parse(MonsterLoad[6])));
                Monster.transform.localScale=new Vector3(float.Parse(MonsterLoad[7]), float.Parse(MonsterLoad[8]), float.Parse(MonsterLoad[9]));
                Monster.name = MonsterLoad[0];

                //읽어온 정보대로 몬스터 설치
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
                    //부모가 존재하면 자식으로 설정
                    GameObject parent = GameObject.Find(MonsterLoad[10]);
                    Monster.transform.SetParent(parent.transform);
                  }
                Tmp = sr.ReadLine(); //다음에 설치할 몬스터 정보 읽어오기
                   
            }
                
        }
    }

    
}
