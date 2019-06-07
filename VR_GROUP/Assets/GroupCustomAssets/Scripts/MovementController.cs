using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class MovementController : MonoBehaviour
{
    public GameObject player;

    public void TeleportPlayer(){
        player.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
    }
}
