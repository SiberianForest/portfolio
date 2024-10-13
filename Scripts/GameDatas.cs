using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class GameDatas : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////
    /// プレイユーザー情報/////////////////////////////////////
    public static string userId;// = "user1";
    public static string password;// = "1234";
    public static string userName;// = "user1";
    public static int money;// = 999;
    public static int[] itemsNum;// = {1000, 1000, 1000, 0};//アイテム1、アイテム2、アイテム3//アイテム4＝キャラガチャの重複でもらえる
    public static int[] charactersLv;// = {1, 0, 0, 1};//初期キャラ、R,SR,SSR
    public static int[] charactersStatus;// = {1, 1, 4, 9};//初期キャラ、R,SR,SSR
    public static int[] nextCharactersStatus;
    public static int[] itemGachaResults;
    public static int characterGachaResult;
    /// デフォルトユーザー情報/////////////////////////////////////
    public static int initialMoney;// = 100;
    public static int[] initialItemsNum;// = {0, 0, 0, 0};//アイテム1、アイテム2、アイテム3//アイテム4＝キャラガチャの重複でもらえる
    public static int[] initialCharactersLv;// = {1, 0, 0, 0};//初期キャラ、R,SR,SSR
    //system/////////////////////////////////////
    public static int itemGachaCost;// = 10;//アイテムガチャコスト
    public static int characterGachaCost;// = 20;  //キャラガチャコスト
    public static float[] itemRarelity;// = {0.6f, 0.3f, 0.1f};  //R,SR,SSR//足して1になるように
    public static float[] characterRarelity;// = {0.5f, 0.4f, 0.1f};  //R,SR,SSR//足して1になるように
    public static int[][] lvUpRequireCase;// = new int[][]
    public static int[][] lvUpRequireCaseEx;// = new int[][]
    //アイテム画像のパスのセット
    public static string[] itemImagePath;// = { "Images/item1", "Images/item2", "Images/item3", "Images/lvUp1" };
    //キャラクター画像のパスのセット
    public static string[] characterImagePath;// ={ "Images/character1", "Images/character2", "Images/character3", "Images/character4" };
    //数量制限
    public static int moneyLimit;// = 99999;
    public static int itemsNumLimit;// = 999;
    public static int charactersLvLimit;// = 999;
    //////////////////////////////////////////////////////////////////////////

}
