using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTrigger : MonoBehaviour
{
    public enum ListOfTrigger
    {
        Tool,
        Bucket,
        Door,
        Press
    };
    public ListOfTrigger TriggerType;

    public Text TextTool;
    public Text TextBucket;
    public Text TextDoor;
    public Text TextPress;
    private Text textToTrigger;
    public static bool holdingTool = false;
    public static bool holdingBucket = false;

    private bool gotItem = false;
    [HideInInspector]public bool openDoor = false;

    public GameObject LeftDoor;
    public GameObject RightDoor;
    private Animator leftDoorAnim;
    private Animator rightDoorAnim;

    public float Countdown;
    private float countdown;
    private bool startCountdown = false;

    void Start()
    {
        countdown = Countdown;

        TextTool.enabled = false;
        TextBucket.enabled = false;
        TextDoor.enabled = false;
        TextPress.enabled = false;

        gotItem = false;

        TypeOfTrigger();

        if (LeftDoor || RightDoor == null)
            return;
    }

    void Update()
    {
        if(gotItem)
        {
           textToTrigger.enabled = true;
            if (TriggerType == ListOfTrigger.Door)
            {
                startCountdown = true;
                leftDoorAnim.SetBool("Open", true);
                rightDoorAnim.SetBool("Open", true);
                if(countdown <= 0)
                {
                    textToTrigger.enabled = false;
                    gotItem = false;
                    startCountdown = false;
                    countdown = Countdown;
                    leftDoorAnim.SetBool("Open", false);
                    rightDoorAnim.SetBool("Open", false);
                }
            }
            if (TriggerType == ListOfTrigger.Bucket)
            {
                holdingBucket = true;
                if(TextTool.enabled == true)
                {
                    TextTool.enabled = false;
                }
            }
             if (TriggerType == ListOfTrigger.Tool)
            {
                holdingTool = true;
                if(TextBucket.enabled == true)
                {
                    TextBucket.enabled = false;
                }
            }
            if (Input.GetButton("Fire2"))
           {
                if(TriggerType == ListOfTrigger.Tool)
                {
                    holdingTool = false;
                }
                if (TriggerType == ListOfTrigger.Bucket)
                {
                    holdingBucket = false;
                }
                if ((TriggerType == ListOfTrigger.Tool || TriggerType == ListOfTrigger.Bucket))
                {
                    gotItem = false;
                    textToTrigger.enabled = false;
                }
           }
        }

        if(startCountdown)
        {
            countdown -= Time.deltaTime;
        }
    }
    
    private void TypeOfTrigger()
    {
        switch (TriggerType)
        {
            case ListOfTrigger.Tool:
                textToTrigger = TextTool;
                break;
            case ListOfTrigger.Bucket:
                textToTrigger = TextBucket;
                break;
            case ListOfTrigger.Door:
                textToTrigger = TextDoor;
                leftDoorAnim = LeftDoor.GetComponent<Animator>();
                rightDoorAnim = RightDoor.GetComponent<Animator>();
                break;
            case ListOfTrigger.Press:
                textToTrigger = TextPress;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && Input.GetButton("Fire1"))
        {
            gotItem = true;
        }
    }
}
