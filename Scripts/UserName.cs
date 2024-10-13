using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserName : MonoBehaviour
{
    private TextMeshProUGUI statusText;
    public void WriteHeaderUserName()
    {
        //ユーザー名の更新
        statusText = GetComponent<TextMeshProUGUI>();
        statusText.text = "ID: " + GameDatas.userId;
    }
}
