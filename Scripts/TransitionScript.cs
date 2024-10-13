using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    [SerializeField] GameObject transitionImage;
    private float counter;
    private float waitTime = 1.0f;
    void Start()
    {
        counter = 0.0f;
    }
    
    void Update()
    {
        if(transitionImage != null)
        {
            counter += Time.deltaTime;
            if (counter < waitTime * 0.5f)
            {
                //transitionのimageのalpha値を徐々に上げる
                float alpha = Mathf.Clamp(counter * 2.0f/waitTime, 0.0f, 1.0f);
                transitionImage.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
            }
            else if(counter > waitTime * 0.5f && counter < waitTime)
            {
                //transitionのimageのalpha値を徐々に下げる
                float alpha = Mathf.Clamp(2f - counter * 2.0f/waitTime, 0.0f, 1.0f);
                transitionImage.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
            }
        }
    }
    
    public IEnumerator TransitionPage()
    {
        GameObject transition = Instantiate(Resources.Load<GameObject>("Prefabs/Transition"));
        transition.SetActive(true);
        transition.transform.SetAsLastSibling();
        Destroy(transition, waitTime);
        yield return new WaitForSeconds(waitTime * 0.5f);
    }
}
