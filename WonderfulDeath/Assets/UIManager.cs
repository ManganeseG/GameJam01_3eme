using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text BotMessage;
    public static UIManager Main;


    private void Awake()
    {
        Main = this;
    }

    public void SetBotMessage(string message)
    {
        if (string.IsNullOrEmpty(message))
            BotMessage.enabled = false;
        else
        {
            BotMessage.text = message;
            BotMessage.enabled = true;
        }

    }

}
