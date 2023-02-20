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
    
    [SerializeField]
    private GameObject playerUIPrefab;
    private GameObject playerUIInstance;
    
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
            
            // Create PlayerUI locally
            playerUIInstance = Instantiate(playerUIPrefab);
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
    
    // When player quit the server
    private void OnDisable()
    {
        Destroy(playerUIInstance);
        
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
        
        //GameManager.UnregisterPlayer(transform.name);
    }
    
}
