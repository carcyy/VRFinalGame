using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class ButtonBehavior : MonoBehaviour
{
    //[SerializeField] private Transform button;
    [SerializeField] private Transform player;
    [SerializeField] private BallBehavior ballVar;
    private float moveSpeed = 10f;
    public bool isTrueVar1 = false;
    public bool isTrueVar4 = false;
    public bool triggerPressed = false;

    public int countdownDuration = 5;
    [SerializeField] private TextMeshProUGUI timerText;

    //if(Google.XR.Cardboard.Api.IsTriggerPressed)

    void Update()
    {
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            triggerPressed = true;
            Triggered();
        }
        else
        {
            triggerPressed = false;
        }

        OnButtonClick();
    }

    public void Triggered()
    {

    }

    public void OnButtonClick()
    {
        Debug.Log(isTrueVar4);

        if(isTrueVar4 == true)
        {
            //player.transform.position += Vector3.forward * Time.deltaTime * moveSpeed; //works

            Vector3 cameraForward = Camera.main.transform.forward;

            cameraForward.y = 0f;
            cameraForward.Normalize();

            player.transform.position += cameraForward * moveSpeed * Time.deltaTime;
        }
        else if (isTrueVar4 == false)
        {
            Debug.Log("stop");
        }
    }

    private void HandleCardboardClick(string buttonName)
    {
        Debug.Log("handle the click");

        switch (buttonName)
        {
            // GROUP 1
            case "1-GRAVITYON":
                Debug.Log("Button Group 1 - grav true");
                isTrueVar1 = true;
                break;

            case "1-GRAVITYOFF":
                Debug.Log("Button Group 1 - grav false");
                isTrueVar1 = false;
                break;

            case "1-TELEPORT":
                Debug.Log("Button Group 1 - commence teleport");
                TeleportPlayer(isTrueVar1);
                break;

            // GROUP 2
            case "2-PRESS2TELEPORT":
                if (TryGetTouchPosition(out Vector3 groundPosition))
                {
                    TeleportToPosition(groundPosition);
                }
                break;
            // GROUP 3
            case "3-X":
                player.transform.position = new Vector3(player.transform.position.x + 5f, player.transform.position.y, player.transform.position.z);
                break;

            case "3-X-":
                player.transform.position = new Vector3(player.transform.position.x - 5f, player.transform.position.y, player.transform.position.z);
                break;

            case "3-Y":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5f, player.transform.position.z);
                break;

            case "3-Y-":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 5f, player.transform.position.z);
                break;

            case "3-Z":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5f);
                break;

            case "3-Z-":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 5f);
                break;

            // GROUP 4
            case "4-ON":
                Debug.Log("Button Group 4 - turn on walking");
                isTrueVar4 = true;
                break;

            case "4-OFF":
                Debug.Log("Button Group 4 - turn off walking");
                isTrueVar4 = false;
                break;

            //GROUP 5
            case "5-FOREST1":
                player.transform.position = new Vector3(626, 20, -7);
                break;

            case "5-MOUNTAINBASE":
                player.transform.position = new Vector3(334, 20, -627);
                break;

            case "5-MOUNTAINTOP":
                player.transform.position = new Vector3(671, 239, -679);
                break;

            //GROUP 6
            case "6-PRESS2LAUNCH":
                StartCoroutine(ballVar.SpawnObject());
                break;
        }
    }

    public void TeleportPlayer(bool trueVar)
    {
        if (trueVar == true)
        {
            float randomX = Random.Range(-750, -650);
            float randomY = Random.Range(20, 120);
            float randomZ = Random.Range(112, 212);

            player.transform.position = new Vector3(randomX, randomY, randomZ);
            Debug.Log("player teleported to: " + player.transform.position);
        }
        else
        {
            float randomX = Random.Range(-750, -650);
            float randomY = Random.Range(20, 20); //maybe
            float randomZ = Random.Range(112, 212);

            player.transform.position = new Vector3(randomX, randomY, randomZ);
            Debug.Log("player teleported to: " + player.transform.position);
        }
    }

    private bool TryGetTouchPosition(out Vector3 groundPosition)
    {
        StartCoroutine(WaitFunction());
        countdownDuration = 5;

        groundPosition = Vector3.zero;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            groundPosition = hit.point;
            return true;
        }

        return false;
    }

    private void TeleportToPosition(Vector3 position)
    {
        player.transform.position = position;
        Debug.Log("player teleported to: " + player.transform.position);
    }

    public IEnumerator WaitFunction()
    {
        while (countdownDuration > 0)
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
    }
}