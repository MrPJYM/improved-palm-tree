using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent nav;
    public Transform monster;
    //public Transform target;
    Vector3 end;
    float moveSpeed;
    void Start()
    {
        end = Vector3.zero;
        moveSpeed = 2.0f;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //nav.SetDestination(hit.point);
                if (hit.collider.CompareTag("Monster"))
                {
                    end = hit.collider.transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, end, Time.deltaTime * moveSpeed);
                    
                }
                else
                {
                    nav.destination = hit.point;
                    //transform.position = nav.destination;
                }
            }
        }
       
        //float distance = Vector3.Distance(transform.position, end);
        //if (distance <= 2f)
        //{
        //    end = transform.position;
        //}
        if (transform.position == nav.destination)
        {
            animator.SetInteger("aniIndex", 0);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetInteger("aniIndex", 2);
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                animator.SetInteger("aniIndex", 0);
            }
        }
        else if (transform.position != nav.destination)
        {
            animator.SetInteger("aniIndex", 1);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                nav.destination = transform.position;
                animator.SetInteger("aniIndex", 2);
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                animator.SetInteger("aniIndex", 0);
            }
        }
    }
}
