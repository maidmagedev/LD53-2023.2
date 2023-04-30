using System.Collections;
using UnityEngine;

public class GrabberObject : MonoBehaviour
{
    [SerializeField] private KeyCode toggleGrabKey;
    public bool isGrabbing;
    public bool isReleaseReady;

    public GameObject grabberObject;
    public BoxCollider2D grabberCollider;

    public BoxCollider2D dummyGrabbedCollider;
    public BoxCollider2D grabbedObjectBounds;

    public GameObject heldObject;
    public GameObject grabZone;
    public GameObject playerControllableActors;
    public Transform holdArea;

    // Start is called before the first frame update
    void Start()
    {
        toggleGrabKey = KeyCode.R;
        isGrabbing = false;
        isReleaseReady = false;
        grabbedObjectBounds.enabled = false;
        dummyGrabbedCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReleaseReady && Input.GetKeyDown(toggleGrabKey))
        {
            //Debug.Log("released!");
            heldObject.transform.parent = playerControllableActors.transform;
            Rigidbody2D grabberRB = grabberObject.GetComponent<Rigidbody2D>();
            Rigidbody2D heldObjRB = heldObject.GetComponent<Rigidbody2D>();
            heldObjRB.bodyType = RigidbodyType2D.Dynamic;
            heldObjRB.velocity = new Vector2(grabberRB.velocity.x, grabberRB.velocity.y);
            isGrabbing = false;
            isReleaseReady = false;
            dummyGrabbedCollider.enabled = false;
            //grabZone.GetComponent<GrabbableObject>().drop_me();

            Physics2D.IgnoreCollision(grabberCollider, grabZone.GetComponent<BoxCollider2D>(), false);
            Physics2D.IgnoreCollision(grabberCollider, grabbedObjectBounds, false);
            if (heldObject.GetComponent<PolygonCollider2D>() != null)
                Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<PolygonCollider2D>(), false);
            if (heldObject.GetComponent<BoxCollider2D>() != null)
                Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<BoxCollider2D>(), false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isGrabbing && other.gameObject.CompareTag("Grabbable") && Input.GetKeyDown(toggleGrabKey))
        {
            //Debug.Log("grabbed!");
            grabZone = other.gameObject;
            heldObject = other.gameObject.GetComponent<GrabbableObject>().root;
            //other.GetComponent<GrabbableObject>().grab_me(holdArea);
            heldObject.transform.parent = transform;
            heldObject.transform.localPosition = holdArea.localPosition;
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            
            // copy data of held object collider to grabber collider
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
}
