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


    [Header("Config")]
    [SerializeField] private int defaultMessageDuration = 5;

    void Start()
    {
        hideMessage();
    }

    public void showMessage(string name, string message)
    {
        messagePanel.SetActive(true);

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
        StartCoroutine(delayedHideMessage(defaultMessageDuration));
    }

    private IEnumerator delayedHideMessage(int duration)
    {
        yield return new WaitForSeconds(duration);
        hideMessage();
    }

    public void hideMessage()
    {
        messagePanel.SetActive(false);
    }

    public void setScreenTitle(string title)
    {
        screenTitle.text = title;
    }

    public void setTimeText(string time)
    {
        timeText.text = time;
    }

}
