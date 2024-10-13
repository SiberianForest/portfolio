using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePageButton : MonoBehaviour
{
    [SerializeField] private GameObject myPage;
    [SerializeField] private GameObject inGame;
    [SerializeField] private Button pushButton;
    [SerializeField] private InGamePage inGamePage;
    [SerializeField] private TransitionScript transitionScript;
    [SerializeField] private MyPage myPageScript;
    
    public void InGamePage()
    {
        StartCoroutine(LoadInGamePage());
    }
    
    private IEnumerator LoadInGamePage()
    {
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(transitionScript.TransitionPage());
        myPageScript.OffGachaResult10();
        myPage.SetActive(false);
        inGame.SetActive(true);
        //キャラクターのロード
        inGamePage.CharacterLoadInGamePage();
        //PUSHボタンを非アクティブ
        pushButton.interactable = false;
        //クリック回数の初期化
        inGamePage.clickCount = 0;
        //表示所持金の更新
        inGamePage.currentMoney = GameDatas.money;
    }
}
