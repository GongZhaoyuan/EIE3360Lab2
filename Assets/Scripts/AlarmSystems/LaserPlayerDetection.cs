using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour
{
    private GameObject player;                          // Reference to the player.
    private LastPlayerSighting lastPlayerSighting;      // Reference to the global last sighting of the player.

    private GameObject timerObject;
    void Awake()
    {
        // Setting up references.
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag("1234").GetComponent<LastPlayerSighting>();

        timerObject = GameObject.FindGameObjectWithTag("timer");

        if (lastPlayerSighting == null)
        {
            Debug.LogError("LastPlayerSighting is not found or not properly initialized!");
        }
        else
        {
            Debug.Log("LastPlayerSighting is successfully initialized.");
        }
    }


    void OnTriggerStay(Collider other)
    {
        // If the beam is on...
        if (GetComponent<Renderer>().enabled)
            // ... and if the colliding gameobject is the player...
            if (other.gameObject == player)
                // ... set the last global sighting of the player to the colliding object's position.
                lastPlayerSighting.position = other.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (timerObject != null)
            {
                TimerScript timerScript = timerObject.GetComponent<TimerScript>();
                if (timerScript != null)
                {
                    timerScript.StopTimer();
                    timerScript.ResetTimer();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            if (timerObject != null)
            {
                TimerScript timerScript = timerObject.GetComponent<TimerScript>();
                if (timerScript != null)
                {
                    timerScript.StartTimer();
                }
            }
        }
    }
}
