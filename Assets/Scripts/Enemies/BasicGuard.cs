using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BasicGuard : MonoBehaviour, IKillable
{
    [Header("Stats")]
    [SerializeField] int damagePerHit = 10;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isPatrolling = true;
    [SerializeField] private GameObject[] waypoints;
    
    [Header("Components")]
    [SerializeField] private DetectionCone detectionCone;
    [SerializeField] private Light2D damageLight;

    
    DamageableComponent damageableComponent;
    BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private float detectionTimer = 0f;
    private int currWaypointIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        damageableComponent = this.gameObject.AddComponent<DamageableComponent>();
        damageableComponent.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();

        if (isPatrolling)
        {
            Patrol();
        }
        
        // if touching the player for long enough, kill them
        if (detectionCone.get_touchingPlayer())
        {
            detectionTimer += Time.deltaTime;
            if (detectionTimer > 0.1f)
            {
                detectionCone.get_touching().GetComponent<DamageableComponent>().TakeDamage(100);
            }
        }
        else
        {
            detectionTimer = 0f;
        }

        // die when you touch hazards
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            Die();
        }
    }

    private void Patrol()
    {
        if (waypoints.Length < 2)
        {
            return;
        }
        if (Mathf.Abs(transform.position.x - waypoints[currWaypointIndex].transform.position.x) < 1)
        {
            ++currWaypointIndex;
            if (currWaypointIndex >= waypoints.Length)
            {
                currWaypointIndex = 0;
            }
        }
        
        // if waypoint is to the left
        if (transform.position.x > waypoints[currWaypointIndex].transform.position.x)
        {
            rb.velocity = Vector2.left * speed;
        }
        else
        {
            rb.velocity = Vector2.right * speed;
        }
        //transform.position = Vector2.MoveTowards(transform.position, waypoints[currWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    private void FlipSprite()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            //detectionCone.transform.localScale = new Vector2(Mathf.Abs(detectionCone.transform.localScale.x),
               // detectionCone.transform.localScale.y);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y);
            //detectionCone.transform.localScale = new Vector2(Mathf.Abs(detectionCone.transform.localScale.x)*-1,
              //  detectionCone.transform.localScale.y);
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<DamageableComponent>(out DamageableComponent target))
        {
            print("dealing damage");
            target.TakeDamage(damagePerHit);
        }
    }

    public void Die() {
        print("guard ded");
        //FindObjectOfType<UIScore>().score += 10;
        Destroy(this.transform.parent.gameObject);  
    }
    public void NotifyDamage()
    {
        print("Guard is taking damage");
        StartCoroutine(DamageLightToggle());
        isPatrolling = false;
    }

    IEnumerator DamageLightToggle()
    {
        damageLight.enabled = true;
        yield return new WaitForSeconds(1f);
        damageLight.enabled = false;
        isPatrolling = true;
    }
    

}
