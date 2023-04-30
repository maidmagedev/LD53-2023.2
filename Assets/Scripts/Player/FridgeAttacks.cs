using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeAttacks : MonoBehaviour
{
    [Header("Throwable Handler")]
    [SerializeField] KeyCode throwFoodKey = KeyCode.Mouse0;
    [SerializeField] List<GameObject> throwables;
    [SerializeField] GameObject throwOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    void InputHandler() {
        if (Input.GetKeyDown(throwFoodKey)) {
            ThrowFood();
        }
    }

    void ThrowFood() {
        GameObject template = throwables[Random.Range(0, throwables.Count)];
        GameObject projectile = Instantiate(template, throwOrigin.transform.position, Quaternion.identity);
        projectile.GetComponentInChildren<Projectile>().sender = this.gameObject; 
        attackFace();
    }

    void attackFace() {
        PlayerExpressions pExp = FindObjectOfType<PlayerExpressions>();
        StartCoroutine(pExp.PlayAnim("exp_arrowface", true, 0.5f));

    }
}
