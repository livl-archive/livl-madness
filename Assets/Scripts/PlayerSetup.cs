using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    
    [SerializeField]
    Behaviour[] componentsToDisable;
    
    Camera sceneCamera;
    
    // Start is called before the first frame update
    private void Start()
    {
     
        if (!isLocalPlayer)
        {
            foreach (Behaviour component in componentsToDisable)
            {
                component.enabled = false;
            }
        } else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        
    }
    
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
    
}
