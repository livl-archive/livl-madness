using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class PhoneController : MonoBehaviour
{

    [Header("Phone Components")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text screenTitle;

    [Header("Message Components")]
    [SerializeField] private Transform messagePanel;
    [SerializeField] private TMP_Text messageInitials;
    [SerializeField] private TMP_Text messageName;
    [SerializeField] private TMP_Text messageText;

    [Header("Screens")]
    [SerializeField] private GameObject productListScreen;
    [SerializeField] private GameObject pauseScreen;


    [Header("Config")]
    [SerializeField] private int messageDuration = 5;
    [SerializeField] private int messageHiddenYPos = -1200;
    [SerializeField] private int messageShownYPos = 0;
    [SerializeField] private float messageTransitionTime = 1.0f;

    private GameObject currentScreen;
    private Dictionary<Phone.Screen, GameObject> screens = new Dictionary<Phone.Screen, GameObject>();

    void Start()
    {
        messagePanel.localPosition = new Vector2(0, messageHiddenYPos);
        GenerateScreens();

        // Hide all screens
        foreach (GameObject screen in screens.Values)
        {
            screen.SetActive(false);
        }

        // Start on the product list screen
        Navigate(Phone.Screen.ProductList);
    }

    private void GenerateScreens()
    {
        screens.Add(Phone.Screen.ProductList, productListScreen);
        screens.Add(Phone.Screen.Pause, pauseScreen);
    }

    public void ShowMessage(string name, string message)
    {
        Debug.Log("Message received : " + name + " : " + message + " !");
        
        // Split name & lastname
        string[] nameParts = name.Split(' ');

        // Make initials from first letter of each part
        if (nameParts.Length > 1)
        {
            messageInitials.text = nameParts[0][0].ToString() + nameParts[1][0].ToString();
        }
        else
        {
            messageInitials.text = name.Substring(0, 2);
        }

        messageName.text = name;
        messageText.text = message;
        
        // Start animation
        messagePanel.localPosition = new Vector2(0, messageHiddenYPos);
        messagePanel.LeanMoveLocalY(messageShownYPos, messageTransitionTime).setEaseOutBack();
        
        // Hide message after a while
        StartCoroutine(DelayedHideMessage(messageDuration));
    }

    private IEnumerator DelayedHideMessage(int duration)
    {
        yield return new WaitForSeconds(duration);
        HideMessage();
    }

    public void HideMessage()
    {
        messagePanel.localPosition = new Vector2(0, messageShownYPos);
        messagePanel.LeanMoveLocalY(messageHiddenYPos, messageTransitionTime).setEaseInBack();
    }

    public void SetScreenTitle(string title)
    {
        screenTitle.text = title;
    }

    public void SetTimeText(string time)
    {
        timeText.text = time;
    }

    public void Navigate(Phone.Screen screen)
    {

        // Check if the screen exists
        if (!screens.ContainsKey(screen))
        {
            throw new System.Exception("Screen " + screen.ToString() + " does not exist");
        }

        // Hide the previous screen
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
        }

        // Show the new screen
        screens[screen].SetActive(true);
        currentScreen = screens[screen];
        SetScreenTitle(Phone.ScreenTitles[screen]);
    }

}
