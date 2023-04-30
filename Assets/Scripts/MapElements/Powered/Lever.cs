using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : PowerSource
{
    [Header("Settings")]
    [SerializeField] Position enabledPosition;
    [SerializeField] bool neutralEnabled;

    [Header("references")]
    [SerializeField] GameObject Drone;
    [SerializeField] GameObject main;
    [SerializeField] GrabberObject grabber;
    [SerializeField] GrabbableObject grabbable;
    private Vector3 originalPos;
    private Quaternion originalRot;
    public PowerableElement pElem;


    [Header("Misc")]
    public Position pos;
    public bool lerping = false;
    public enum Position {
        a, center, b
    }

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] float volume;
    // Start is called before the first frame update
    void Start()
    {
        if (grabber == null) {
            grabber = FindObjectOfType<GrabberObject>();
        }
        pElem = GetComponentInParent<PowerableElement>();
        pElem.SetPowerSource(this);

        originalPos = grabbable.root.transform.position;
        originalRot = grabbable.root.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Drone.transform.position.x - main.transform.position.x > 0.45) {
            pos = Position.b;
        } else if (Drone.transform.position.x - main.transform.position.x < -0.45) {
            pos = Position.a;
        } else {
            pos = Position.center;
        }
        if (!neutralEnabled && pos == Position.center) {
            pos = Position.a;
        }

        if (pos == enabledPosition) {
            isPowered = true;
            pElem.StartPowered();
        }

        if (grabber.isGrabbing) {
            // Get the direction to the target
            StopAllCoroutines();
            lerping = false;
            Vector3 direction = grabbable.root.transform.position - transform.position;
            direction.Normalize();

            // Calculate the angle to rotate towards the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Set the rotation to face the target
            main.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (grabber.isReleaseReady && Input.GetKeyDown(KeyCode.R)) {
                Release();
            }
        
        } 
        
        if (Vector3.Distance(Drone.transform.position, main.transform.position) > 1.5) {
            if (grabber.isReleaseReady) {

                Release();
            }
        } 
        
    
    }

    private void Release() {
        grabber.Release();
                grabbable.root.transform.SetParent(this.transform);
                grabbable.root.transform.position = originalPos;
                grabbable.root.transform.localRotation = Quaternion.Euler(Vector3.zero);
        switch(pos) {
            case Position.a:
                StartCoroutine(RotateToQuaternion(main.transform, new Vector3(0, 0, 60f), 0.5f));
                break;
            case Position.b:
                StartCoroutine(RotateToQuaternion(main.transform, new Vector3(0, 0, 300f), 0.5f));
                break;
            case Position.center:
                if (neutralEnabled) {
                    StartCoroutine(RotateToQuaternion(main.transform, new Vector3(0, 0, 0f), 0.5f));
                }
                break;
        }
    }

    IEnumerator RotateToQuaternion(Transform transform, Vector3 targetRotation, float duration)
    {
        if (lerping) {
            yield break;
        }
        Debug.Log("Test");
        lerping = true;
        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetQuaternion, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetQuaternion;
        lerping = false;
    }
}
