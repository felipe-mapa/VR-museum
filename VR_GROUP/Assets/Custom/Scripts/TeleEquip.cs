using UnityEngine;
using UnityEngine.UI;

public class TeleEquip : MonoBehaviour {
    public Image img;
    public float totalTime = 2;
    public float minWalkDistance = 5f;
    public int rayDistance = 20;
    public Light _spotLight;
    
    private RaycastHit _hit;
    private float elevateReady = 8f;
    private bool teleStatus;
    private float teleTimer;
    private Camera mainCamera;
    private Ray ray;
    private float offIntensity = 0.00f;
    private MovementController currentMovementController;
    private float onIntensity = 3.00f;

    private void Awake() {
        _spotLight.GetComponentInChildren<Light>();
        mainCamera = Camera.main;
        _spotLight.intensity = offIntensity;
    }

    private void Update() {
        if (teleStatus) {
            teleTimer += Time.deltaTime;
            img.fillAmount = teleTimer / totalTime;
        }

        if (img.fillAmount == 1 
        &&  currentMovementController.isExiting) {
            teleTimer = 0;
            // Debug.Log("is exiting");
            Application.Quit();
        } else {
            RayCastToFindMovement();
        }
    }

    private void RayCastToFindMovement() {
        ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, rayDistance)) {
           
            if (!_hit.transform.CompareTag("teleport") && !_hit.transform.CompareTag("Elevator") && !_hit.transform.CompareTag("walkObject")) {
                currentMovementController = null;
                return;
            }

            if (_hit.transform != currentMovementController?.transform) {
                currentMovementController = _hit.transform.gameObject.GetComponent<MovementController>();
                Debug.LogWarning("Searching for MovementController" + _hit.transform);
                
                if (!currentMovementController) {
                    Debug.LogError("MovementController not found on: " + _hit.transform.name);
                    return;
                }
            }

            if (img.fillAmount == 1 
            &&  currentMovementController.CompareTag("teleport")) {
                currentMovementController.TeleportPlayer();
                teleTimer = 0;
            }

            if (currentMovementController.CompareTag("walkObject")
            &&  Vector3.Distance(transform.position, _hit.transform.position) > minWalkDistance) {
                img.fillAmount = 0;  
                currentMovementController.WalkPlayer();
            }

            if (currentMovementController.CompareTag("Elevator")
            &&  img.fillAmount == 1
            &&  Vector3.Distance(transform.position, _hit.transform.position) < elevateReady
            ) {
                Debug.Log("Elevator");
                currentMovementController?.ElevatorTeleport();
                img.fillAmount = 0;
            }
            
            if (_hit.transform.CompareTag("Elevator") && img.fillAmount < 1){
                 _spotLight.intensity = onIntensity;
            } else
            {
                 _spotLight.intensity = offIntensity;
            }

         
        }
    }

    public void TeleOn() {
        teleStatus = true;
    }

    public void TeleOff() {
        teleTimer = 0;
        img.fillAmount = 0;
        teleStatus = false;
    }
}
