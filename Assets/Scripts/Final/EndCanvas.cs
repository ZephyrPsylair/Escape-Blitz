using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCanvas : MonoBehaviourPun
{

    [SerializeField] private CanvasGroup CG;

    [SerializeField] private float fadeDuration = 5.0f;

    [SerializeField] private GameObject BGMusic;
    [SerializeField] private GameObject EndCv;
    [SerializeField] private AudioSource EndSource;
    [SerializeField] private BoxCollider2D C2D;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Camera;

    private void Start()
    {
        C2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EndCv.SetActive(true);
        StartCoroutine(FadeCanvasGroup(CG, CG.alpha, 1, fadeDuration));
        BGMusic.SetActive(false);
        EndSource.Play();
        C2D.enabled = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg,float Start, float End, float Duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(Start, End, elapsedTime / Duration);
            yield return null;
        }
        cg.alpha = End;
    }

    public void LeaveRoom()
    {
        
        EndCv.SetActive(false);
        Camera.SetActive(true);
        Loading.SetActive(true);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
    