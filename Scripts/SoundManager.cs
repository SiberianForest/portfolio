using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    void Start()
    {
        //保存されているボリュームの値を取得
        float value = PlayerPrefs.GetFloat("Volume", 0.5f);
        //スライダーの値を設定
        gameObject.GetComponent<UnityEngine.UI.Slider>().value = value;
        //ボリュームを設定
        AudioListener.volume = value;
    }
    //スライダーが動かされた時に実行する関数
    public void OnValueChanged(float value)
    {
        //スライダーの値をボリュームに設定
        AudioListener.volume = value;
    }
}
