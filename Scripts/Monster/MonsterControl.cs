using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System.IO;


public class MonsterControl : MonoBehaviour
{
    string handPath = "Rig/root/hips/spine/chest/shoulder.R/upper_arm.R/forearm.R";  // 오른팔 경로
    GameObject hand;
    Damege damege;
    Vector3 origin_pos;
    Vector3 random_pos;
    Animator monsterAni;
    MonsterObj monsterObj;
    GameObject monsterHpUi;
    ItemLoad itemLoad;
    Camera mainCam;
    Vector3 currectPos;
    float time;
 
    Image hpBar;
    float fullHp;
    
    public int playerDamegae = 20;//플레이어가 주는 데미지
    float distance;
    public float distance_Player;
     Animator playerAni;
    GameObject player;
    GameObject dropItem = null;

    

    void Start()
    {
        monsterObj = gameObject.GetComponent<MonsterObj>();
        origin_pos = transform.position;
        random_pos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        monsterAni = GetComponent<Animator>();
            hand = transform.Find(handPath).gameObject;
            hand.AddComponent<Damege>();//공격 판정 하는 스크립트 오른팔에 추가
        damege = hand.GetComponent<Damege>();
        playerAni = player.GetComponent<Animator>();
        mainCam = Camera.main.GetComponent<Camera>();
        currectPos = transform.position;
        fullHp = monsterObj.m_hp;
        HPUI();

        //걷는 모션
    }

    private void OnTriggerEnter(Collider _colider)
    {
        if (monsterObj.m_atkType.Equals("Short")) { 
        if ((_colider.tag.Equals("Player")|| _colider.name.Contains("Broadsword")) && playerAni.GetCurrentAnimatorStateInfo(0).IsName("AttackState"))
        {
            monsterAni.SetInteger("State", 3);
            HitDamage(playerDamegae);
                //임시로 플레이어로부터 받는 공격 20로 설정      
        }
        }

    }
    //정환
    public void HitDamage(int playerDamege_1)
    {
        monsterObj.m_hp -= playerDamege_1;
        hpBar.fillAmount = monsterObj.m_hp / fullHp;
    }
    
    void DropItem(int _itemNum) {
 
        Vector3 itemPos = transform.position + new Vector3(Random.Range(0.0f, 2.0f), 1, Random.Range(0.0f, 2.0f));

        dropItem = new GameObject(ItemLoad._itemList[_itemNum].itemName);
        //string itemPath = "Assets/Basic_RPG_Icons/Items/Resources";

        Sprite loadItem = ItemLoad._itemList[_itemNum].itemImage;
     
        dropItem.transform.SetParent(transform.parent);
        dropItem.transform.localScale = new Vector3(0.5f, 0.5f, 0.2f);
        dropItem.transform.position = itemPos;
        dropItem.AddComponent<BoxCollider>();
        dropItem.AddComponent<SpriteRenderer>();
        dropItem.AddComponent<dropItem>();

        dropItem dropItemScript = dropItem.GetComponent<dropItem>();
        dropItemScript.myItem = ItemLoad._itemList[_itemNum];
        Debug.Log("출력될 아이템: "+ ItemLoad._itemList[_itemNum].itemName);
        SpriteRenderer dropItemRenderer = dropItem.GetComponent<SpriteRenderer>();
        dropItemRenderer.sprite = loadItem;
        Debug.Log(dropItemRenderer.sprite) ;


    }
    void TheowBall() {
        //공격 모션 중간에 한번 부름
        //공격타입이 원거리면 스노우볼 리스트중 비활성화 된거 손 위치(hand 위치)로 이동하고 
        //스노우볼은 던젔을떄의 플레이어의 위치로 이동(Dmage 내용)
        //스노우볼이 닿은게 플레이어 혹은 몬스터가 아닌것이 닿으면 사라짐(Dmage 내용)
        if (monsterObj.m_atkType.Equals("Long")) { 
        for (int i = 0; i < MonsterLoad.SnowBalls.Length; i++)
        {
            if (!MonsterLoad.SnowBalls[i].activeSelf)
            {
                MonsterLoad.SnowBalls[i].SetActive(true);
                MonsterLoad.SnowBalls[i].transform.position = hand.transform.position;             
                break;
            }

        }
        }
    }
   
