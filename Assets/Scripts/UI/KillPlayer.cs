using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    // Below is the code for the deadzones. This is used to "kill" the player when they fall of the map and respawn them at a respawn point.
    public GameObject player;
    public Transform respawnPoint;

    void Start() {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.transform.position = respawnPoint.position;
        }
    }
}
