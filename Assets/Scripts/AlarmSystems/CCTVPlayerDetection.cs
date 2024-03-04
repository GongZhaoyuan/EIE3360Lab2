using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class CCTVPlayerDetection : MonoBehaviour
{


    private GameObject player; 		// Reference to the player.
    private LastPlayerSighting lastPlayerSighting;   // Reference to the global last sighting of the player.

    private CCTVController CCTVController;

    private float timer = 0f;

    private bool isPlayerDetected = false; // Flag to indicate if player is detected.

    public float detectionTimeThreshold = 3f; // Time threshold for player detection.

    public static bool isGameOver = false;

    public GameOverManager gameOverManager;

    private GameObject timerObject;

    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag(Tags.player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag("1234").GetComponent<LastPlayerSighting>();
        timerObject = GameObject.FindGameObjectWithTag("timer");
    }


    private void Start()
    {
        CCTVController = GetComponentInParent<CCTVController>();
    }


    void OnTriggerStay(Collider other)
    {
        // If the colliding gameobject is the player...
        if (other.gameObject == player)
        {
            // ... raycast from the camera towards the player.
            Vector3 relPlayerPos = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
                // If the raycast hits the player...
                if (hit.collider.gameObject == player)
                    // ... set the last global sighting of the player to the player's position.
                    lastPlayerSighting.position = player.transform.position;

            if (!isPlayerDetected)
            {
                timer = 0f;
                isPlayerDetected = true;
                StartCoroutine(EndGameAfterDelay());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            CCTVController.StartFollowing();
        }
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
        // 如果玩家离开碰撞体区域
        if (other.gameObject == player)
        {
            // Stop following the player.
            CCTVController.StopFollowing();

            // Reset the player detection flag and timer.
            isPlayerDetected = false;
            timer = 0f;

            // Stop the coroutine if it's running.
            StopCoroutine(EndGameAfterDelay());
        }
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

    private IEnumerator EndGameAfterDelay()
    {
        while (isPlayerDetected && timer < detectionTimeThreshold)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (isPlayerDetected && timer >= detectionTimeThreshold)
        {
            // Stop following the player.
            CCTVController.StopFollowing();

            // Perform game over logic.
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;

        gameOverManager.GameOver();
    }


}
