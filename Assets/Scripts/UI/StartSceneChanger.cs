using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneChanger : MonoBehaviour {
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
