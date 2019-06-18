using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class MovementController : MonoBehaviour {
    public GameObject player, elevatorExit;
    public bool isExiting = false;
    
    AudioClip footsteps;
    Camera cam;
    // private float speed = 3.0f;

    public void Awake() {
        cam = Camera.main;
    } 

    public void TeleportPlayer(){
        player.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
    }

    public void ElevatorTeleport(){
        player.transform.position = new Vector3(elevatorExit.transform.position.x, elevatorExit.transform.position.y + 3.5f, elevatorExit.transform.position.z);
    }

    public void WalkPlayer(){
        Vector3 forward = cam.transform.TransformDirection(Vector3.forward);
        player.transform.position = player.transform.position + forward * Time.deltaTime * 2f;
    }
}
