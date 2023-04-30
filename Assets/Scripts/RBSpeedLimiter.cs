using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBSpeedLimiter : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float maxSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(Mathf.Abs(rb.velocity.x));
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            if (rb.velocity.x > 0)
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            else if (rb.velocity.x < 0)
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
            else if (rb.velocity.y < 0)
                rb.velocity = new Vector2(rb.velocity.x, -maxSpeed);
        }
    }
}
