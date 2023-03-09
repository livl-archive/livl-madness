using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour
{

    [Header("Phone Components")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text screenTitle;

    [Header("Message Components")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageInitials;
    [SerializeField] private TMP_Text messageName;
    [SerializeField] private TMP_Text messageText;

    [Header("Screens")]
    [SerializeField] private GameObject productListScreen;
    [SerializeField] private GameObject pauseScreen;


    [Header("Config")]
    [SerializeField] private int defaultMessageDuration = 5;

    private GameObject currentScreen;
    private Dictionary<Phone.Screen, GameObject> screens = new Dictionary<Phone.Screen, GameObject>();

    void Start()
    {
        HideMessage();
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
        messagePanel.SetActive(true);
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
        messagePanel.transform.localPosition = new Vector3(0, -170, 0);
        messagePanel.transform.LeanMoveY(0, 0.5f).setEaseOutBack();
        
        // Hide message after a while
        StartCoroutine(DelayedHideMessage(defaultMessageDuration));
    }

    private IEnumerator DelayedHideMessage(int duration)
    {
        yield return new WaitForSeconds(duration);
        HideMessage();
    }

    public void HideMessage()
    {
        messagePanel.transform.localPosition = new Vector3(0, 0, 0);
        messagePanel.transform.LeanMoveY(-170, 0.5f).setEaseInBack();
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
