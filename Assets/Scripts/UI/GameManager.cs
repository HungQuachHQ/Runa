using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // This manages the persistant objects. These objects are maintained throughout scenes.
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    private void Awake() {
        if (Instance != null) {
            CleanUpAndDestroy();
            return;
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }

    private void MarkPersistentObjects() {
        foreach (GameObject obj in persistentObjects) {
            if (obj != null) {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy() {
        foreach (GameObject obj in persistentObjects) {
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
