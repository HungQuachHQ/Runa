using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionTrigger : MonoBehaviour {
    private InstructionManager manager;

    void Start() { 
        manager = FindObjectOfType<InstructionManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            manager.ShowInstruction(gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            manager.HideInstruction();
            Destroy(gameObject);
        }
    }
}
