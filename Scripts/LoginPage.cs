using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject myPage;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject footer;
    [SerializeField] private GameObject registerPage;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject createAccountButton;
    [SerializeField] private InputField newIdInputField;
    [SerializeField] private InputField newPasswordInputField;
    [SerializeField] private InputField newPasswordInputFieldCheck;
    [SerializeField] private FooterText footerText;
    [SerializeField] private GetHeaderData getHeaderData;
    [SerializeField] private TransitionScript transitionScript;
    [SerializeField] private CreateAccount createAccount;
    [SerializeField] private CreateAccountTransition createAccountTransition;
    [SerializeField] private MyPage myPageScript;
    [SerializeField] private InputField idInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private GetHeaderApi getHeaderApi;
    [SerializeField] private GuestLoginApi guestLoginApi;
    [SerializeField] private UserStartApi userStartApi;
    [SerializeField] private UserCheckApi userCheckApi;
    [SerializeField] private UserCreateApi userCreateApi;
    private bool isClick = false;
    public bool started;
    
    public void Login()
    {
        if(isClick) return;
        loginButton.GetComponent<Button>().interactable = false;
        StartCoroutine(LoginRequest());
    }

    private IEnumerator LoginRequest()
    {
        isClick = true;
        started = false;
        var userId = idInputField.text;
        var password = passwordInputField.text;
        //ログイン処理
        //APIを叩いてidとpwを渡す。(→所持系データをロードしてくるのはgetHeader)
        //guest\login,user\startの順に叩く。//GameDatasにidとpwを保存する。
        if (!createAccountTransition.check)
        {
            yield return StartCoroutine(userStartApi.Run());
        }
        else
        {
            yield return StartCoroutine(guestLoginApi.Run(userId, password));
            yield return StartCoroutine(userCheckApi.Run());
            yield return StartCoroutine(userStartApi.Run());
        }
        {
            
        }
        started = userStartApi.GetStarted();
        if (started)
        {
            StartCoroutine(LoadMyPage());
            newIdInputField.text = "";
            newPasswordInputField.text = "";
            newPasswordInputFieldCheck.text = "";
        }
        isClick = false;
    }

    private IEnumerator LoadMyPage()
    {
        yield return StartCoroutine(getHeaderApi.Run());
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(transitionScript.TransitionPage());
        loginPage.SetActive(false);
        myPage.SetActive(true);
        //残金確認
        myPageScript.CheckMoney(); 
        header.SetActive(true);
        footer.SetActive(true);
        getHeaderData.WriteHeaderUserDatas();
        myPageScript.CheckItems();
    }
    
    public void LoadCreateAccountPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        footer.SetActive(true);
        footerText.WriteFooterText(createAccount.RegisterToFooterText(), 16);
        createAccountButton.GetComponent<Button>().interactable = true;
    }
}
