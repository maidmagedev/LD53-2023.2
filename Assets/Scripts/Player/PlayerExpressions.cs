using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the facial expressions of the player.
public class PlayerExpressions : MonoBehaviour
{

    [SerializeField] Animator animator;
    public List<string> animClips;
    public int currentIndex = 0;
    public bool changeExpression;
    int animCallCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(AnimationClip clip in animator.runtimeAnimatorController.animationClips) {
            animClips.Add(clip.name);
        }
        
        animator.Play("action_idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (changeExpression) {
            StartCoroutine(PlayAnim(animClips[currentIndex], false, 0));
            changeExpression = !changeExpression;
        }
    }

    // public void PlayAnim(string clipName) {
    //     animator.Play(clipName);
    // }

    public IEnumerator PlayAnim(string clipName, bool returnToIdle, float duration) {
        animator.Play(clipName);
        animCallCount++;
        yield return new WaitForSeconds(duration);
        animCallCount--;
        if (animCallCount == 0 && returnToIdle) {
            animator.Play("action_idle");
        }
        yield return null;
    }


}
