using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerObj : MonoBehaviour
{
    public UIManager uiManager;
    public int playerMaxHp;
    public int playerCurrentHp { get; set; }
    public int playerMaxMp;
    public int playerCurrentMp { get; set; }
    public bool playerAttackEnable;
    PlayerMove PM;

    public GameObject SkillObj;
    void Start()
    {
        playerMaxHp = PlayerState.instance.p_hp;
        playerCurrentHp = playerMaxHp;
        playerMaxMp = PlayerState.instance.p_mp;
        playerCurrentMp = playerMaxMp;
        playerAttackEnable = false;
        PM = GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*MonsterControl MC=other.GetComponentInParent<MonsterControl>();
        if (other.gameObject.CompareTag("MonsterWeapon"))
        {
            DescreseHp(5);
        }*/
    }
    public void DescreseHp(int deHp)
    {
        if (playerCurrentHp >= deHp)
            playerCurrentHp -= deHp;
        else
            playerCurrentHp = 0;
        if (playerCurrentHp <= 0)
        {
            PlayerDied();
        }
    }
    public void RecoveryHp(int RcHp)
    {
        if (uiManager.hpItemCount > 0)
        {
            uiManager.hpItemCount -= 1;
            playerCurrentHp += RcHp;
            if (playerCurrentHp >= playerMaxHp)
                playerCurrentHp = playerMaxHp;
        }
    }
    public void DescreseMp(int deMp)
    {
        if (playerCurrentMp >= deMp)
            playerCurrentMp -= deMp;
        else
            playerCurrentMp = 0;
    }
    public void RecoveryMp(int RcMp)
    {
        if (uiManager.mpItemCount > 0)
        {
            uiManager.mpItemCount -= 1;
            playerCurrentMp += RcMp;
            if (playerCurrentMp >= playerMaxMp)
                playerCurrentMp = playerMaxMp;
        }
    }
    public void PlayerDied()
    {
        this.enabled = false;
        PM.ani.SetInteger("DieState", 1);
        PM.enabled = false;
        GmaeManager.PlayerDiedDetect(this);
        //PlayerReVive();
    }

    public void PlayerReVive()
    {
        this.enabled = true;
        PM.enabled = true;
        playerMaxHp = PlayerState.instance.p_hp;
        playerCurrentHp = playerMaxHp;
        playerMaxMp = PlayerState.instance.p_mp;
        playerCurrentMp = playerMaxMp;
        playerAttackEnable = false;
        PM.ani.SetInteger("DieState", 0);
    }

    public void SkillActive()
    {
        PM.AttackFrame();
        PM.SkillFrame();
        var obj = SkillPool.GetObject();
        obj.transform.SetParent(transform);
        obj.transform.localPosition= Vector3.forward * 2;
        DescreseMp(10);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
