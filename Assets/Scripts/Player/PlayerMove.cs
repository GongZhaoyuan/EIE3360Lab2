using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5f;
    private Rigidbody rb;

    protected Joystick joystick;
    protected Joybutton joybutton;

    private LaserBlinking[] laserScripts;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        GameObject[] LaserObjects = GameObject.FindGameObjectsWithTag("laser");
        laserScripts = new LaserBlinking[LaserObjects.Length];

        for (int i = 0; i < LaserObjects.Length; i++)
        {
            laserScripts[i] = LaserObjects[i].GetComponent<LaserBlinking>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (CCTVPlayerDetection.isGameOver)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            enabled = false;
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.AddForce(movement * speed);

            rb.velocity = new Vector3(joystick.Horizontal * 6f, rb.velocity.y, joystick.Vertical * 6f);

            if (joybutton.Pressed)
            {
                foreach (LaserBlinking script in laserScripts)
                {
                    script.haveClosed = true;
                }
                Debug.Log("pressed");
            }
            else
            {
                foreach (LaserBlinking script in laserScripts)
                {
                    script.haveClosed = false;
                }
                Debug.Log("dis pressed");
            }
        }
    }
}
