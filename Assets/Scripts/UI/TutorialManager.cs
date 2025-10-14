using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject moveText;
    public GameObject jumpText;
    public GameObject sprintText;

    private void Start() {
        moveText.SetActive(true);
        jumpText.SetActive(false);
        sprintText.SetActive(false);
    }

    void Update()
    {
        if (moveText.activeSelf && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            StartCoroutine(ShowPopupWithDelay(jumpText, moveText, 1.0f));
        }
        if (jumpText.activeSelf && Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(ShowPopupWithDelay(sprintText, jumpText, 1.0f));
        }
        if (sprintText.activeSelf && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(HidePopupWithDelay(sprintText, 1.0f));
        }
    }

    IEnumerator ShowPopupWithDelay(GameObject show, GameObject hide, float delay)
    {
        hide.SetActive(false);
        yield return new WaitForSeconds(delay);
        show.SetActive(true);
    }

    IEnumerator HidePopupWithDelay(GameObject hide, float delay)
    {
        yield return new WaitForSeconds(delay);
        hide.SetActive(false);
    }
}
