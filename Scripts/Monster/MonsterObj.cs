using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class MonsterObj : MonoBehaviour
{
    public float m_movespeed;
    public float m_atkSpeed;//���ݼӵ�
    public float m_monsterRR;//���Ͱ� �÷��̾ �ν��ϴ� ����(monster_recognition_range)
    public float m_monsterAR;//���� ���� ����(monster_attack_range)

    public string m_name;
    public float m_hp;
    public float m_attackMoveSpeed;//�����Ϸ� ���� ���ǵ�
    public int m_damage; //���� ������
    public int m_defens; //����
    public string m_atkType;//����Ÿ��(���Ÿ�/�ٰŸ�)

    public int m_level; //����
    public int m_exp; //����ġ

}
