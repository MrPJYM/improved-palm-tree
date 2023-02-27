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
    GameObject playerWeapon;    // �÷��̾� ���� ����
    Animator playerAni;
    public TextMeshProUGUI HpText;
    void Start()
    {
        bossMaxHp = BossState.instance.bm_hp;
        bossCurrentHp = bossMaxHp;
        bossAttackEnable = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = GameObject.FindGameObjectWithTag("Weapon");  // �±׷� �÷��̾��� ���⸦ ã�� �����Ѵ�.
        playerAni = player.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerObj PO = other.GetComponent<PlayerObj>();

        // �÷��̾�, �÷��̾� ����� �浹�ϰ� �÷��̾��� �ִϸ����� ���°� ���ݻ����� �� ���� hp�� ���δ�.
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
