using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionClass : MonoBehaviour
{
    private Vector3 lastCollisionPosition;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] Transform boardTrans;
    public GameObject board;

    void Update()
    {
        rb.velocity = Vector3.zero;
        boardTrans.rotation = Quaternion.identity;
        boardTrans.position = new Vector3(895, 1, 163);
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Debug.Log("Hit");

            ContactPoint contact = collision.contacts[0];
            lastCollisionPosition = contact.point;

            player.transform.position = new Vector3(lastCollisionPosition.x, 20, lastCollisionPosition.z);
        }
    }
}