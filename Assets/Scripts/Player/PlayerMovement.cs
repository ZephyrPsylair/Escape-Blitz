using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;
using Photon.Pun.UtilityScripts;
using TMPro;

public class PlayerMovement : MonoBehaviourPun , IPunObservable
{
    [SerializeField] private PhotonView pv;

    [SerializeField] private float MovementSpeed = 5f;

    [SerializeField] private float SmoothMoveSpeed = 5f;
    private Vector3 SmoothMove;
    private float Horizontal;
    private float Vertical;
    public TMP_Text PlayerName;

    [SerializeField] private Animator Anim;

    [SerializeField] private bool IsMoving;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject PlayerCamera;

    [SerializeField] private SpriteRenderer SR; 



    private void Start()
    {
        if(photonView.IsMine)
        {
            PlayerName.text = PhotonNetwork.NickName;

            PlayerCamera.SetActive(true);
            
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            
            rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
        }
        else
        {
            PlayerName.text = pv.Owner.NickName;
            
            //PlayerCamera.SetActive(false);
        }
        
    }

    
    void Update()
    {
        if(photonView.IsMine)
        {
            ProcessInputs();
        }
        else
        {
            SmoothMovement();
        }
    }
    
    private void SmoothMovement()
    {
        transform.position= Vector3.Lerp(transform.position, SmoothMove, Time.deltaTime * SmoothMoveSpeed);    
    }
        
    private void ProcessInputs()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        transform.position += move * MovementSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.D))
        {
            SR.flipX = false;
            IsMoving = true;
            pv.RPC("OnDirectionChange_LEFT", RpcTarget.Others);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SR.flipX = true;
            IsMoving = true;
            pv.RPC("OnDirectionChange_RIGHT", RpcTarget.Others);
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            IsMoving = true;
            pv.RPC("OnMoving", RpcTarget.Others);
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            IsMoving = false;
            pv.RPC("OnNotMoving", RpcTarget.Others);
        }


        if (IsMoving)
        {
            Anim.SetBool("IsMoving", true);
            pv.RPC("OnAnimMoving", RpcTarget.Others);
        }
        
        if (!IsMoving)
        {
            Anim.SetBool("IsMoving", false);
            pv.RPC("OnAnimNotMoving", RpcTarget.Others);
        }
    }

    [PunRPC]
    void OnDirectionChange_LEFT()
    {
        SR.flipX = false;
        IsMoving = true;
    }

    [PunRPC]
    void OnDirectionChange_RIGHT()
    {
        SR.flipX = true;
        IsMoving = true;
    }

    [PunRPC]
    void OnMoving()
    {
        IsMoving = true;
    }

    [PunRPC]
    void OnNotMoving()
    {
        IsMoving = false;
    }

    [PunRPC]
    void OnAnimMoving()
    {
        Anim.SetBool("IsMoving", true);
    }

    [PunRPC]
    void OnAnimNotMoving()
    {
        Anim.SetBool("IsMoving", false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if(stream.IsReading)
        {
            SmoothMove = (Vector3) stream.ReceiveNext( );
        }
    }
}
