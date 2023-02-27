using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GmaeManager : MonoBehaviour
{
    public static SceneChange SC;
    public static Image DieUi;
    static PlayerObj player_obj;
    [SerializeField]
    Image DieUiSer;
    static public void PlayerDiedDetect(PlayerObj PO)
    {
        player_obj = PO;
        DieUi.gameObject.SetActive(true);        
    }
    public void DieYes()
    {
        player_obj.PlayerReVive();
        SC.MoveScene("Village");
        DieUi.gameObject.SetActive(false);
    }
    public void DieNo()
    {
        player_obj.PlayerReVive();
        SC.MoveScene("Dungeon");
        DieUi.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        SC = GameObject.FindWithTag("Player").GetComponent<SceneChange>();
        DieUi = DieUiSer;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
