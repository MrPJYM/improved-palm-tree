using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleTon<T> where T : class, new()
{
    private static T inst;
    public SingleTon() { }
    public static T instance
    {
        get
        {
            if (inst == null)
                inst = new T();
            return inst;
        }
    }
}
public class PlayerState:SingleTon<PlayerState>
{
    public int p_hp;
    public int p_mp;
    public string p_name;
    public float p_attackSpeed;
    public float p_moveSpeed;
    public float p_attackDamage;
    public float p_sieldVar;
    public int gold;
}

public class BossState : SingleTon<BossState>
{
    public string bm_name;
    public int bm_hp;
    public float bm_attackSpeed;
    public float bm_attackDamage;

    public void Test()
    {
        Debug.Log(bm_hp);
    }
}


