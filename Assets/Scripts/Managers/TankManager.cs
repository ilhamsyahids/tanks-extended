using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

[Serializable]
public class TankManager : NetworkBehaviour
{
    [SyncVar(hook = nameof(SyncPlayerColor))] private Color PlayerColor;
    [SyncVar(hook = nameof(SyncPlayerName))] private string PlayerName;

    //[SyncVar(hook = nameof(SyncSpawnPoint))]
    public Transform m_SpawnPoint;

    //[HideInInspector] [SyncVar] public string m_PlayerName;
    [HideInInspector] [SyncVar] public int m_PlayerNumber;
    [HideInInspector] [SyncVar] public string m_ColoredPlayerText;
    [HideInInspector] [SyncVar(hook = nameof(SyncWins))] private int m_Wins;

    [SyncVar(hook = nameof(SyncIsReady))] private bool m_isReady;

    private TankMovement m_Movement;
    private TankShooting m_Shooting;
    private GameObject m_CanvasGameObject;

    public int numberOfWins
    {
        get { return m_Wins; }
        set { SyncWins(m_Wins, value); }
    }

    public bool isReady
    {
        get { return m_isReady; }
        set { CmdIsReady(value); }
    }

    public string playerName
    {
        get { return PlayerName; }
        set { CmdIsPlayerName(value); }
    }

    public override void OnStartServer()
    {
        Setup();
        base.OnStartServer();
    }

    public override void OnStartClient()
    {
        Setup();
        base.OnStartClient();
    }

    private void Setup()
    {
        SyncPlayerColor(PlayerColor, PlayerColor);
        SyncPlayerName(PlayerName, PlayerName);
        m_isReady = false;
        m_Movement = gameObject.GetComponent<TankMovement>();
        m_Shooting = gameObject.GetComponent<TankShooting>();
        m_CanvasGameObject = gameObject.GetComponentInChildren<Canvas>().gameObject;
        m_Movement.m_PlayerNumber = 1;
        m_Shooting.m_PlayerNumber = 1;
        //m_PlayerName = PlayerNameHandler.playerName;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">" + PlayerName + "</color>";
        //m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";
        m_SpawnPoint = GameObject.Find("SpawnPoint" + m_PlayerNumber).GetComponent<Transform>();
    }

    private void SyncPlayerName(string OldValue, string NewValue)
    {
        PlayerName = NewValue;
        //m_PlayerName = NewValue;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">" + PlayerName + "</color>";
    }

    public void Setup(String playerName, Color color)
    {
        SyncPlayerColor(PlayerColor, color);
        //SyncPlayerName(PlayerName, playerName);
    }

    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }

    public void Reset()
    {
        gameObject.transform.position = m_SpawnPoint.position;
        gameObject.transform.rotation = m_SpawnPoint.rotation;

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    //Server and sync methods
    private void SyncPlayerColor(Color OldValue, Color NewValue)
    {
        PlayerColor = NewValue;
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = PlayerColor;
        }
    }

    private void SyncIsReady(bool OldValue, bool NewValue)
    {
        m_isReady = NewValue;
    }

    private void SyncWins(int OldValue, int NewValue)
    {
        m_Wins = NewValue;
    }


    [Command]
    private void CmdIsPlayerName(string value)
    {
        SyncPlayerName(PlayerName, value);
    }

    [Command]
    private void CmdIsReady(bool value)
    {
        SyncIsReady(m_isReady, value);
    }

    [Command]
    private void CmdUpdateNumberWinner(int value)
    {
        SyncWins(m_Wins, value);
    }
}
