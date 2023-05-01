using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this is mainly intended for the fridge and the drone.
public class ActorStats : MonoBehaviour, IKillable
{
    [SerializeField] PlayerExpressions pExp;
    [SerializeField] ActiveCharacterManager activeCharManager;

    void Start() {
        if (activeCharManager == null) {
            activeCharManager = FindObjectOfType<ActiveCharacterManager>();
        }
    }
    void IKillable.Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void IKillable.NotifyDamage()
    {
        HitBuffer();
        if (this.gameObject.name.CompareTo("Fridge") != 0) {
            TrollFaceManager();
        }
    }

    void HitBuffer() {
        if (pExp != null) {
            StartCoroutine(pExp.PlayAnim("exp_x_xfaceB", true, 0.5f));
        }

    }
   
    void TrollFaceManager() {
        
        StartCoroutine(activeCharManager.fridge.GetComponentInChildren<PlayerExpressions>().PlayAnim("exp_trollface", true, 0.5f));
        
    }
}
