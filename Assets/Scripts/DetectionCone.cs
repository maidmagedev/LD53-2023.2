using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCone : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask target;
    [SerializeField] private float coneAngle = 45f;
    [SerializeField] private int numSegments = 16;
    private Color rayColor = Color.red;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a 2D ray from the current position in the forward direction of the transform
        /*for (int i = 0; i < 10; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + i, 0), transform.right, rayDistance, target);

            // Draw a visible ray in the Scene view for debugging purposes
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + i, 0), transform.right * rayDistance, rayColor, Time.deltaTime);
            // Check if the ray hits the player (or any other object on the specified layer(s))
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // Do something if the player is detected, for example:
                Debug.Log("Player detected!");
            }
        }*/
        drawCone();
        
    }

    private void drawCone()
    {
        // Calculate the start and end angles of the cone
        float startAngle = transform.eulerAngles.z - coneAngle / 2f;

        // Calculate the angle between each segment
        float angleStep = coneAngle / numSegments;

        // Cast a ray for each segment of the cone
        for (int i = 0; i < numSegments; i++)
        {
            // Calculate the current angle
            float angle = startAngle + angleStep * i;

            // Calculate the direction of the ray
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // Cast a 2D ray in the current direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, target); 
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // Do something if the player is detected, for example:
                Debug.Log("Player detected!");
            }

            // Draw a line to represent the raycast in the Scene view
            Debug.DrawRay(transform.position, direction * (hit ? hit.distance : rayDistance), rayColor);
        }
    }
    
   
}
