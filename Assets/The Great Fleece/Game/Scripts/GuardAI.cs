using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _wayPoints;
    private NavMeshAgent _enemyAgent;
    [SerializeField]
    private int _currentTarget;
    private bool _reverse = false;

    void Start()
    {
        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();

        if (_enemyAgent == null)
        {
            Debug.LogError("Enemy NavMeshAgent is Null");
        }
    }

    void Update()
    {
        EnemyAI();
    }

    private void EnemyAI()
    {
        if (_wayPoints.Count > 0 && _wayPoints[_currentTarget] != null)
        {
            _enemyAgent.SetDestination(_wayPoints[_currentTarget].position);

            float distance = Vector3.Distance(transform.position, _wayPoints[_currentTarget].position);

            if (distance < 1.0f)
            {
                if (_reverse == false && _currentTarget != (_wayPoints.Count -1))
                {
                    _currentTarget++;
                }
                else if (_currentTarget == (_wayPoints.Count - 1) || _reverse == true)
                {
                    _currentTarget--;
                    _reverse = true;
                }

                if (_currentTarget == 0)
                {
                    _reverse = false;
                }
            }
        }
    }
}
