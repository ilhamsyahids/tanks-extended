using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameHandler : MonoBehaviour
{
    public static string playerName;
    public static string map;
    public static string weapon;
    public GameObject NameInputField;
    public GameObject MapDropdown;
    public GameObject WeaponDropdown;

    public void StoreName()
    {
        playerName = NameInputField.GetComponent<TMP_InputField>().text;
        if(MapDropdown.GetComponent<TMP_Dropdown>().value == 0)
        {
            map = "Mojave Desert";
        }
        else if (MapDropdown.GetComponent<TMP_Dropdown>().value == 1)
        {
            map = "Snowy Westfjords";
        }
        else if (MapDropdown.GetComponent<TMP_Dropdown>().value == 2)
        {
            map = "Boquete Forest";
        }

        if (WeaponDropdown.GetComponent<TMP_Dropdown>().value == 0)
        {
            weapon = "Laser Beam";
        }
        else if (WeaponDropdown.GetComponent<TMP_Dropdown>().value == 1)
        {
            weapon = "Ordnance Pounder";
        }
        else if (WeaponDropdown.GetComponent<TMP_Dropdown>().value == 2)
        {
            weapon = "Blue Cockerill";
        }
    }
}
