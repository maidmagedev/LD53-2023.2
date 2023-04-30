using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;


public class BasicGuard : MonoBehaviour, IKillable
{
    [Header("Stats")]
    [SerializeField] int damagePerHit = 10;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isPatrolling = true;

    [Header("Components")]
    [SerializeField] private DetectionCone detectionCone;
    [SerializeField] private Light2D damageLight;

    DamageableComponent damageableComponent;
    BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private float detectionTimer = 0f;
    
    
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

        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            Die();
        }
    }

    private void Patrol()
    {
        
    }

    private void FlipSprite()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x)*-1, gameObject.transform.localScale.y);
            detectionCone.transform.localScale = new Vector2(Mathf.Abs(detectionCone.transform.localScale.x),
                detectionCone.transform.localScale.y);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
            detectionCone.transform.localScale = new Vector2(Mathf.Abs(detectionCone.transform.localScale.x)*-1,
                detectionCone.transform.localScale.y);
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
        //FindObjectOfType<UIScore>().score += 10;
        Destroy(this.transform.parent.gameObject);  
    }
    public void NotifyDamage()
    {
        StartCoroutine(DamageLightToggle());
    }

    IEnumerator DamageLightToggle()
    {
        damageLight.enabled = true;
        yield return new WaitForSeconds(.5f);
        damageLight.enabled = false;
    }
    

}
