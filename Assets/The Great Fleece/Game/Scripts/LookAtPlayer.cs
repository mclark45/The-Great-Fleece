using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    public Transform startCamera;
    private void Start()
    {
        transform.position = startCamera.position;
        transform.rotation = startCamera.rotation;
    }


    void Update()
    {
        transform.LookAt(_player);
    }
}
