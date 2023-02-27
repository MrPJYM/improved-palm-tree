using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damege : MonoBehaviour
{
    BoxCollider boxCollider;
    Animator playerAni;

    public Vector3 target= new Vector3(0,0,0);
    
    // Start is called before the first frame update
    void Start()
    {
        playerAni = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        if (!gameObject.name.Contains("SnowBall"))
        {
            Set_shrot();
        }
        
    }
    public void Set_shrot() {
        gameObject.AddComponent<BoxCollider>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(1.5f, 0.5f, 0.5f);//공격하는 팔 위치에 박스 콜라이더 추가
        boxCollider.center = new Vector3(0.2f, 0, 0);
    }
    public void Set_SnowBall() {    
        gameObject.SetActive(false);
        target = new Vector3(0, 0, 0);
    }
    private void OnTriggerEnter(Collider _collider)
    {
        if (gameObject.name.Contains("SnowBall"))
        {
            if (_collider.gameObject.CompareTag("Player") && !playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                _collider.BroadcastMessage("DescreseHp", 10);
                gameObject.SetActive(false);
            }
            if (!_collider.gameObject.name.Equals("forearm.R")&&!_collider.gameObject.CompareTag("Monster"))
            {             
                target = transform.position;
                Invoke("Set_SnowBall", 2);

            }
            

        }
        else if (_collider.gameObject.CompareTag("Player")&& ! playerAni.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            //정환
            _collider.BroadcastMessage("DescreseHp", 20);
            
        }

    }

    void Update()
    {
        if (gameObject.name.Contains("SnowBall") && gameObject.activeSelf != false) {
            if (target ==new Vector3 (0,0,0)) {
                target = playerAni.gameObject.transform.position;
            }
            transform.position = Vector3.MoveTowards(gameObject.transform.position, target, 10 * Time.deltaTime) ;
           // transform.position = Vector3.Slerp(gameObject.transform.position, target, 0.02f);
        }
        
    }
}
