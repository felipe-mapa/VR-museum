using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleEquip : MonoBehaviour
{
    private RaycastHit _hit;
    public Image img;
    public float totalTime = 2;
    public float minWalkDistance = 5f;
    private float elevateReady = 8f;
    bool teleStatus;
    float teleTimer;
    public int rayDistance = 20;

    Ray ray;

    private void Update()
    {
        if (teleStatus)
        {
            teleTimer += Time.deltaTime;
            img.fillAmount = teleTimer / totalTime;
        }

        RayCastToFindMovement();
    }

    private void RayCastToFindMovement()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, rayDistance))
        {
            if (img.fillAmount == 1 && _hit.transform.CompareTag("teleport"))
            {
                Debug.Log("working");
                _hit.transform.gameObject.GetComponent<MovementController>().TeleportPlayer();
                teleTimer = 0;
            }

            if (_hit.transform.CompareTag("walkObject") && Vector3.Distance(transform.position, _hit.transform.position) > minWalkDistance)
            {
                img.fillAmount = 0;  
                // Debug.Log(minWalkDistance);
                _hit.transform.gameObject.GetComponent<MovementController>().WalkPlayer();
            }

            if (img.fillAmount == 1 && _hit.transform.CompareTag("Elevator")&& Vector3.Distance(transform.position, _hit.transform.position) < elevateReady) {
                _hit.transform.gameObject.GetComponent<MovementController>()?.ElevatorTeleport();
                img.fillAmount = 0;
            }
        }
    }

    public void TeleOn()
    {
        teleStatus = true;
    }
    public void TeleOff()
    {
        teleTimer = 0;
        img.fillAmount = 0;
        teleStatus = false;
    }
}
