using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CreateAccountTransition : MonoBehaviour
{
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject registerPage;
    [SerializeField] private GameObject createAccountButton;
    [SerializeField] private CreateAccount createAccount;
    [SerializeField] private FooterText footerText;
    [SerializeField] private InputField idInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private InputField newIdInputField;
    [SerializeField] private InputField newPasswordInputField;
    [SerializeField] private GuestCreateApi guestCreateApi;
    [SerializeField] private GuestLoginApi guestLoginApi;
    [SerializeField] private UserCreateApi userCreateApi;
    [SerializeField] private UserCheckApi userCheckApi;
    private bool isClick = false;
    public bool check = true;
    
    public void CreateAccountButton()
    {
        if(isClick) return;
        createAccountButton.GetComponent<Button>().interactable = false;
        StartCoroutine(CreateAccount());
    }
    private IEnumerator CreateAccount()
    {
        isClick = true;
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(CreateAccountRequest());
        isClick = false;
        createAccountButton.GetComponent<Button>().interactable = true;
    }

    private IEnumerator CreateAccountRequest()
    {
        GameDatas.userId = newIdInputField.text;
        GameDatas.password = newPasswordInputField.text;
        if (createAccount.CheckInputCondition())
        {
            //APIを叩いてidとpwをuserテーブルの新規レコードに追加してバックエンドで保存する。
            //guest\create
            yield return StartCoroutine(guestCreateApi.Run(newIdInputField.text, newPasswordInputField.text));
            yield return StartCoroutine(guestLoginApi.Run(newIdInputField.text, newPasswordInputField.text));
            yield return StartCoroutine(userCheckApi.Run());
            check = userCheckApi.GetCheck();
            if (!check)
            {
                yield return StartCoroutine(userCreateApi.Run());
                registerPage.SetActive(false); 
                loginPage.SetActive(true); 
                footerText.ClearFooterText();
                idInputField.text = GameDatas.userId; 
                passwordInputField.text = GameDatas.password;
            }
            else
            {
                newIdInputField.text = "";
                IdDuplicatedComment();
            }
        }
    }
    
    private void IdDuplicatedComment()
    {
        var text = "アカウント作成に失敗しました。\nIDが重複しています。";
        footerText.WriteFooterText(text, 16);
        createAccountButton.GetComponent<Button>().interactable = true;
    }
    
    public void BackToLoginPage()
    {
        //サウンド
        gameObject.GetComponent<AudioSource>().Play();
        registerPage.SetActive(false);
        loginPage.SetActive(true);
    }
}
