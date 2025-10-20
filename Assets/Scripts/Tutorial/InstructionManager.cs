using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionManager : MonoBehaviour {
    public TextMeshProUGUI tutorialText;

    public void ShowInstruction(string colliderName) {
        if (colliderName == "MoveCollider") {
            tutorialText.text = "Press the left/right arrow keys to move.";
        }
        else if (colliderName == "JumpCollider") {
            tutorialText.text = "Press the up arrow to jump.";
        }
        else if (colliderName == "SprintCollider") {
            tutorialText.text = "Hold left shift while moving to sprint.";
        }
        
        tutorialText.gameObject.SetActive(true);
    }

    public void HideInstruction() {
        tutorialText.gameObject.SetActive(false);
    }
}
