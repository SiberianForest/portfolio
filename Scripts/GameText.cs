using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameText : MonoBehaviour
{
    [SerializeField] private InGamePage inGamePage;
    private GameObject flyText;
    [SerializeField] private GameObject activeCharacterAtInGamePage;
    
    void Update()
    {
        if(flyText != null)
        {
            flyText.transform.localPosition += new Vector3(0.0f, 0.1f * Time.deltaTime, 0.0f);
            flyText.GetComponent<TextMeshProUGUI>().color -= new Color(0.0f, 0.0f, 0.0f, 0.5f * Time.deltaTime);
        }
    }

    public void FlyText()
    {
        //フライテイストの生成
        flyText = Instantiate(gameObject, activeCharacterAtInGamePage.transform);
        flyText.tag = "GameText";
        var num = inGamePage.CalcAddMoney();
        //フライテキストの記述
        flyText.GetComponent<TextMeshProUGUI>().text = num.ToString();
        flyText.transform.localPosition = new Vector3(-0.6f, 0.6f, 0.0f);
        flyText.SetActive(true);
        //フライテキストの削除
        Destroy(flyText, 1.0f);
    }
    

    
}
