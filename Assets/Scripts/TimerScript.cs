using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float timer = 0f;
    private bool isTiming = false;
    private LastPlayerSighting lastPlayerSighting;

    void Start()
    {
        lastPlayerSighting = GameObject.FindGameObjectWithTag("1234").GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer);

            if (timer >= 5f)
            {
                lastPlayerSighting.position = new Vector3(1000f, 1000f, 1000f);
                isTiming = false;
            }
        }
    }

    public void StartTimer()
    {
        isTiming = true;
        timer = 0f;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }
}