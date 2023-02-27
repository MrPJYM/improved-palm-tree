using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj : MonoBehaviour
{
    LightningBoltScript LBS;
    float time = 0f;
    Vector3 v3=new Vector3(0,2,0);
    GameObject[] monster_Obj;
    List<MonsterControl> monster_List;
    Bounds bounds;
    int damage = 20;
    private void Awake()
    {
        monster_Obj = GameObject.FindGameObjectsWithTag("Monster");
        bounds = GetComponent<BoxCollider>().bounds;
        LBS = GetComponent<LightningBoltScript>();
    }
    private void OnEnable()
    {
        LBS.StartPosition = gameObject.transform.position;
        LBS.EndPosition = gameObject.transform.position + v3;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (monster_Obj.Length > 0)
        {
            for (int i = 0; i < monster_Obj.Length; i++)
            {
                if (bounds.Contains(monster_Obj[i].transform.position))
                {
                    monster_Obj[i].BroadcastMessage("HitDamage", damage);
                }
            }
        }
        time += Time.deltaTime;
        if(time>=3f)
        {
            SkillPool.ReturnObject(this);
        }
    }
}
