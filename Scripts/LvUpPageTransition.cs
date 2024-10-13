using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvUpPageTransition : MonoBehaviour
{
    [SerializeField] private GameObject myPage;
    [SerializeField] private GameObject characterLvUp;
    [SerializeField] private CharacterLvUpPage characterLvUpPage;
    [SerializeField] private Button lvUpButton;
    [SerializeField] private CharacterLvUpButton characterLvUpButton;
    [SerializeField] private FooterText footerText;
    [SerializeField] private MyPage myPageScript;
    [SerializeField] private TransitionScript transitionScript;
    
    public void MyPageFromCharacterLvUpPage()
    {
        StartCoroutine(LoadMyPageFromCharacterLvUpPage());
    }
    
    private IEnumerator LoadMyPageFromCharacterLvUpPage()
    {
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        characterLvUp.SetActive(false);
        yield return StartCoroutine(transitionScript.TransitionPage());
        myPage.SetActive(true);
        //残金確認
        myPageScript.CheckMoney();
        //フッターのテキストをクリア
        footerText.ClearFooterText();
        myPageScript.CheckItems();
    }
}
