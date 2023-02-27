using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterCtrl : MonoBehaviour
{
    public Animator bossAni;
    Animator playerAni;
    GameObject player;
    public Camera mainCam;          // ����ī�޶� (����ī�޶�)
    public Camera bossroomCam;      // ������ī�޶� (��ü���� ������ ������)
    public BossObj bossObj;
    public HPBar HpBar;
    Vector3 bossroom;               // ������ z��ǥ ����
    public float enterTime;         // ������ ���� �ð� ����
    public float attackRange;       // ���� �Ÿ�
    public int playerDamage;        // �÷��̾ �ִ� ������


    void Start()
    {
        bossAni = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossroom = new Vector3(0, 0, 193); // ������ ���� �κ� z�� ����
        mainCam.enabled = true;
        bossroomCam.enabled = false;
        enterTime = 0f;
        attackRange = 0;
        playerDamage = 5;
    }
    public void ChangeAnimation()
    {
        if (player.transform.position.z < bossroom.z)
        {
            bossAni.SetInteger("aniIndex", 0);  // �÷��̾ ������ ������ ���� �� Idle
        }
        if (player.transform.position.z >= bossroom.z && enterTime < 5.0f)
        {
            bossAni.SetInteger("aniIndex", 1); // �÷��̾ ������ ������ scream
        }
        else if (enterTime >= 5.0f && attackRange >= 10.0f)
        {
            bossAni.SetInteger("aniIndex", 3); // ������ ������ 5���̻��̰� �Ÿ��� �ָ� ���Ÿ�����
        }
        else if (attackRange <= 10.0f)
        {
            bossAni.SetInteger("aniIndex", 2); // �Ÿ��� ��������� �ٰŸ�����
        }
        if (bossObj.bossCurrentHp == 0)
        {
            bossAni.SetInteger("aniIndex", 5); // HP�� 0�̵Ǹ� �״´�.
        }
    }

    public void ChangeCamera()
    {
        if (player.transform.position.z >= bossroom.z && enterTime < 1.0f) // �����濡 ���� ī�޶� ��ü���� ���������� ���Ѵ�.
        {
            mainCam.enabled = false;
            bossroomCam.enabled = true;
        }
        if (bossObj.bossCurrentHp == 0 || (player.transform.position.z < bossroom.z && enterTime > 1.0f)) // ������ ������ ī�޶� �ٽ� �÷��̾� �������� ���ư���.
        {
            mainCam.enabled = true;
            bossroomCam.enabled = false;
        }
    }

    void Update()
    {
        ChangeAnimation();
        ChangeCamera();

        if (player.transform.position.z >= bossroom.z)
        {
            enterTime += Time.deltaTime; // �÷��̾ ������ ���� �� �ð� ���
        }

        attackRange = Vector3.Distance(player.transform.position, transform.position); // ���� �Ÿ� ���
    }
}