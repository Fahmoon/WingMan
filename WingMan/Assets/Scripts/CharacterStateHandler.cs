using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHandler : MonoBehaviour
{
    [SerializeField] GameObject[] parachuteObjects;
    [SerializeField] Animator parachuteAnimator;
    Rigidbody myRb;
    Animator myAnimator;
    private void Start()
    {
        myRb = GetComponent<Rigidbody>();

        myAnimator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.CurrentPlayerState = PlayerStates.Parachuting;
    }
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        GameManager.Instance.CurrentGameState = GameStates.Waiting;
        GameManager.Instance.CurrentPlayerState = PlayerStates.Dead;
        yield return new WaitForSeconds(2);
        GameManager.Instance.CurrentGameState = GameStates.Lose;
    }

    public void HandlePlayerState(PlayerStates currentState)
    {
        switch (currentState)
        {
            case PlayerStates.Idle:
                break;
            case PlayerStates.FreeFalling:
                break;
            case PlayerStates.Parachuting:
                StartCoroutine(ParachuteOpen());
                break;
            case PlayerStates.Celebrating:
                break;
            case PlayerStates.Dead:
                break;

        }

    }
    IEnumerator ParachuteOpen()
    {
        myAnimator.SetTrigger("Parachuting");
        yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length);
        parachuteAnimator.enabled = true;
        yield return new WaitForSeconds(parachuteAnimator.GetCurrentAnimatorStateInfo(0).length);
        myRb.AddForceAtPosition(Vector3.up * 10, transform.up * 3, ForceMode.Impulse);
        myAnimator.transform.localEulerAngles = new Vector3(-90, 0, 0);
        parachuteAnimator.enabled = false;
        parachuteAnimator.gameObject.SetActive(false);
        UseParachute();
    }
    void UseParachute()
    {
        myRb.isKinematic = false;
        myRb.useGravity = true;
        for (int i = 0; i < parachuteObjects.Length; i++)
        {
            parachuteObjects[i].SetActive(true);
        }
    }
}
