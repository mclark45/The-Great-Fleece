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
                    StartCoroutine(CanWalk(hit.point));
                    coins++;
                }
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

    IEnumerator CanWalk(Vector3 hit)
    {
        _canWalk = false;
        _playerMovementAnim.SetTrigger("Toss");
        yield return new WaitForSeconds(1.5f);
        Instantiate(_coin, new Vector3(hit.x, hit.y, hit.z), Quaternion.identity);
        AudioManager.Instance.CoinSound(_coinDrop);
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var guard in guards)
        {
            NavMeshAgent currentAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentAnim = guard.GetComponent<Animator>();

            if (currentAgent != null)
            {
                currentAgent.SetDestination(hit);
            }

            if (currentGuard != null)
            {
                currentGuard.coinTossed = true;
                currentGuard.coinPos = hit;
            }

            if (currentAnim != null)
            {
                currentAnim.SetBool("Walk", true);
            }
        }
        _canWalk = true;
    }
}
