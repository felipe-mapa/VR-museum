using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleEquip : MonoBehaviour
{
    private RaycastHit _hit;
    public Image img;
    public float totalTime = 2;
    bool teleStatus;
    float teleTimer;
    public int rayDistance = 10;

    Ray ray;

    private void Update()
    {
        if (teleStatus)
        {
            teleTimer += Time.deltaTime;
            img.fillAmount = teleTimer / totalTime;
        }

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, rayDistance))
        {
            if (img.fillAmount == 1 && _hit.transform.CompareTag("teleport"))
            {
                Debug.Log("working");
                _hit.transform.gameObject.GetComponent<MovementController>().TeleportPlayer();
                teleTimer = 0;
            }

            if (_hit.transform.CompareTag("walkObject")) 
            {
                img.fillAmount = 0;
                _hit.transform.gameObject.GetComponent<MovementController>().WalkPlayer();
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
