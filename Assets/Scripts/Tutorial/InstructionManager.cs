using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject text;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(DeactivateText());
        }
    }

    private IEnumerator DeactivateText() {
        yield return new WaitForSeconds(5);
        text.SetActive(false);
        Destroy(gameObject);
    }
}
