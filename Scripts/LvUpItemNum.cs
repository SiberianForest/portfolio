using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvUpItemNum : MonoBehaviour
{
    [SerializeField] private Image lvUpItemImage;
    [SerializeField] private TextMeshProUGUI lvUpItemNum;
    
    public void Initialize(int itemId)
    {
        lvUpItemImage.sprite = Resources.Load<Sprite>(GameDatas.itemImagePath[itemId]);
        lvUpItemNum.text = "0";
    }
    
    public void SetLvUpItemNum(int num)
    {
        lvUpItemNum.text = num.ToString();
    }
}
