using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class dummyScript : MonoBehaviour
{
    public int current_health = 100;
    public Animator dummyAnimation;

    public Rigidbody2D dummyRigidBody;
    public GameObject DummyReferenceBeg;
    public GameObject DummyReferenceEnd;

    public void Start()
    {
        dummyRigidBody.freezeRotation = true;
    }

    // If the dummy is hit, play the appropriate animation
    public void broIsDown(){
        GameObject Player1 = GameObject.FindGameObjectWithTag("Player 1");

        if (Player1.GetComponent<PlayerMovement>().playerFlipped)
            {
                dummyAnimation.SetBool("hitFront", true);
            }
            else
            {
                dummyAnimation.SetBool("hitBack", true);
            }
    }

    // Reset the animation back to idle
    public void broIsUp(){
        dummyAnimation.SetBool("hitBack", false);
        dummyAnimation.SetBool("hitFront", false);
    }
}


