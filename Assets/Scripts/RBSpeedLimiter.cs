using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBSpeedLimiter : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float maxSpeedX;
    public float maxSpeedY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > maxSpeedX)
        {
            if (rb.velocity.x > 0)
                rb.velocity = new Vector2(maxSpeedX, rb.velocity.y);
            else if (rb.velocity.x < 0)
                rb.velocity = new Vector2(-maxSpeedX, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.y) > maxSpeedY)
        {
            if (rb.velocity.y > 0)
                rb.velocity = new Vector2(rb.velocity.x, maxSpeedY);
            else if (rb.velocity.y < 0)
                rb.velocity = new Vector2(rb.velocity.x, -maxSpeedY);
        }
    }
}
