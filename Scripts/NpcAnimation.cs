using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NpcAnimation : MonoBehaviour
{
    Animator npcAni;
    public GameObject npc;
    GameObject player;
    //public CameraControl camera;
    //public GameObject npcImage;
    void Start()
    {
        npcAni = GetComponent<Animator>();
        //npcImage.SetActive(false);

        //우준, 씬 이동시에 할당 안되는 문제 해결
        player = GameObject.FindGameObjectWithTag("Player");
        //우준
    }

    void Update()
    {
        float Distance = Vector3.Distance(transform.position, player.transform.position);
        // 대화
        if (Distance <= 3f)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("eNPC") || hit.collider.CompareTag("pNPC"))
                    {
                        npcAni.SetInteger("npcAni", 1);
                        npc.transform.rotation = Quaternion.LookRotation(player.transform.position - npc.transform.position);
                        player.transform.rotation = Quaternion.LookRotation(npc.transform.position - player.transform.position);
                        //camera.distance = 6f;
                        //camera.height = 3f;
                        //npcImage.SetActive(false);
                    }
                }
            }
        }
        else if(Distance > 3f)
        {
            npcAni.SetInteger("npcAni", 0);
            //    camera.distance = 10f;
            //    camera.height = 10f;
        }

        //// 이미지 위치
        //if (npcImage != null)
        //{
        //    Vector3 imagePos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,3,0));
        //    npcImage.transform.position = imagePos;
        //}

        //// 가까워지면 느낌표 이미지 나타남 / 멀어지면 사라짐
        //if (Distance <= 5f)
        //{
        //    if (camera.distance == 10f)
        //    {
        //        npcImage.SetActive(true);
        //    }
        //}
        //else if (Distance > 5f)
        //{
        //    npcImage.SetActive(false);
        //}
    }
}
