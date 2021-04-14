using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _playerAgent;
    private Animator _playerMovementAnim;
    private bool _walking = false;
    [SerializeField]
    private GameObject _coin;
    [SerializeField]
    private AudioClip _coinDrop;
    private int coins = 0;
    private bool _canWalk = true;
    void Start()
    {
        _walking = false;
        _playerAgent = gameObject.GetComponent<NavMeshAgent>();
        _playerMovementAnim = GetComponentInChildren<Animator>();

        if (_playerAgent == null)
        {
            Debug.LogError("NavMeshAgent is Null");
        }

        if (_playerMovementAnim == null)
        {
            Debug.LogError("Player Animator is Null");
        }
    }

    
    void Update()
    {
        PlayerMovement();
        AnimationControl();
    }


    private void PlayerMovement()
    {
        if (Input.GetMouseButtonDown(0) && _canWalk == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _playerAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1) && _walking == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (coins < 1)
                {
                    StartCoroutine(CanWalk());
                    Instantiate(_coin, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                    AudioSource.PlayClipAtPoint(_coinDrop, hit.point);
                    coins++;
                    AItoCoin(hit.point);
                }
            }
        }
    }

    void AItoCoin(Vector3 coinpos)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var guard in guards)
        {
            NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentAnim = guard.GetComponent<Animator>();

            if (currentAgent != null)
            {
                currentAgent.SetDestination(coinpos);
            }

            if (currentGuard != null)
            {
                currentGuard.coinTossed = true;
                currentGuard.coinPos = coinpos;
            }

            if (currentAnim != null)
            {
                currentAnim.SetBool("Walk", true);
            }
        }
    }

    private void AnimationControl()
    {
        if (_playerAgent.remainingDistance > _playerAgent.stoppingDistance)
        {
            _walking = true;
        }
        else
        {
            _walking = false;
        }

        _playerMovementAnim.SetBool("Walk", _walking);
    }

    IEnumerator CanWalk()
    {
        _canWalk = false;
        _playerMovementAnim.SetTrigger("Toss");
        yield return new WaitForSeconds(1.5f);
        _canWalk = true;
    }
}
