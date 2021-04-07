using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _newCameraPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Camera.main.transform.position = _newCameraPosition.transform.position;
            Camera.main.transform.rotation = _newCameraPosition.transform.rotation;
        }
    }
}
