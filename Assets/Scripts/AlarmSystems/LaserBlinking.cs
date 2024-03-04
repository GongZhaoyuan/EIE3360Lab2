using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour
{
    public float onTime;            // Amount of time in seconds the laser is on for.
    public float offTime;           // Amount of time in seconds the laser is off for.
    private float timer;            // Timer to time the laser blinking.

    public bool haveClosed;

    public bool isfourfive;

    void Update()
    {
        // Increment the timer by the amount of time since the last frame.

        if (isfourfive)
        {
            timer += Time.deltaTime;

            // If the beam is on and the onTime has been reached...
            if (GetComponent<Renderer>().enabled && timer >= onTime)
                // Switch the beam.
                SwitchBeam();

            // If the beam is off and the offTime has been reached...
            if (!GetComponent<Renderer>().enabled && timer >= offTime)
                // Switch the beam.
                SwitchBeam();
        }
        else
        {
            if (haveClosed)
            {
                Debug.Log("close");
                GetComponent<Renderer>().enabled = false;
                GetComponent<Light>().enabled = false;
            }
            else
            {
                Debug.Log("open");
                GetComponent<Renderer>().enabled = true;
                GetComponent<Light>().enabled = true;
            }
        }
    }

    void SwitchBeam()
    {
        // Reset the timer.
        timer = 0f;

        // Switch whether the beam and light are on or off.
        GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
    }

}
