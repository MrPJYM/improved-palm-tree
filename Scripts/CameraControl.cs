using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    private float rotationDamping = 3.0f;
    private float heightDamping = 2.0f;

    //우준, 씬 이동시에 할당 안되는 문제 해결
    private void Awake()
    {
        if (!gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
        }
    }
    //우준
    void LateUpdate()
    {
        if (!target)
            return;

        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;
        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime); // 부드럽게
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z); // 카메라 포지션 설정
        transform.LookAt(target); // 플레이어가 보는 방향을 따라 회전시킨다.
    }
}
