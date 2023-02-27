using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterStateManager : MonoBehaviour
{
    public int setBossHp;
    public float setBossattackSpeed;
    public float setBossattackDamage;
    private void OnEnable()
    {
        BossState.instance.bm_hp = setBossHp;
        BossState.instance.bm_attackSpeed = setBossattackSpeed;
        BossState.instance.bm_attackDamage = setBossattackDamage;
        //DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
