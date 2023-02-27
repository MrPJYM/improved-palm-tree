using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
    
{
   
    void Start()
    {
        PlayerMove.tmpPos = transform.position;
        
    }
    //씬 이동 중간 로딩 화면 호출 함수
    public void MoveScene(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName);
        if (SceneManager.GetActiveScene().name == "Village")
        {
            transform.position = new Vector3(-2.04f, 0, -26.6f);
        }
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            transform.position = new Vector3(-9.49f, 0, 40.7f);
        }
        Debug.Log(_SceneName);
    }
    //Portal에 물리 충돌하면 각각의 씬으로 이동,
    //씬 이동 후 transform.position은 Portal에서 일정거리 떨어진 위치로 초기화
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("villagePortal"))
        {
            Debug.Log(other.gameObject.tag);
            //PlayerMove.tmpPos = new Vector3(-2.04f, 0, -26.6f);
            //transform.position = PlayerMove.tmpPos;
            transform.rotation = Quaternion.Euler(0, 0, 0f);
            MoveScene("Dungeon");
            Debug.Log("Dungeon 위치" + transform.position);
            
        }
        if (other.gameObject.CompareTag("dungeonPortal"))
        {
            //PlayerMove.tmpPos = new Vector3(-9.49f, 0, 40.7f);
            //transform.position = PlayerMove.tmpPos;
            transform.rotation = Quaternion.Euler(0, 90, 0f);
            MoveScene("Village");
            Debug.Log("Village 위치" + transform.position);
        }
    }
}
