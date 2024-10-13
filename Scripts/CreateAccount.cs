using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

[System.Serializable]
public class SendUserData
{
    public string userId;
    public string password;
}

public class CreateAccount : MonoBehaviour
{
    [SerializeField] private InputField newIdInputField;
    [SerializeField] private InputField newPasswordInputField;
    [SerializeField] private InputField newPasswordInputFieldCheck;
    [SerializeField] private FooterText footerText;
    
    //アカウント作成ボタンを押したときにチェックが通ればアカウントを作成する
    public bool CheckInputCondition()
    {
        //IDとパスワードのチェック
        if (CheckId(newIdInputField.text) && CheckPassword(newPasswordInputField.text))
        {
            if (newPasswordInputField.text != newPasswordInputFieldCheck.text)
            {
                var text = "アカウント作成に失敗しました。\nパスワードが一致しません。";
                footerText.WriteFooterText(text, 16);
                return false;
            }
            return true;
        }
        else
        {
            //IDとパスワードのチェックに引っかかったときの処理
            var text = "アカウント作成に失敗しました。\nIDまたはパスワードが条件を満たしていません。";
            footerText.WriteFooterText(text, 16);
            return false;
        }
    }
    
    //フッターにidとpwの条件を表示
    public string RegisterToFooterText()
    {
        string text = "ID: 4〜16文字　大文字、小文字、半角数字、アンダースコア(_)が使用できます。\n";
        text += "PW: 8〜32文字　 大文字、小文字、半角数字のうち全ての種類を含んでください。";
        return text;
    }

    //IDについて要件を満たすかチェック
    private bool CheckId(string id)
    {
        //IDの長さが4文字以上かつ16文字以下で- アルファベットの大文字、アルファベットの小文字、半角数字、アンダーバーのみで構成されているか
        if (id.Length >= 4 && id.Length <= 16 &&
            System.Text.RegularExpressions.Regex.IsMatch(id, @"^[a-zA-Z0-9_]+$")) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //パスワードについて要件を満たすかチェック
    public bool CheckPassword(string password)
    {
        //パスワードの長さが8文字以上かつ32文字以下で- アルファベットの大文字、アルファベットの小文字、半角数字のみで構成され、全ての種類を含むか
        if (password.Length >= 8 && password.Length <= 32 && System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z0-9]+$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
