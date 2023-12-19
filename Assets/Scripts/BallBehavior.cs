using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Google.XR.Cardboard;
using TMPro;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject player;

    [SerializeField] private TextMeshProUGUI timerText;

    public float shootForce = 99f;

    public int countdownDuration = 5;
    public int spawnInterval = 5;
    
    public int loop = 0;

    public IEnumerator SpawnObject()
    {
        while (countdownDuration > 0) // timer
        { 
            if (countdownDuration == 1)
            {
                timerText.text = countdownDuration + " second";
            }
            else
            {
                timerText.text = countdownDuration + " seconds";
            }

        yield return new WaitForSeconds(1f);
        countdownDuration--;
        }

        while (loop == 0) // instantiate a ball at the end of the timer from the camera
        {
            GameObject newObj = Instantiate(ball, Camera.main.transform.position, Camera.main.transform.rotation);
            Rigidbody rb = newObj.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddForce(Camera.main.transform.forward * shootForce, ForceMode.Impulse);
                    loop = 1;
                    Destroy(newObj, 3f);
                }
            }
        timerText.text = "";
        countdownDuration = 5;
        loop = 0;
    }
}
