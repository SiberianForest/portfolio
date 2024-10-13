using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class SendActiveCharacterInGame
{
    public int activeCharacter;
    public int clickCount;
}

public class Push : MonoBehaviour
{
    [SerializeField] private InGamePage inGamePage;
    [SerializeField] private Money money;
    [SerializeField] private GameText gameText;
    [SerializeField] private InGameApi inGameApi;
    public float time;
    void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
        inGamePage.isInProgress = false;
    }
    
    void Update()
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            time += Time.deltaTime;
            if (time >= 5.0f)
            { 
                time = 0;
                if(inGamePage.clickCount < 1 || inGamePage.isInProgress)
                {
                    return;
                }
                else
                {
                    inGamePage.isInProgress = true;
                    if(GameDatas.money < GameDatas.moneyLimit)
                    {
                        StartCoroutine(RunInGame());
                    }
                }
            }
        }
    }
    
    //PUSHすると表示だけmoneyが増える
    public void PushButton()
    {
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        //フライテキスト表示
        gameText.FlyText();
        if(inGamePage.currentMoney < GameDatas.moneyLimit)
        {
            //クリック回数の更新
            inGamePage.clickCount++;
            //表示所持金の更新
            inGamePage.currentMoney += inGamePage.CalcAddMoney();
            //所持金の更新
            money.WriteHeaderMoney(inGamePage.currentMoney);
        }
    }
    
    public IEnumerator RunInGame()
    {
        //クリック数確定
        var sendClickCount = inGamePage.clickCount;
        //APIでの送受信
        yield return StartCoroutine(inGameApi.Run(inGamePage.activeCharacter + 1, sendClickCount));
        //クリック回数の補正
        inGamePage.clickCount -= sendClickCount;
        if (inGamePage.currentMoney <= GameDatas.money)
        {
            //表示所持金の更新
            inGamePage.currentMoney = GameDatas.money;
            //所持金の更新
            money.WriteHeaderMoney(inGamePage.currentMoney);
        }
        //時間の初期化
        time = 0;
        //API呼び出しの状態をリセット
        inGamePage.isInProgress = false;
    }
}
