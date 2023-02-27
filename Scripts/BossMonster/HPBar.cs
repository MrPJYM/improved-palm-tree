using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image hpBar;
    string hpBarText;
    void Start()
    {

    }

    public void bossHPUpdate()
    {
        int hp = BossState.instance.bm_hp;
        hpBar.fillAmount = hp / 500f;
        hpBarText = string.Format("{0} / 500", hp);
    }
    void Update()
    {
        bossHPUpdate();
    }
}