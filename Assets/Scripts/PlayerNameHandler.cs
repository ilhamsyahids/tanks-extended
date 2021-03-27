using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayerNameHandler : MonoBehaviour
{
    public string playerName;
    public static string map;
    public static string weapon;
    public GameObject NameInputField;
    public GameObject MapDropdown;
    public GameObject WeaponDropdown;

    void Awake()
    {
       DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        playerName = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 5)
                                            .Select(s => s[Random.Range(0, s.Length)]).ToArray());
        NameInputField.GetComponent<TMP_InputField>().text = playerName;
    }

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
