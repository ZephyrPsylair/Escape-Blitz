using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject LobbyUI;
    [SerializeField] private GameObject LobbyTilemap;
    [SerializeField] private GameObject MainMenuUI;
    [SerializeField] private GameObject Loading;

    private void Awake()
    {
        LobbyUI.SetActive(false);
        MainMenuUI.SetActive(true);
        Loading.SetActive(false);
        LobbyTilemap.SetActive(false);
    }
    public void Play()
    {
        PhotonNetwork.ConnectUsingSettings();
        Loading.SetActive(true);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
        
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
        
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected To Lobby");
        MainMenuUI.SetActive(false);
        LobbyUI.SetActive(true);
        LobbyTilemap.SetActive(true);
        Loading.SetActive(false);
    }

    public void Back()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        MainMenuUI.SetActive(true);
        LobbyUI.SetActive(false);
        LobbyTilemap.SetActive(false);
    }
}