    void HPUI()
    {
        monsterHpUi = new GameObject();
        monsterHpUi.name = "MonsterHP";

        GameObject parent = GameObject.Find("MonsterUI");

        monsterHpUi.transform.SetParent(parent.transform);
        monsterHpUi.AddComponent<RectTransform>();

        Image tmp1 = Resources.Load<Image>("MonsterHP");
        hpBar = Instantiate(tmp1, new Vector3(0, 0, 0), Quaternion.identity, monsterHpUi.transform);

        TextMeshProUGUI tmp2 = Resources.Load<TextMeshProUGUI>("Level.Name");
        TextMeshProUGUI levelName = Instantiate(tmp2, new Vector3(0, 30, 0), Quaternion.identity, monsterHpUi.transform);

        levelName.text = "Lv " + monsterObj.m_level + " " + monsterObj.m_name;
    }
    void RandomMove()
    {

        //원점(origin_pos) 주변에서 서성거리기
        if (0.4f >= Vector3.Distance(random_pos, transform.position))
        {
            
            
            random_pos = transform.position + new Vector3(Random.Range(-8 ,8), 0, Random.Range(-8, 8));
            //랜덤으로 게임 오브젝트 주변 위치 선점
            //선점된 랜덤위치 상공에서 땅을 향해 레이를 쏴서 물체가 있는지 판단 있으면 다시 랜덤 위치 선정
            Ray ray = new Ray();
            ray.origin = new Vector3(random_pos.x, 50, random_pos.z);
            ray.direction = new Vector3(random_pos.x, -50, random_pos.z);
            RaycastHit rayhit;
            if (Physics.Raycast(ray.origin, ray.direction, out rayhit, Mathf.Infinity))
            {
              
                if (!rayhit.collider.CompareTag("Terrain")||rayhit.collider.name.Contains("MountainSnow"))
                {
                    random_pos = origin_pos + new Vector3(Random.Range(-8,8 ), 0, Random.Range(-8, 8));
                   
                   
                }
                

            }
           
        }
        //여기가 이동 부분
        distance = Vector3.Distance(origin_pos, transform.position);
        monsterAni.SetInteger("State", 1);
        if (monsterObj.m_hp> 0) { 
        if (distance < 8f)
        {
            transform.position = Vector3.MoveTowards(transform.position, random_pos, monsterObj.m_movespeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation((random_pos - transform.position));
                
        }
        else
        {
            random_pos = origin_pos;
            transform.position = Vector3.MoveTowards(transform.position, origin_pos, monsterObj.m_movespeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation((random_pos - transform.position));
  

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        distance_Player = Vector3.Distance(player.transform.position, transform.position);
        if (time > 2f) {
            time -=2f;
            if (currectPos == transform.position)
            {
                random_pos = origin_pos + new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
            }
            
                currectPos = transform.position;
            
        }
        time += Time.deltaTime;
        if (((distance_Player > monsterObj.m_monsterRR) && monsterObj.m_hp > 0) || (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die")&& monsterObj.m_hp >0f))
        {
           
            damege.enabled = false;

            //원래 위치에서 서성 거리기         
            RandomMove();


        }
        else if ((distance_Player <= monsterObj.m_monsterRR && distance_Player > monsterObj.m_monsterAR) &&! playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {

            //플레이어와 일정 거리 가까워지면 플레이어에게 다가가기  
            
            if (monsterObj.m_atkType.Equals("Short"))
            {
                damege.enabled = false;
                monsterAni.SetInteger("State", 1);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), monsterObj.m_attackMoveSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);

            }
            else if (monsterObj.m_atkType.Equals("Long"))
            {
                random_pos = transform.position;
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);              
                //공격 모션                      
                  monsterAni.SetInteger("State", 2);

            }

        }
        else if ((distance_Player < monsterObj.m_monsterAR)&& !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die")&& monsterObj.m_hp > 0f)
        {
            
            if (monsterObj.m_atkType.Equals("Short"))
            {
                //공격 범위(3f) 들어오면 공격
                random_pos = transform.position;
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                //공격 모션
                monsterAni.SetInteger("State", 2);
                damege.enabled = true;
                //공격 상태일때만 Damege 스크립트 활성화(공격 판정 스크립트)

            }
            else if (monsterObj.m_atkType.Equals("Long")) {
                //원거리이므로 플레이어가 가까이 다가오면 플레이어와 반대 방향으로 이동해야한다 
                monsterAni.SetInteger("State", 1);
                //플레이어와 반대 방향 바라보기
                transform.rotation = Quaternion.LookRotation(gameObject.transform.position-player.transform.position);
                //TransformDirection가 로컬->월드 좌료로 변환해주는 명령어
                //TransformDirection를 통해 (월드 기준)몬스터가 어디를 바라보든 몬스터가 바라보는 방향으로의 앞(로컬기준,new Vector3(0, 0, 10))을 월드좌표로 반환
                random_pos =  transform.TransformDirection(transform.position+new Vector3(0, 0, 10));
                transform.position = Vector3.MoveTowards(gameObject.transform.position, random_pos, monsterObj.m_movespeed * Time.deltaTime);

            }


            
        }
        else if (monsterObj.m_hp <= 0f)
        {
            monsterAni.SetInteger("State", 3);
            random_pos = new Vector3(transform.position.x, -100, transform.position.z);//이동 멈춤
        
            if (dropItem == null)
            {
                DropItem(5);
                DropItem(6);

            }

            if (monsterAni.speed>0.01f) {
                 transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 100, transform.position.z), Time.deltaTime * monsterObj.m_movespeed);
                monsterAni.speed = monsterAni.speed - 0.4f*Time.deltaTime;
            }else if(monsterAni.speed <= 0.01f) 
            {
                gameObject.SetActive(false);
            }

    }
     

        if (mainCam.isActiveAndEnabled) {

            Vector3 screenPoint= Camera.main.WorldToViewportPoint(transform.position);
            //3차원상 좌표값을 2차원으로 바꾸기(2차원의 크기는 0~1사이,즉 노말라이즈 된 화면)
            bool onScreen = screenPoint.z > 0 && (screenPoint.x > 0 && screenPoint.x < 1) && (screenPoint.y > 0 && screenPoint.y < 1);
            //해당 오브젝트가 카메라 시야 범위 내에 있는지 확인 
                monsterHpUi.SetActive(onScreen==true && monsterObj.m_hp>0);
            //시야 범위 외 일때는 비활성화
            if (onScreen) {

                    monsterHpUi.transform.position = Camera.main.WorldToScreenPoint(transform.position+new Vector3(0,4,0));
    
            }
            
        }

    }
}
