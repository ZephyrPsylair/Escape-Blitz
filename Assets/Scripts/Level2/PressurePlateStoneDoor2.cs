using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PressurePlateDoor2 : MonoBehaviourPun
{
    [SerializeField] private PhotonView pv;
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject Stone;

    private void Start()
    {
        if (photonView.IsMine)
        {
            Anim = GetComponent<Animator>();
            Stone = GameObject.Find("ThirdStoneDoor");
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                Anim.SetBool("IsPressing", true);
                Stone.SetActive(false);
                Debug.Log("Pressed");
                pv.RPC("Pressed", RpcTarget.Others);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                Anim.SetBool("IsPressing", true);
                Debug.Log("Pressed");
                Stone.SetActive(false);
                pv.RPC("Pressed", RpcTarget.Others);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                Anim.SetBool("IsPressing", false);
                Debug.Log("notPressed");
                Stone.SetActive(true);
                pv.RPC("NotPressed", RpcTarget.Others);
            }
        }
    }

    [PunRPC]
    void Pressed()
    {
        Debug.Log("Pressed");

        Anim.SetBool("IsPressing", true);
        Stone.SetActive(false);
    }


    [PunRPC]
    void NotPressed()
    {
        Debug.Log("NotPressed");
        Anim.SetBool("IsPressing", false);
        Stone.SetActive(true);
    }
}

