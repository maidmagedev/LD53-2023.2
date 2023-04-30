using System.Collections;
using UnityEngine;

public class GrabberObject : MonoBehaviour
{
    [SerializeField] private KeyCode toggleGrabKey;
    public bool isGrabbing;
    public bool isReleaseReady;
    public GameObject heldObject;
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
            //heldObject.transform.parent = playerControllableActors.transform;
            //heldObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            isGrabbing = false;
            isReleaseReady = false;
            heldObject.GetComponent<GrabbableObject>().drop_me();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isGrabbing && other.gameObject.CompareTag("Grabbable") && Input.GetKeyDown(toggleGrabKey))
        {
            Debug.Log("grabbed!");
            heldObject = other.gameObject.GetComponent<GrabbableObject>().root;
            other.GetComponent<GrabbableObject>().grab_me(holdArea);
            /*heldObject.transform.parent = transform;
            heldObject.transform.localPosition = holdArea.localPosition;
            heldObject.GetComponent<Rigidbody2D>().isKinematic = true;*/
            /*GameObject newObj = Instantiate(heldObject, this.transform);
            newObj.transform.localPosition = holdArea.localPosition;
            Destroy(newObj.GetComponent<PlatformingMovementComponent>());
            Destroy(newObj.GetComponent<SpriteRenderer>());
            Destroy(newObj.GetComponent<Rigidbody2D>());*/
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
