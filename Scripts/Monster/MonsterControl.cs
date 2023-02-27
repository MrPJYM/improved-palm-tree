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
    string handPath = "Rig/root/hips/spine/chest/shoulder.R/upper_arm.R/forearm.R";  // ������ ���
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
    
    public int playerDamegae = 20;//�÷��̾ �ִ� ������
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
            hand.AddComponent<Damege>();//���� ���� �ϴ� ��ũ��Ʈ �����ȿ� �߰�
        damege = hand.GetComponent<Damege>();
        playerAni = player.GetComponent<Animator>();
        mainCam = Camera.main.GetComponent<Camera>();
        currectPos = transform.position;
        fullHp = monsterObj.m_hp;
        HPUI();

        //�ȴ� ���
    }

    private void OnTriggerEnter(Collider _colider)
    {
        if (monsterObj.m_atkType.Equals("Short")) { 
        if ((_colider.tag.Equals("Player")|| _colider.name.Contains("Broadsword")) && playerAni.GetCurrentAnimatorStateInfo(0).IsName("AttackState"))
        {
            monsterAni.SetInteger("State", 3);
            HitDamage(playerDamegae);
                //�ӽ÷� �÷��̾�κ��� �޴� ���� 20�� ����      
        }
        }

    }
    //��ȯ
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
        Debug.Log("��µ� ������: "+ ItemLoad._itemList[_itemNum].itemName);
        SpriteRenderer dropItemRenderer = dropItem.GetComponent<SpriteRenderer>();
        dropItemRenderer.sprite = loadItem;
        Debug.Log(dropItemRenderer.sprite) ;


    }
    void TheowBall() {
        //���� ��� �߰��� �ѹ� �θ�
        //����Ÿ���� ���Ÿ��� ����캼 ����Ʈ�� ��Ȱ��ȭ �Ȱ� �� ��ġ(hand ��ġ)�� �̵��ϰ� 
        //����캼�� ���������� �÷��̾��� ��ġ�� �̵�(Dmage ����)
        //����캼�� ������ �÷��̾� Ȥ�� ���Ͱ� �ƴѰ��� ������ �����(Dmage ����)
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

        //����(origin_pos) �ֺ����� �����Ÿ���
        if (0.4f >= Vector3.Distance(random_pos, transform.position))
        {
            
            
            random_pos = transform.position + new Vector3(Random.Range(-8 ,8), 0, Random.Range(-8, 8));
            //�������� ���� ������Ʈ �ֺ� ��ġ ����
            //������ ������ġ ������� ���� ���� ���̸� ���� ��ü�� �ִ��� �Ǵ� ������ �ٽ� ���� ��ġ ����
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
        //���Ⱑ �̵� �κ�
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

            //���� ��ġ���� ���� �Ÿ���         
            RandomMove();


        }
        else if ((distance_Player <= monsterObj.m_monsterRR && distance_Player > monsterObj.m_monsterAR) &&! playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {

            //�÷��̾�� ���� �Ÿ� ��������� �÷��̾�� �ٰ�����  
            
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
                //���� ���                      
                  monsterAni.SetInteger("State", 2);

            }

        }
        else if ((distance_Player < monsterObj.m_monsterAR)&& !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die")&& monsterObj.m_hp > 0f)
        {
            
            if (monsterObj.m_atkType.Equals("Short"))
            {
                //���� ����(3f) ������ ����
                random_pos = transform.position;
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                //���� ���
                monsterAni.SetInteger("State", 2);
                damege.enabled = true;
                //���� �����϶��� Damege ��ũ��Ʈ Ȱ��ȭ(���� ���� ��ũ��Ʈ)

            }
            else if (monsterObj.m_atkType.Equals("Long")) {
                //���Ÿ��̹Ƿ� �÷��̾ ������ �ٰ����� �÷��̾�� �ݴ� �������� �̵��ؾ��Ѵ� 
                monsterAni.SetInteger("State", 1);
                //�÷��̾�� �ݴ� ���� �ٶ󺸱�
                transform.rotation = Quaternion.LookRotation(gameObject.transform.position-player.transform.position);
                //TransformDirection�� ����->���� �·�� ��ȯ���ִ� ��ɾ�
                //TransformDirection�� ���� (���� ����)���Ͱ� ��� �ٶ󺸵� ���Ͱ� �ٶ󺸴� ���������� ��(���ñ���,new Vector3(0, 0, 10))�� ������ǥ�� ��ȯ
                random_pos =  transform.TransformDirection(transform.position+new Vector3(0, 0, 10));
                transform.position = Vector3.MoveTowards(gameObject.transform.position, random_pos, monsterObj.m_movespeed * Time.deltaTime);

            }


            
        }
        else if (monsterObj.m_hp <= 0f)
        {
            monsterAni.SetInteger("State", 3);
            random_pos = new Vector3(transform.position.x, -100, transform.position.z);//�̵� ����
        
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
            //3������ ��ǥ���� 2�������� �ٲٱ�(2������ ũ��� 0~1����,�� �븻������ �� ȭ��)
            bool onScreen = screenPoint.z > 0 && (screenPoint.x > 0 && screenPoint.x < 1) && (screenPoint.y > 0 && screenPoint.y < 1);
            //�ش� ������Ʈ�� ī�޶� �þ� ���� ���� �ִ��� Ȯ�� 
                monsterHpUi.SetActive(onScreen==true && monsterObj.m_hp>0);
            //�þ� ���� �� �϶��� ��Ȱ��ȭ
            if (onScreen) {

                    monsterHpUi.transform.position = Camera.main.WorldToScreenPoint(transform.position+new Vector3(0,4,0));
    
            }
            
        }

    }
}
