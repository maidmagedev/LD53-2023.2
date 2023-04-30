using System;
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
    private bool touchingPlayer = false;

    private Collider2D touching = null;

    [SerializeField] private SpriteRenderer detectionVisual;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(detectionVisual.color.a);
        //print(detectionVisual.gameObject.name);
        //print(touchingPlayer);
        if (touchingPlayer)
        {
            setAlpha(0.8f);
        }
        else
        {
            setAlpha(0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("collider detected");
        if (col.CompareTag("Player"))
        {
            touchingPlayer = true;
            touching = col;
        }
    }


    private void setAlpha(float alpha)
    {
        Color oldColor = detectionVisual.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        detectionVisual.color = newColor;
    }
    public bool get_touchingPlayer()
    {
        return touchingPlayer;
    }

    public Collider2D get_touching()
    {
        return touching;
    }
    
}
