using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CanvasHUD : MonoBehaviour
{
    [SerializeField]
	public GameObject PanelStart;
    
	[SerializeField]
    public Button buttonHost, buttonServer, buttonClient;
    
	[SerializeField]
    public InputField inputFieldAddress;
	
	[SerializeField]
	public Text textInfo; 
   
    private void Start()
    {
        //Update the canvas text if you have manually changed network managers address from the game object before starting the game scene
        if (NetworkManager.singleton.networkAddress != "localhost") { inputFieldAddress.text = NetworkManager.singleton.networkAddress; }

        //Adds a listener to the main input field and invokes a method when the value changes.
        inputFieldAddress.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        //Make sure to attach these Buttons in the Inspector
        buttonHost.onClick.AddListener(ButtonHost);
        buttonServer.onClick.AddListener(ButtonServer);
        buttonClient.onClick.AddListener(ButtonClient);

        //This updates the Unity canvas, we have to manually call it every change, unlike legacy OnGUI.
        SetupCanvas();
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        NetworkManager.singleton.networkAddress = inputFieldAddress.text;
    }

    public void ButtonHost()
    {
		textInfo.text = "Please wait. Connecting...";
		NetworkManager.singleton.StartHost();
        SetupCanvas();
    }

    public void ButtonServer()
    {
        textInfo.text = "Please wait. Connecting...";
		NetworkManager.singleton.StartServer();
        SetupCanvas();
    }

    public void ButtonClient()
    {   
		textInfo.text = "Please wait. Connecting...";
		NetworkManager.singleton.StartClient();
        SetupCanvas();
    }

    public void SetupCanvas()
    {
        // Here we will dump majority of the canvas UI that may be changed.
		if (NetworkClient.isConnected && NetworkServer.active)
        {
			PanelStart.SetActive(false);
        }
    }
}
