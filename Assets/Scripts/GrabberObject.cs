using System.Collections;
using UnityEngine;

public class GrabberObject : MonoBehaviour
{
    public bool isGrabbing;
    public bool isReleaseReady;

    public GameObject grabberObject;
    public BoxCollider2D grabberCollider;

    public BoxCollider2D dummyGrabbedCollider;

    public GameObject heldObject;
    public GameObject grabZone;
    public GameObject playerControllableActors;
    public Transform holdArea;

    [Header("Reference")]
    [SerializeField] Settings settings;

    // Start is called before the first frame update
    void Start()
    {   
        if (settings == null) {
            settings = FindObjectOfType<Settings>();
        }

        isGrabbing = false;
        isReleaseReady = false;
        dummyGrabbedCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   


        if (isReleaseReady && Input.GetKeyDown(settings.grabKey))
        {
            Release();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isGrabbing && other.gameObject.CompareTag("Grabbable") && Input.GetKeyDown(settings.grabKey))
        {
            //Debug.Log("grabbed!");
            grabZone = other.gameObject;
            heldObject = other.gameObject.GetComponent<GrabbableObject>().root;
            //other.GetComponent<GrabbableObject>().grab_me(holdArea);

            holdArea.localPosition = other.gameObject.GetComponent<GrabbableObject>().desiredHoldArea;

            heldObject.transform.parent = transform;
            heldObject.transform.localPosition = holdArea.localPosition;
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            heldObject.GetComponent<Rigidbody2D>().simulated = false;

            // copy data of held object collider to grabber collider
            BoxCollider2D grabbedObjectBounds = grabZone.GetComponent<GrabbableObject>().bounds;
            dummyGrabbedCollider.enabled = true;
            dummyGrabbedCollider.size = grabbedObjectBounds.size;
            dummyGrabbedCollider.offset = grabbedObjectBounds.offset;
            dummyGrabbedCollider.gameObject.transform.localPosition = holdArea.localPosition;

            Physics2D.IgnoreCollision(grabberCollider, grabZone.GetComponent<BoxCollider2D>(), true);
            Physics2D.IgnoreCollision(grabberCollider, dummyGrabbedCollider, true);
            if (heldObject.GetComponent<PolygonCollider2D>() != null)
                Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<PolygonCollider2D>(), true);
            if (heldObject.GetComponent<PolygonCollider2D>() != null)
                Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<BoxCollider2D>(), true);

            isGrabbing = true;
            StartCoroutine(DelayCoroutine(0.1f));
        }
    }

    private IEnumerator DelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isReleaseReady = true;
    }

    public void Release() {
        if (heldObject == null) {
            return;
        }
        //Debug.Log("released!");
        //heldObject.transform.parent = playerControllableActors.transform;
        heldObject.transform.parent = heldObject.GetComponentInChildren<GrabbableObject>().originalParent;
        
        heldObject.GetComponentInParent<Lever>()?.Release(false);

        Rigidbody2D grabberRB = grabberObject.GetComponent<Rigidbody2D>();
        Rigidbody2D heldObjRB = heldObject.GetComponent<Rigidbody2D>();
        heldObjRB.bodyType = RigidbodyType2D.Dynamic;
        heldObjRB.simulated = true;
        
        if (Mathf.Abs(grabberRB.velocity.x) > 8f) {
            //heldObjRB.velocity = new Vector2(grabberRB.velocity.x, grabberRB.velocity.y);
            heldObjRB.AddForce(new Vector2(grabberRB.velocity.x, grabberRB.velocity.y), ForceMode2D.Impulse);
            RBSpeedLimiter rBSpeedLimiter = heldObjRB.GetComponentInChildren<RBSpeedLimiter>();
            if (rBSpeedLimiter != null) {
                StartCoroutine(DisableSpeedLimitTemp(rBSpeedLimiter));
            }

        } else {
            heldObjRB.velocity = Vector3.zero;
        }
        isGrabbing = false;
        isReleaseReady = false;
        dummyGrabbedCollider.enabled = false;
        //grabZone.GetComponent<GrabbableObject>().drop_me();

        Physics2D.IgnoreCollision(grabberCollider, grabZone.GetComponent<BoxCollider2D>(), false);
        //Physics2D.IgnoreCollision(grabberCollider, grabbedObjectBounds, false);
        if (heldObject.GetComponent<PolygonCollider2D>() != null)
            Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<PolygonCollider2D>(), false);
        if (heldObject.GetComponent<BoxCollider2D>() != null)
            Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<BoxCollider2D>(), false);
    }

    IEnumerator DisableSpeedLimitTemp(RBSpeedLimiter rBSpeedLimiter) {
        rBSpeedLimiter.enabled = false;
        yield return new WaitForSeconds(1f);
        rBSpeedLimiter.enabled = true;

    }
}
