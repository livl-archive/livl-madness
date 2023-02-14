using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    
    [SerializeField]
    Behaviour[] componentsToDisable;
    
    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    
    Camera sceneCamera;
    
    // Start is called before the first frame update
    private void Start()
    {

        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }

    }

    private void DisableComponents()
    {
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = false;
        }
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
    
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
    
}
