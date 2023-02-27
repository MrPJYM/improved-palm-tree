using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerJump : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<NavMeshAgent>().enabled = false;
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.gameObject.GetComponent<PlayerMove>().MoveFlag = true;
        animator.SetInteger("JumpState", 0);
        animator.GetComponent<NavMeshAgent>().enabled = true;
    }
}
