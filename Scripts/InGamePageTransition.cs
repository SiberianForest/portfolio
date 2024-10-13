using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePageTransition : MonoBehaviour
{
    [SerializeField] private GameObject myPage;
    [SerializeField] private GameObject inGame;
    [SerializeField] private Button pushButton;
    [SerializeField] private InGamePage inGamePage;
    [SerializeField] private FooterText footerText;
    [SerializeField] private MyPage myPageScript;
    [SerializeField] private TransitionScript transitionScript;
    [SerializeField] private Push push;
    
    public void MyPageFromInGame()
    {
        StartCoroutine(BackFromInGame());
    }

    private IEnumerator BackFromInGame()
    {
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        inGamePage.isInProgress = true;
        yield return StartCoroutine(push.RunInGame());
        inGame.SetActive(false);
        yield return StartCoroutine(transitionScript.TransitionPage());
        myPage.SetActive(true);
        //残金確認
        myPageScript.CheckMoney();
        //フッターのテキストをクリア
        footerText.ClearFooterText();
        inGamePage.clickCount = 0;
        myPageScript.CheckItems();
    }
}
