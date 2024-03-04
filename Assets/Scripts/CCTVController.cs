using UnityEngine;

public class CCTVController : MonoBehaviour
{
    public float rotationAmount = 60f;
    public float rotationSpeed = 30f;

    public Transform player;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    private bool isRotatingForward = true;


    private bool isPlayerDetected = false;

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0f, rotationAmount, 0f) * startRotation;
    }

    void Update()
    {
        float step = rotationSpeed * Time.deltaTime;

        if (CCTVPlayerDetection.isGameOver)
        {
            rotationSpeed = 0f;
        }
        else
        {
            if (isPlayerDetected)
            {

                Vector3 playerPosition = player.position;

                // 计算旋转方向
                Vector3 targetDirection = playerPosition - transform.position;
                targetDirection.y = 0; // 将Y分量设为0
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                // 应用旋转
                transform.rotation = targetRotation;

            }
            else
            {
                if (isRotatingForward)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

                    if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                    {
                        isRotatingForward = false;
                        targetRotation = Quaternion.Euler(0f, -rotationAmount, 0f) * startRotation;
                    }
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

                    if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                    {
                        isRotatingForward = true;
                        targetRotation = Quaternion.Euler(0f, rotationAmount, 0f) * startRotation;
                    }
                }
            }
        }
    }
    public void StopFollowing()
    {
        isPlayerDetected = false;
    }

    public void StartFollowing()
    {
        isPlayerDetected = true;
    }
}


