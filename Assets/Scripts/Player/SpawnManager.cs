using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject PressurePrefab;
    public GameObject LeverPrefab;
    public GameObject SceneCamera;
    [SerializeField] private GameObject Spawnlocation;

    void Start()
    {
        SceneCamera.SetActive(false);
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name, Spawnlocation.transform.position , Quaternion.identity,0);
        PhotonNetwork.Instantiate(PressurePrefab.name, PressurePrefab.transform.position , Quaternion.identity,0);
    }
}
