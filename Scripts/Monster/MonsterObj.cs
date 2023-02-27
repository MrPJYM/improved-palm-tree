using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MonsterObj : MonoBehaviour
{
    public float m_movespeed;
    public float m_atkSpeed;//공격속도
    public float m_monsterRR;//몬스터가 플레이어를 인식하는 범위(monster_recognition_range)
    public float m_monsterAR;//몬스터 공격 범위(monster_attack_range)

    public string m_name;
    public float m_hp;
    public float m_attackMoveSpeed;//공격하러 갈때 스피드
    public int m_damage; //들어가는 데미지
    public int m_defens; //방어력
    public string m_atkType;//공격타입(원거리/근거리)

    public int m_level; //레벨
    public int m_exp; //경험치

}
