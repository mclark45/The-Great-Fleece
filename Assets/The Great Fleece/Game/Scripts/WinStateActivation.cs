using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject _winState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.HasCard == true)
        {
            _winState.SetActive(true);
        }
        else
        {
            Debug.Log("You need to grab the keycard");
        }
    }
}
