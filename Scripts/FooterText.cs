using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FooterText : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textView;
    
    //フッターの書き込み;
    public void WriteFooterText(string textToWrite,int fontSize)
    {
        textView.fontSize = fontSize;
        textView.text = textToWrite;
    }
    
    public void ClearFooterText()
    {
        textView.text = "";
    }

}
