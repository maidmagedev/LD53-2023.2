using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextLevel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //loadNext();
            SceneTransition scene_Transition = FindObjectOfType<SceneTransition>();
            scene_Transition.behavior = SceneTransition.Behavior.loadNext;
            scene_Transition.FadeOut();
        }
    }

    private void loadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
