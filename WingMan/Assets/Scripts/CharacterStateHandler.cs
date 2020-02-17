using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterStateHandler : MonoBehaviour
{
    [SerializeField] GameObject[] parachuteObjects;
    [SerializeField] Animator parachuteAnimator;
    [SerializeField] float pushBack;
    Rigidbody myRb;
    Animator myAnimator;
    [SerializeField] AudioSource deathScream;
    bool dead;
    CharacterManipulatorScript _cms;
    float timestamp; Stopwatch stopWatch = new Stopwatch();
    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
        _cms = GetComponent<CharacterManipulatorScript>();
        stopWatch.Start();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CurrentPlayerState != PlayerStates.Parachuting)
        {
            GameManager.Instance.CurrentPlayerState = PlayerStates.Parachuting;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!dead)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                myAnimator.SetTrigger("OnGround");
                myRb.constraints = RigidbodyConstraints.FreezeAll;
                print(stopWatch.Elapsed);

            }
            else
            {
                if (collision.gameObject.CompareTag("Side"))
                {
                    StartCoroutine(DeathRoutine(collision.gameObject.transform.position, collision.GetContact(0).point));
                    // Debug.Log("side");
                }
                else
                {
                    Vector3 temp = Camera.main.transform.position;
                    StartCoroutine(DeathRoutine(new Vector3(temp.x, -temp.y, temp.z), collision.GetContact(0).point)); //Debug.Log("top");
                }

            }
        }
    }

    IEnumerator DeathRoutine(Vector3 collisionObjectPos, Vector3 collisionPoint)
    {
        dead = true;
        myRb.isKinematic = true;
        _cms.ToggleDeath();
        deathScream.Play();
        _cms.AddForce(collisionObjectPos, collisionPoint);
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
                print(stopWatch.Elapsed);
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
        yield return new WaitForSeconds(1.208f);
        parachuteAnimator.enabled = true;
        yield return new WaitForSeconds(1.208f);
        myRb.AddForceAtPosition(Vector3.up * 10, transform.up * 3, ForceMode.Impulse);
        //myAnimator.transform.localEulerAngles = new Vector3(-90, 0, 0);
        parachuteAnimator.enabled = false;
        parachuteAnimator.gameObject.SetActive(false);
        UseParachute();
    }
    void UseParachute()
    {
        myRb.isKinematic = false;
        myRb.useGravity = true;
        myRb.constraints = RigidbodyConstraints.None;
        for (int i = 0; i < parachuteObjects.Length; i++)
        {
            parachuteObjects[i].SetActive(true);
        }
    }
}
