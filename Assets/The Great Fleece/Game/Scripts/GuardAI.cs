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
    private int _startPosition;
    private int _endPosition;
    private int _guardTwoEndPosition;
    private Animator _enemyAnim;
    private bool _walking = false;
    public bool coinTossed = false;
    public Vector3 coinPos;


    void Start()
    {
        _walking = false;
        _startPosition = Random.Range(10, 15);
        _endPosition = Random.Range(8, 12);
        _guardTwoEndPosition = Random.Range(10, 15);

        _enemyAgent = gameObject.GetComponent<NavMeshAgent>();
        _enemyAnim = gameObject.GetComponent<Animator>();

        if (_enemyAgent == null)
        {
            Debug.LogError("Enemy NavMeshAgent is Null");
        }

        if (_enemyAnim == null)
        {
            Debug.LogError("Enemy animator is null");
        }
    }

    void Update()
    {
        EnemyAI();
        if (coinTossed == false)
        {
            AnimationControl();
        }
    }

    private void EnemyAI()
    {
        if (_wayPoints.Count > 0 && _wayPoints[_currentTarget] != null && coinTossed == false)
        {
            _enemyAgent.SetDestination(_wayPoints[_currentTarget].position);

            float distance = Vector3.Distance(transform.position, _wayPoints[_currentTarget].position);

            if (distance < 1.0f)
            {
                if (_reverse == false && _currentTarget != (_wayPoints.Count - 1))
                {
                    _currentTarget++;
                }
                else if (_reverse == true && _currentTarget != 0)
                {
                    _currentTarget--;
                }

                if (_currentTarget == 0)
                {
                    StartCoroutine(PauseMovement(_startPosition));
                }
                else if (_currentTarget == (_wayPoints.Count - 1))
                {
                    StartCoroutine(PauseMovement(_endPosition));
                }
                else if (_wayPoints.Count > 3)
                {
                    StartCoroutine(PauseMovement(_guardTwoEndPosition));
                }
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPos);

            if (_enemyAgent.remainingDistance < _enemyAgent.stoppingDistance)
            {
                _enemyAnim.SetBool("Walk", false);
            }
        }
    }

    private void AnimationControl()
    {
        if (_enemyAgent.remainingDistance > _enemyAgent.stoppingDistance)
        {
            _walking = true;
        }
        else
        {
            _walking = false;
        }

        _enemyAnim.SetBool("Walk", _walking);
    }

    IEnumerator PauseMovement(int random)
    {
        if (_currentTarget == 0)
        {
            yield return new WaitForSeconds(random);
            _reverse = false;
        }
        else if (_currentTarget == (_wayPoints.Count - 1))
        {
            yield return new WaitForSeconds(random);
            _reverse = true;
        }
    }
}
