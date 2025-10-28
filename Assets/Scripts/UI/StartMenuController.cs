using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartMenuController : MonoBehaviour {
    // The below code handles the button logic for the starting screen.
    private StartSceneChanger changer;

    private void Start() {
        changer = FindObjectOfType<StartSceneChanger>();
    }

    public void OnStartClick() {
        changer.FadeToBlack();
    }

    public void OnExitClick() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
