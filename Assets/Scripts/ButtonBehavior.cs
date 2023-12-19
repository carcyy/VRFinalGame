using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class ButtonBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform player;
    [SerializeField] private BallBehavior ballVar;
    private float moveSpeed = 10f;
    public bool isTrueVar1 = false;
    public bool isTrueVar4 = false;
    public bool triggerPressed = false;

    public int countdownDuration = 5;
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        OnButtonClick();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast; // raycast from pointer

        if (raycastResult.gameObject != null)
        {
            RectTransform hitRect = raycastResult.gameObject.GetComponent<RectTransform>(); // for the Button UI
            Collider hitCollider = raycastResult.gameObject.GetComponent<Collider>(); // for teleporting back using a gameobject

            string rectName = hitRect != null ? hitRect.name : "Unknown"; // telling me if its empty or not
            string colliderName = hitCollider != null ? hitCollider.name : "Unknown";

            Debug.Log("Pointer clicked on: " + rectName); // telling me what i have clicked
            Debug.Log("Pointer clicked on: " + colliderName);

            if (hitRect != null && rectName != null)
            {
                Debug.Log("Hit on: " + rectName); 

                HandleCardboardClick(rectName); // call function with name of what i have hit
            }
            if (hitCollider != null && colliderName != null)
            {
                Debug.Log("Hit on: " + colliderName);

                HandleCardboardClick(colliderName);
            }
        }
    }

    public void OnButtonClick() // a function written to tell whether the constant moving is on or off
    {
        //Debug.Log(isTrueVar4);

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
            //Debug.Log("stop");
        }
    }

    private void HandleCardboardClick(string buttonName)
    {
        Debug.Log("handle the click");

        switch (buttonName) // take the name of the collider/rect and apply code to it based on what exactly it should do
        {
            // GROUP 1
            case "1-GravityOn":
                Debug.Log("Button Group 1 - grav true");
                isTrueVar1 = true; // gravity is true, so the button will always spawn at y = 20
                break;

            case "1-GravityOff":
                Debug.Log("Button Group 1 - grav false");
                isTrueVar1 = false; // false gravity means random y
                break;

            case "1-Teleport":
                Debug.Log("Button Group 1 - commence teleport");
                TeleportPlayer(isTrueVar1); // commence code for teleport (function at bottom)
                break;

            // GROUP 2
            case "2-Press2Teleport":
                if (TryGetTouchPosition(out Vector3 groundPosition)) // prepare to teleport
                {
                    TeleportToPosition(groundPosition); 
                }
                break;
            // GROUP 3
            case "3-x": // these just increment by 5 in the direction labeled
                player.transform.position = new Vector3(player.transform.position.x + 5f, player.transform.position.y, player.transform.position.z);
                break;

            case "3-x-":
                player.transform.position = new Vector3(player.transform.position.x - 5f, player.transform.position.y, player.transform.position.z);
                break;

            case "3-y":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5f, player.transform.position.z);
                break;

            case "3-y-":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 5f, player.transform.position.z);
                break;

            case "3-Z":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5f);
                break;

            case "3-z-":
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 5f);
                break;

            // GROUP 4
            case "4-On":
                Debug.Log("Button Group 4 - turn on walking");
                isTrueVar4 = true; // function from top, walking is true so the player should move
                //OnButtonClick();
                break;

            case "4-Off":
                Debug.Log("Button Group 4 - turn off walking");
                isTrueVar4 = false; // false, no moving
                //OnButtonClick();
                break;

            //GROUP 5
            case "5-Forest1": // teleporting to predetermined locations
                player.transform.position = new Vector3(626, 20, -7);
                break;

            case "5-MountainBase":
                player.transform.position = new Vector3(334, 20, -627);
                break;

            case "5-MountainTop":
                player.transform.position = new Vector3(671, 239, -679);
                break;

            case "5-Forest2":
                player.transform.position = new Vector3(100, 20, -500);
                break;

            case "5-Forest3":
                player.transform.position = new Vector3(-655, 20, -748);
                break;

            case "5-6":
                player.transform.position = new Vector3(880, 20, 100);
                break;

            case "5-4":
                player.transform.position = new Vector3(168, 20, 100);
                break;

            case "5-3":
                player.transform.position = new Vector3(-89, 20, 100);
                break;

            case "5-2":
                player.transform.position = new Vector3(-369, 20, 100);
                break;

            case "5-1":
                player.transform.position = new Vector3(-696, 20, 100);
                break;

            case "GoBack": // this will teleport you back to the "teleportation hub" or implimentation #4
                player.transform.position = new Vector3(512, 20, 100);
                break;

            //GROUP 6
            case "6-Press2Launch": //function that spawns a ball, wherever the ball hits is where you teleport
                StartCoroutine(ballVar.SpawnObject());
                break;
        }
    }

    public void TeleportPlayer(bool trueVar)
    {
        if (trueVar == false) // if no gravity, then
        {
            float randomX = Random.Range(-750, -650);
            float randomY = Random.Range(20, 120); //random y
            float randomZ = Random.Range(112, 212);

            player.transform.position = new Vector3(randomX, randomY, randomZ);
            Debug.Log("player teleported to: " + player.transform.position);
        }
        if (trueVar == true)
        {
            float randomX = Random.Range(-750, -650);
            float randomY = Random.Range(20, 20); //else, no random y aka gravity
            float randomZ = Random.Range(112, 212);

            player.transform.position = new Vector3(randomX, randomY, randomZ); //teleport
            Debug.Log("player teleported to: " + player.transform.position);
        }
    }

    private bool TryGetTouchPosition(out Vector3 groundPosition) // function where a timer is called giving the player time to look
    {
        StartCoroutine(WaitFunction());
        countdownDuration = 5;

        groundPosition = Vector3.zero;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //once time is up, raycasted from the camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) // if it is within the checker board,
        {
            groundPosition = hit.point; // teleport to there
            return true;
        }

        return false;
    }

    private void TeleportToPosition(Vector3 position) // function to teleport
    {
        Vector3 cameraForward = Camera.main.transform.forward;

        Vector3 targetPosition = Camera.main.transform.position + cameraForward * 10f;

        player.transform.position = targetPosition;
        Debug.Log("player teleported to: " + player.transform.position);
    }

    public IEnumerator WaitFunction() // a wait function for timers
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
        timerText.text = "";
        countdownDuration = 5;
    }
}