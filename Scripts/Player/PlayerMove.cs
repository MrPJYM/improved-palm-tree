using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMove : MonoBehaviour
{
    public Transform tra;
    public Animator ani;
    float h = 0f;
    float v = 0f;
    float r = 0f;
    float moveSpeed;
    float rotateSpeed;
    public bool MoveFlag;
    Vector3 currentRotation;
    public VariableJoystick variableJoystick;
    NavMeshAgent nav;
    Rigidbody rigidbody;
    public AudioSource audio;
    public AudioClip[] audioClips;
    //快霖
    public Vector3 moveDistance;
    public static Vector3 tmpPos;
    //快霖
    void Start()
    {
        moveSpeed = 3f;
        rotateSpeed = 80f;
        currentRotation = transform.rotation.eulerAngles;
        nav = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        MoveFlag = true;

        //快霖
        tmpPos = transform.position;
        //快霖
    }
    public void AttackFrame()
    {
        ani.SetInteger("AttackState", 1);
    }
    public void SkillFrame()
    {
        ani.SetInteger("AttackState", 2);
    }
    void Update()
    {
        h = variableJoystick.Horizontal;
        v = variableJoystick.Vertical;
        if (Mathf.Abs(h) <= 0.1f && Mathf.Abs(v) <= 0.1f)
        {
            h = 0f;
            v = 0f;
        }

        if (h != 0f || v != 0)
        {
            ani.SetInteger("WalkState", 1);
            moveSpeed = 3f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 6f;
                ani.SetInteger("WalkState", 2);
                audio.clip = audioClips[1];
            }
            else
            {
                if (!audio.clip.Equals(audioClips[0]))
                    audio.clip = audioClips[0];
            }
            if (!audio.isPlaying)
                audio.Play();
        }
        else
        {
            ani.SetInteger("WalkState", 0);
            audio.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetInteger("JumpState", 1);
            MoveFlag = false;
        }
        if (h == 0f && v == 0f)
        {
            currentRotation = transform.rotation.eulerAngles;
        }
        float deg = Mathf.Atan2(variableJoystick.Horizontal, variableJoystick.Vertical) * Mathf.Rad2Deg;
        if (h != 0f && v != 0f && MoveFlag == true)
        {
            transform.rotation = Quaternion.Euler(0f, currentRotation.y + deg, 0f);
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            //快霖
            moveDistance = transform.forward * moveSpeed * Time.deltaTime;
            tmpPos = transform.position;
            tmpPos += moveDistance;
            transform.position = tmpPos;
            //快霖
        }
    }
}
