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
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageInitials;
    [SerializeField] private TMP_Text messageName;
    [SerializeField] private TMP_Text messageText;

    [Header("Screens")]
    [SerializeField] private GameObject productListScreen;
    [SerializeField] private GameObject pauseScreen;


    [Header("Config")]
    [SerializeField] private int messageDuration = 5;
    [SerializeField] private int messageHiddenMultiplier = 0;
    [SerializeField] private int messageShownMultiplier = 0;
    [SerializeField] private float messageTransitionTime = 1.0f;
    [SerializeField] private float screenTransitionTime = 1.0f;

    private GameObject currentScreen;
    private Dictionary<Phone.Screen, GameObject> screens = new Dictionary<Phone.Screen, GameObject>();

    private float lastCoroutineTime;

    void Start()
    {   
        HideMessage(false);
        GenerateScreens();

        // Hide all screens
        foreach (GameObject screen in screens.Values)
        {
            HideScreen(screen, false);
        }

        // Start on the product list screen
        Navigate(Phone.Screen.ProductList);
    }

    private void GenerateScreens()
    {
        screens.Add(Phone.Screen.ProductList, productListScreen);
        screens.Add(Phone.Screen.Pause, pauseScreen);
    }

    // TODO : Move message to a separate script
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
        messagePanel.transform.LeanMoveLocal(messagePanel.transform.up * messageShownMultiplier, messageTransitionTime).setEaseOutQuint();
        
        // Hide message after a while
        StartCoroutine(DelayedHideMessage(messageDuration));
    }

    private IEnumerator DelayedHideMessage(int duration)
    {
        yield return new WaitForSeconds(duration);
        HideMessage();
    }

    public void HideMessage(bool animated = true)
    {
        if (!animated)
        {
            messagePanel.transform.localPosition = messagePanel.transform.up * messageHiddenMultiplier;
            return;
        }
        
        messagePanel.transform.LeanMoveLocal(messagePanel.transform.up * messageHiddenMultiplier, messageTransitionTime).setEaseInQuint();
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
        
        var animationFinished = Time.time - lastCoroutineTime > screenTransitionTime;
        // Cancel current hide coroutine if any
        if (!animationFinished)
        {
            StopAllCoroutines();
        }

        // Hide the previous screen
        if (currentScreen != null)
        {
            HideScreen(currentScreen, animationFinished);
        }

        // Show the new screen
        currentScreen = screens[screen];
        ShowScreen(currentScreen);
        SetScreenTitle(Phone.ScreenTitles[screen]);
    }

    private void HideScreen(GameObject screen, bool animated = true)
    {
        screen.transform.localScale = new Vector3(0, 0, 0);

        if (!animated)
        {
            screen.SetActive(false);
            return;
        }

        lastCoroutineTime = Time.time;
        var coroutine = DelayedSetActive(screen, false, screenTransitionTime);
        StartCoroutine(coroutine);
    }
    
    private void ShowScreen(GameObject screen)
    {
        screen.SetActive(true);
        screen.LeanScale(new Vector3(1, 1, 1), screenTransitionTime).setEaseOutQuint();
    }
    
    private IEnumerator DelayedSetActive(GameObject obj, bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(active);
    }

}
