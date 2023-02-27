using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossObj : MonoBehaviour
{
    public int bossMaxHp;
    public int bossCurrentHp;
    public bool bossAttackEnable;
    public Image image;
    GameObject player;
    GameObject playerWeapon;    // 플레이어 무기 정보
    Animator playerAni;
    public TextMeshProUGUI HpText;
    void Start()
    {
        bossMaxHp = BossState.instance.bm_hp;
        bossCurrentHp = bossMaxHp;
        bossAttackEnable = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = GameObject.FindGameObjectWithTag("Weapon");  // 태그로 플레이어의 무기를 찾아 대입한다.
        playerAni = player.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerObj PO = other.GetComponent<PlayerObj>();

        // 플레이어, 플레이어 무기와 충돌하고 플레이어의 애니메이터 상태가 공격상태일 때 보스 hp가 깎인다.
        if (other.gameObject.CompareTag("Player") && playerWeapon && playerAni.GetCurrentAnimatorStateInfo(0).IsName("AttackState"))
        {
            DescreseHp(10);
        }
    }

    public void DescreseHp(int deHp)
    {
        bossCurrentHp -= deHp;
    }
    // Update is called once per frame
    void Update()
    {
        if (image != null)
            image.fillAmount = (float)bossCurrentHp / (float)bossMaxHp;

        HpText.text = bossCurrentHp.ToString();

    }
}
