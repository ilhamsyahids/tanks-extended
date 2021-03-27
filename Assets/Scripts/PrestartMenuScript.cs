using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrestartMenuScript : MonoBehaviour
{
    public bool isOffline = true;
    public PlayerNameHandler m_PlayerPrefs;

    public void OnChangeToggle(bool value)
    {
        isOffline = value;
    }

    public void PlayGame()
    {
        int scene = 1;
        if (isOffline)
        {
            scene++;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scene);
    }

    public void CancelPreparation()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
