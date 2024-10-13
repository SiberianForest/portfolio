using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject loginPage;
    void Start()
    {
        loginPage.SetActive(true);
    }
}
