using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject _grabCard;
    private bool _scenePlayed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _scenePlayed == false)
        {
            _scenePlayed = true;
            _grabCard.SetActive(true);
            GameManager.Instance.HasCard = true;
        }
    }
}
