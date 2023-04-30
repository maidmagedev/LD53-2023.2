using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ActiveCharacterManager : MonoBehaviour
{
    [SerializeField] GameObject followCam;
    public GameObject fridge;
    public GameObject drone;

    public GameObject activeActor;
    private List<GameObject> actors;

    private void Start()
    {
        actors = new List<GameObject>
        {
            fridge.gameObject,
            drone.gameObject
        };

        setActiveActor(fridge);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // select fridge
        {
            setActiveActor(fridge);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // select drone
        {
            setActiveActor(drone);
        }
    }


    void setActiveActor(GameObject actor)
    {
        disableAllActors();
        enableActor(actor, true);
        followCam.GetComponent<CinemachineVirtualCamera>().Follow = actor.transform;
        activeActor = actor;
    }

    void disableAllActors()
    {
        foreach (GameObject actor in actors)
        {
            enableActor(actor, false);
        }
    }

    void enableActor(GameObject actor, bool enabled)
    {
        if (actor.GetComponent<TopDownMovementComponent>() != null)
            actor.GetComponent<TopDownMovementComponent>().enabled = enabled;
        else if (actor.gameObject.GetComponent<PlatformingMovementComponent>() != null)
            actor.GetComponent<PlatformingMovementComponent>().enabled = enabled;
    }
}