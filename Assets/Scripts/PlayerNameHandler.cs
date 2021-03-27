using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayerNameHandler : MonoBehaviour
{
    public static string playerName = "Player1";
    public static string playerName2 = "Player2";
    public static string map = "maps1";
    public static string weapon;

    void Awake()
    {
       DontDestroyOnLoad(this.gameObject);
    }

    public void OnChangeMap(int value)
    {
        if (value == 0)
        {
            map = "maps1";
        }
        else // 1
        {
            map = "maps2";
        }
    }

    public void OnChangePlayerName1(string value)
    {
        playerName = value;
    }

    public void OnChangePlayerName2(string value)
    {
        playerName2 = value;
    }

    public void OnChangeWeapon(int value)
    {
        if (value == 0)
        {
            weapon = "Default";
        } else
        {
            weapon = "Special";
        }
    }
}
