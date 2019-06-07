using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class MovementController : MonoBehaviour
{
    public GameObject player;
    AudioClip footsteps;
    private float speed = 3.0f;

    public void TeleportPlayer(){
        player.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
    }

    public void WalkPlayer(){
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        player.transform.position = player.transform.position + forward * Time.deltaTime/1.5f;

    }
}
