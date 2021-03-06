using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerTanks : NetworkManager
{
    public GameManager m_GameManager;
    public Transform[] m_spawnPoint;
    //public Transform m_spawnPoint2;
    public Color color1;
    public Color color2;

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (!m_GameManager.GameStart)
            base.OnServerConnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        GameObject player;

        if (numPlayers >= m_spawnPoint.Length)
        {
            return;
        }

        player = Instantiate(playerPrefab, m_spawnPoint[numPlayers].position, m_spawnPoint[numPlayers].rotation) as GameObject;
        TankManager tankManager = player.GetComponent<TankManager>();
        string playerName = PlayerNameHandler.playerName;
        tankManager.m_PlayerNumber = numPlayers + 1;
        tankManager.Setup(playerName, Random.ColorHSV(0f, 1f, 0.9f, 1f, 1f, 1f));
        //if (numPlayers == 0)
        //{
        //    player = Instantiate(playerPrefab, m_spawnPoint1.position, m_spawnPoint1.rotation) as GameObject;
        //    TankManager tankManager = player.GetComponent<TankManager>();
        //    tankManager.m_PlayerNumber = numPlayers + 1;
        //    tankManager.Setup(m_spawnPoint1, color1);
        //}
        //else
        //{
        //    player = Instantiate(playerPrefab, m_spawnPoint2.position, m_spawnPoint2.rotation) as GameObject;
        //    TankManager tankManager = player.GetComponent<TankManager>();
        //    tankManager.m_PlayerNumber = numPlayers + 1;
        //    tankManager.Setup(m_spawnPoint2, color2);
        //}
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        m_GameManager.ResetCamera();
        base.OnClientDisconnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkServer.RemovePlayerForConnection(conn, true);
        m_GameManager.ResetCamera();
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        Debug.Log("Stop Server");
        NetworkServer.DisconnectAllConnections();
        base.OnStopServer();
    }

    public override void OnStopHost()
    {
        Debug.Log("Stop Server Host");
        NetworkServer.DisconnectAllConnections();
        base.OnStopHost();
    }
}
