using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class Lever : MonoBehaviourPun
{
    [SerializeField] private PhotonView pv;
    [SerializeField] private Animator Anim;
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject InteractiveText;
    [SerializeField] private bool IsLeft = true;
    [SerializeField] private bool IsInteractive = false;

    private void Start()
    {
        if (photonView.IsMine)
        {
            Anim = GetComponent<Animator>();
            Door = GameObject.Find("SecondStoneDoor");
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Interactive();
        }
    }

    void Interactive()
    {
        if (IsInteractive)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (IsLeft)
                {
                    Anim.SetBool("IsRight", true);
                    Door.SetActive(false);
                    IsLeft = false;
                    pv.RPC("Right", RpcTarget.Others);
                }

                else if (!IsLeft)
                {
                    Anim.SetBool("IsRight", false);
                    Door.SetActive(true);
                    IsLeft = true;
                    pv.RPC("NotRight", RpcTarget.Others);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(photonView.IsMine)
        {
            if(collision.gameObject.tag == "Player")
            {
                InteractiveText.SetActive(true);
                IsInteractive = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "Player")
            {
                InteractiveText.SetActive(true);
                IsInteractive = true;

            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (photonView.IsMine)
        {
            if(collision.gameObject.tag == "Player")
            {
                InteractiveText.SetActive(false);
                IsInteractive = false;
            }
        }
    }

    [PunRPC]
    void Right()
    {
        Debug.Log("Pressed");

        Anim.SetBool("IsRight", true);
        Door.SetActive(false);
        IsLeft = false;
    }


    [PunRPC]
    void NotRight()
    {
        Debug.Log("NotPressed");

        Anim.SetBool("IsRight", false);
        Door.SetActive(true);
        IsLeft = true;

    }
}
