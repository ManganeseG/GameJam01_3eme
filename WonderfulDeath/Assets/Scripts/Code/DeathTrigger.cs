using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTrigger : MonoBehaviour
{

    public enum ListOfDeath
    {
        None,
        Electrocute,
        Melt,
        Press
    }
    public ListOfDeath DeathType;

    public string Message;
    public static ListOfDeath isDying;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch (DeathType)
            {
                case ListOfDeath.Electrocute:
                    if (!ObjectTrigger.holdingBucket)
                        return;

                    isDying = DeathType;
                    UIManager.Main.SetBotMessage(Message);
                    break;
                case ListOfDeath.Melt:

                    isDying = DeathType;
                    UIManager.Main.SetBotMessage(Message);

                    break;
                case ListOfDeath.Press:
                    if (!ObjectTrigger.holdingTool)
                        return;

                    isDying = DeathType;
                    UIManager.Main.SetBotMessage(Message);

                    break;
            }
        }
    }
}
