using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField PlayerNameInputField;
    [SerializeField] private Button ConfirmButton;

    private void Start()
    {
        ConfirmButton.interactable = false;

        PlayerNameInputField.onValueChanged.AddListener(Interactable);
    }

    private void Interactable(string arg0)
    {
        ConfirmButton.interactable = arg0.Length >= 5;
    }

    public void SetName()
    {
        PhotonNetwork.NickName = PlayerNameInputField.text;
        Debug.Log("PlayerName Confirmed");
    }
}
