using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject uiPanel;
    public bool isDisplayed = false;
    
    void Start()
    {
        mainCamera = Camera.main;
        uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up); //Make the worldspace UI rotate with player
    }

    public void SetUp()
    {
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {   
        uiPanel.SetActive(false);
        isDisplayed = false;        
    }
}
