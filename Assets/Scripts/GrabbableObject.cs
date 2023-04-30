using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public GameObject root;
    private Transform holder;
    private bool isHeld = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (isHeld)
        {
            this.root.transform.position = new Vector3(holder.transform.position.x, holder.transform.position.y, 0);
        }
    }

    public void grab_me(Transform given_holder)
    {
        isHeld = true;
        holder = given_holder;
    }

    public void drop_me()
    {
        isHeld = false;
        holder = null;
    }
}
