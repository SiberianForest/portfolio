using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LogoutTransition : MonoBehaviour
{
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject myPage;
    [SerializeField] private GameObject header;
    [SerializeField] private GameObject footer;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private InputField idInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private HeaderItems headerItems;
    [SerializeField] private HeaderCharacters headerCharacters;
    [SerializeField] private TransitionScript transitionScript;
    [SerializeField] private LogoutApi logoutApi;
    [SerializeField] private MyPage myPageScript;
    
    private bool isClick = false;
    public void Logout()
    {
        if(isClick) return;
        loginButton.GetComponent<Button>().interactable = false;
        StartCoroutine(LoadLoginPage());
    }

    private IEnumerator LoadLoginPage()
    {
        isClick = true;
        gameObject.GetComponent<AudioSource>().Play();
        yield return StartCoroutine(logoutApi.Run());
        myPageScript.OffGachaResult10();
        myPage.SetActive(false);
        header.SetActive(false);
        footer.SetActive(false);
        headerItems.DeleteItems();
        headerCharacters.DeleteCharacters();
        idInputField.text = "";
        passwordInputField.text = "";
        yield return StartCoroutine(transitionScript.TransitionPage());
        loginButton.GetComponent<Button>().interactable = true;
        loginPage.SetActive(true);
        isClick = false;
    }
}
