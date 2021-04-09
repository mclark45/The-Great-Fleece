using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _playerAgent;
    private Animator _playerMovementAnim;
    private bool _walking = false;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _playerAgent.SetDestination(hit.point);
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
}
