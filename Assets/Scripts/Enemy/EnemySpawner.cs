using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;
    public int spawnCount = 3;
    public bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !hasSpawned) {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    private void SpawnEnemies() {
        for (int i = 0; i < spawnCount && i < spawnPoints.Length; i++) {
            Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
