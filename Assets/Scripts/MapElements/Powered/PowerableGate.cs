using UnityEngine;

public class PowerableGate : MonoBehaviour, PowerableElement
{
    
    [SerializeField] PowerSource powerSource;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private Vector3 endPosition;
    private Vector3 startPosition;
    private bool movingForward = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerSource.isPowered)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, Time.deltaTime * movementSpeed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime * movementSpeed);
        }
    }

    public void StartPowered()
    {
        if (transform.position == startPosition)
        {
            movingForward = true;
        }
        else if (transform.position == endPosition)
        {
            movingForward = false;
        }
        
    }

    public void EndPowered()
    {
        //throw new System.NotImplementedException();
    }

    public void SetPowerSource(PowerSource pSource)
    {
        powerSource = pSource;
    }
}
