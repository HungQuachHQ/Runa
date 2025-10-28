using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneChanger : MonoBehaviour {
    // The below code is for transitioning from the start screen to the tutorial.
    public Animator animator;
    public float fadeTime = 0.5f;

    public void FadeToBlack() {
        animator.Play("FadeToBlack");

        StartCoroutine(DelayFade());
    }

    IEnumerator DelayFade() {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Tutorial");
    }
}
