using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public int setPlayerHp;
    public int setPlayerMp;
    public int setPlayerGold;

    private void OnEnable()
    {
        PlayerState.instance.p_hp = setPlayerHp;
        PlayerState.instance.p_mp = setPlayerMp;
        PlayerState.instance.gold = setPlayerGold;

        //DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        
    }
}
