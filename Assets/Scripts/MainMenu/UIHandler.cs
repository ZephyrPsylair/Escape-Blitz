using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;

public class UIHandler : MonoBehaviourPunCallbacks
{
    public TMP_InputField CreateRoomInputField;
    public TMP_InputField JoinRoomInputField;
    public Button CreateButton;
    public Button JoinButton;
    public GameObject Loading;

    private void Start()
    {
        CreateButton.interactable = false;
        JoinButton.interactable = false;

        CreateRoomInputField.onValueChanged.AddListener(CreateButtonInteractable);
        JoinRoomInputField.onValueChanged.AddListener(JoinButtonInteractable);
    }

    private void CreateButtonInteractable(string arg0)
    {   
        CreateButton.interactable = arg0.Length >= 5;
    }

    private void JoinButtonInteractable(string arg0)
    {
        JoinButton.interactable = arg0.Length >= 5;
    }


    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRoomInputField.text, new RoomOptions { MaxPlayers = 2 }, null);
        Loading.SetActive(true);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinRoomInputField.text, null);
        Loading.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("RoomFailed : " + returnCode + " Message : " + message);
        PhotonNetwork.JoinRandomRoom();
    }
}
