using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    PlayerObj player;
    public TextMeshProUGUI[] HpTexts;//1=curent 0=Max
    public TextMeshProUGUI[] MpTexts;
    public Image HpImage;
    public Image MpImage;

    // 포션 UI추가 (안재현)
    public int hpItemCount;
    public int mpItemCount;
    public Image hpItemImage;
    public Image mpItemImage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObj>();
    }
    public void UpdateHPMp()
    {
        HpTexts[0].text = player.playerCurrentHp.ToString();
        HpTexts[1].text = player.playerMaxHp.ToString();
        MpTexts[0].text = player.playerCurrentMp.ToString();
        MpTexts[1].text = player.playerMaxMp.ToString();
        HpImage.fillAmount = (float)player.playerCurrentHp / (float)player.playerMaxHp;
        MpImage.fillAmount = (float)player.playerCurrentMp / (float)player.playerMaxMp;

        HpTexts[2].text = hpItemCount.ToString();
        MpTexts[2].text = mpItemCount.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateHPMp();
    }
}
