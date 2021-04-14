using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameras : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOver;

    Animator _securityCamera;
    Animator _securityCameraTwo;

    private void Start()
    {
        _securityCamera = GameObject.Find("Camera_1").GetComponent<Animator>();
        _securityCameraTwo = GameObject.Find("Camera_2").GetComponent<Animator>();


        if (_securityCamera == null)
        {
            Debug.LogError("Animator is Null");
        }

        if (_securityCameraTwo == null)
        {
            Debug.LogError("Animator is Null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ColorChange();
            StartCoroutine(GameOver());
        }
    }

    void ColorChange()
    {
        MeshRenderer render = GetComponent<MeshRenderer>();

        if (render == null)
        {
            Debug.LogError("Mesh Renderer is Null");
        }

        Color color = new Color(0.6f, .1f, .1f, .3f);
        render.material.SetColor("_TintColor", color);
    }
    IEnumerator GameOver()
    {
        _securityCamera.enabled = false;
        _securityCameraTwo.enabled = false;
        yield return new WaitForSeconds(1.5f);
        _gameOver.SetActive(true);
    }
}
