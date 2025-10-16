using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionManager : MonoBehaviour {
    public TextMeshProUGUI tutorialText;

    private Dictionary<string, string> instructions = new Dictionary<string, string>()
    {
        { "MoveCollider", "Press the left/right arrow keys to move." },
        { "JumpCollider", "Press the up arrow to jump." },
        { "SprintCollider", "Hold left shift while moving to sprint." }
    };

    public void ShowInstruction(string colliderName) {
        if (instructions.ContainsKey(colliderName)) {
            tutorialText.text = instructions[colliderName];
            tutorialText.gameObject.SetActive(true);
        }
    }

    public void HideInstruction() {
        tutorialText.gameObject.SetActive(false);
    }
}
