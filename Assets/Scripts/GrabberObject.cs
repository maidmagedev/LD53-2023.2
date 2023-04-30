using System.Collections;
using UnityEngine;

public class GrabberObject : MonoBehaviour
{
    [SerializeField] private KeyCode toggleGrabKey;
    public bool isGrabbing;
    public bool isReleaseReady;

    public Collider2D grabberCollider;
    public GameObject heldObject;
    public GameObject grabZone;
    public Transform holdArea;
    public GameObject playerControllableActors;

    // Start is called before the first frame update
    void Start()
    {
        toggleGrabKey = KeyCode.R;
        isGrabbing = false;
        isReleaseReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReleaseReady && Input.GetKeyDown(toggleGrabKey))
        {
            Debug.Log("released!");
            heldObject.transform.parent = playerControllableActors.transform;
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isGrabbing = false;
            isReleaseReady = false;
            //grabZone.GetComponent<GrabbableObject>().drop_me();

            Physics2D.IgnoreCollision(grabberCollider, grabZone.GetComponent<BoxCollider2D>(), false);
            Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<PolygonCollider2D>(), false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isGrabbing && other.gameObject.CompareTag("Grabbable") && Input.GetKeyDown(toggleGrabKey))
        {
            Debug.Log("grabbed!");
            grabZone = other.gameObject;
            heldObject = other.gameObject.GetComponent<GrabbableObject>().root;
            //other.GetComponent<GrabbableObject>().grab_me(holdArea);
            heldObject.transform.parent = transform;
            heldObject.transform.localPosition = holdArea.localPosition;
            heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            Physics2D.IgnoreCollision(grabberCollider, grabZone.GetComponent<Collider2D>(), true);
            Physics2D.IgnoreCollision(grabberCollider, heldObject.GetComponent<PolygonCollider2D>(), true);

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
