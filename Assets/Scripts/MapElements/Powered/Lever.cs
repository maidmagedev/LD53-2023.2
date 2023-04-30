using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : PowerSource
{
    [Header("Settings")]
    public Position pos; // current position, also starting position.

    [SerializeField] Position enabledPosition;
    private Position offPosition;
    [SerializeField] bool isBinary;
    [SerializeField] bool activateOnce;
    private bool neutralEnabled;
    [SerializeField] float angleZ_posA;
    [SerializeField] float angleZ_posB;
    [SerializeField] float angleZ_posCenter;

    [Header("references")]
    [SerializeField] GameObject leverGrab;
    [SerializeField] GameObject main;
    [SerializeField] GrabberObject grabber;
    [SerializeField] GrabbableObject grabbable;
    [SerializeField] Settings settings;
    private Vector3 originalPos;
    private Quaternion originalRot;
    //public GameObject pElemSource; //Should have a pElem component.

    //public PowerableElement pElem;


    [Header("Misc")]
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
        if (settings == null) {
            settings = FindObjectOfType<Settings>();
        }
        if (isBinary) {
            neutralEnabled = false;
            if (enabledPosition == Position.a) {
                offPosition = Position.b;
            } else if (enabledPosition == Position.b) {
                offPosition = Position.a;
            }
        }

        //pElem = pElemSource.GetComponentInChildren<PowerableElement>();
        //pElem.SetPowerSource(this);

        foreach (PowerableElement pE in powerableElements) {
            pE.SetPowerSource(this);
        }

        originalPos = grabbable.root.transform.position;
        originalRot = grabbable.root.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (leverGrab.transform.position.x - main.transform.position.x > 0.45) {
            // Debug.Log("B");
            pos = Position.b;
        } else if (leverGrab.transform.position.x - main.transform.position.x < -0.45) {
            pos = Position.a;
            // Debug.Log("A");

        } else {
            // Debug.Log("C");

            pos = Position.center;
        }
        if (!neutralEnabled && pos == Position.center) {
            pos = Position.a;
        }

        if (pos == enabledPosition) {
            isPowered = true;
            foreach (PowerableElement pElem in powerableElements) {
                pElem.StartPowered();
            }
        } else {
            if (isPowered) {
                foreach (PowerableElement pElem in powerableElements) {
                    pElem.EndPowered();
                }
                isPowered = false;
            }
        }

        if (grabber.isGrabbing && grabber.heldObject == leverGrab) {
            // Get the direction to the target
            StopAllCoroutines();
            lerping = false;
            Vector3 direction = grabbable.root.transform.position - transform.position;
            direction.Normalize();

            // Calculate the angle to rotate towards the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Set the rotation to face the target
            main.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // if (grabber.isReleaseReady && Input.GetKey(settings.grabKey)) {
            //     Debug.Log("Release wheel grabbing.");
            //     Release();
            // }
        
        } 
        
        if (Mathf.Abs(leverGrab.transform.position.x - main.transform.position.x) > 3 || Mathf.Abs(leverGrab.transform.position.y - main.transform.position.y) > 3) {
            Debug.Log("Forced Release");
            if (grabber.isReleaseReady) {

                Release(true);
            }
        } 
        
    
    }

    public void Release(bool fromInside) {

        if (fromInside) {
            grabber.Release();
        }
        grabbable.root.transform.SetParent(this.transform);
        grabbable.root.transform.position = originalPos;
        grabbable.root.transform.localRotation = Quaternion.Euler(Vector3.zero);

        switch(pos) {
            case Position.a:
                StartCoroutine(RotateToQuaternion(main.transform, new Vector3(0, 0, angleZ_posA), 0.5f));
                break;
            case Position.b:
                StartCoroutine(RotateToQuaternion(main.transform, new Vector3(0, 0, angleZ_posB), 0.5f));
                break;
            case Position.center:
                if (neutralEnabled) {
                    StartCoroutine(RotateToQuaternion(main.transform, new Vector3(0, 0, angleZ_posCenter), 0.5f));
                }
                break;
        }
        //Debug.Log("end of switch from release");
    }

    IEnumerator RotateToQuaternion(Transform transform, Vector3 targetRotation, float duration)
    {

        if (lerping) {
            yield break;
        }
        //Debug.Log("Rotating");
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
