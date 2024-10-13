using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;


public class GetHeaderData : MonoBehaviour
{
    [SerializeField] private HeaderItems headerItems;
    [SerializeField] private HeaderCharacters headerCharacters;
    [SerializeField] private Money moneyScript;
    [SerializeField] private UserName userNameScript;

    public void WriteHeaderUserDatas()
    {
        userNameScript.WriteHeaderUserName();
        moneyScript.WriteHeaderMoney(GameDatas.money);
        headerItems.WriteHeaderItems();
        headerCharacters.WriteHeaderCharacters();
    }
}
