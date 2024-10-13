using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public void WriteHeaderMoney(int money)
    {
        //所持金の更新
        gameObject.GetComponent<TextMeshProUGUI>().text = "MONEY: " + money.ToString();
    }
}