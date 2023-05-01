using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Behavior behavior;
    [SerializeField] int desiredSceneNumber;

    public enum Behavior {
        reload,
        changeSceneTo,
        loadNext
    }

    public void FadeOut() {
        animator.SetTrigger("FadeOut");
    }

    public void AnimationEventTransition() {
        switch (behavior)
        {
            case Behavior.changeSceneTo:
                SceneManager.LoadScene(desiredSceneNumber);
                break;
            case Behavior.reload:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case Behavior.loadNext:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }

    private void loadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
