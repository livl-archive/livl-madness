using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerUI : NetworkBehaviour
{

    private static bool paused = false;


    private Player player;

    private NetworkManager networkManager;
    private PlayerController controller;


    [Header("Components")]
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private PhoneController phoneController;

    public static bool isPaused
    {
        get => paused;
    }

    private void Start()
    {
        networkManager = NetworkManager.singleton;

        // Hide pause overlay
        pauseOverlay.SetActive(false);
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        SetPauseMenuVisibility(!isPaused);
    }

    public void SetPauseMenuVisibility(bool visible)
    {
        PlayerUI.paused = visible;
        pauseOverlay.SetActive(visible);
        if (isPaused)
        {
            phoneController.navigate(Phone.Screen.Pause);
        }
        else
        {
            phoneController.navigate(Phone.Screen.ProductList);
        }
    }

    public void LeaveRoomButton()
    {
        if (isClientOnly)
        {
            networkManager.StopClient();
        }
        else
        {
            networkManager.StopHost();
        }
    }

}
