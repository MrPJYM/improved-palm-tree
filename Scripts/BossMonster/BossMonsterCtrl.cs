using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterCtrl : MonoBehaviour
{
    public Animator bossAni;
    Animator playerAni;
    GameObject player;
    public Camera mainCam;          // 메인카메라 (기존카메라)
    public Camera bossroomCam;      // 보스방카메라 (전체적인 보스룸 보여줌)
    public BossObj bossObj;
    public HPBar HpBar;
    Vector3 bossroom;               // 보스방 z좌표 저장
    public float enterTime;         // 보스방 진입 시간 저장
    public float attackRange;       // 공격 거리
    public int playerDamage;        // 플레이어가 주는 데미지


    void Start()
    {
        bossAni = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossroom = new Vector3(0, 0, 193); // 보스방 시작 부분 z값 설정
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
            bossAni.SetInteger("aniIndex", 0);  // 플레이어가 던전에 들어오지 않을 때 Idle
        }
        if (player.transform.position.z >= bossroom.z && enterTime < 5.0f)
        {
            bossAni.SetInteger("aniIndex", 1); // 플레이어가 던전에 들어오면 scream
        }
        else if (enterTime >= 5.0f && attackRange >= 10.0f)
        {
            bossAni.SetInteger("aniIndex", 3); // 던전에 들어온지 5초이상이고 거리가 멀면 원거리공격
        }
        else if (attackRange <= 10.0f)
        {
            bossAni.SetInteger("aniIndex", 2); // 거리가 가까워지면 근거리공격
        }
        if (bossObj.bossCurrentHp == 0)
        {
            bossAni.SetInteger("aniIndex", 5); // HP가 0이되면 죽는다.
        }
    }

    public void ChangeCamera()
    {
        if (player.transform.position.z >= bossroom.z && enterTime < 1.0f) // 보스방에 들어가면 카메라가 전체적인 포지션으로 변한다.
        {
            mainCam.enabled = false;
            bossroomCam.enabled = true;
        }
        if (bossObj.bossCurrentHp == 0 || (player.transform.position.z < bossroom.z && enterTime > 1.0f)) // 보스가 죽으면 카메라가 다시 플레이어 시점으로 돌아간다.
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
            enterTime += Time.deltaTime; // 플레이어가 던전에 들어온 후 시간 계산
        }

        attackRange = Vector3.Distance(player.transform.position, transform.position); // 공격 거리 계산
    }
}