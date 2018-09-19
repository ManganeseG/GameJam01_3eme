using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float Speed = 1f;
    public float horizontal;
    public float vertical;
    public float turnSmoothing = 10f;

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        horizontal *= Speed;
        vertical = Input.GetAxis("Vertical");
        vertical *= Speed;

        PlayerControl(horizontal, vertical);

        transform.position += new Vector3(horizontal, 0f, vertical);
    }

    private void Update()
    {
        if(DeathTrigger.isDying != DeathTrigger.ListOfDeath.None)
        {
            StartCoroutine(die());
        }
        
    }

    IEnumerator die()
    {
        Debug.Log("WaitingToDie");
        yield return new WaitUntil(() => Input.GetButton("Fire1"));
        DeathTrigger.isDying = DeathTrigger.ListOfDeath.None;
        Debug.Log("WonderfullyDead");
    }

    void PlayerControl(float horizontal, float vertical)
    {
        if(horizontal != 0f || vertical != 0f)
        {
            Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
            transform.rotation = newRotation;
        }
    }
}
