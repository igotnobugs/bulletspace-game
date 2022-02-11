using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawning")]
    public Transform SpawnDirection;
    public Transform FarLeftSpawn;
    public Transform FarRightSpawn;
    public Wave wave;

    private float xStep;
    private int lineIndex = 0;
    private PlayableDirector spawnDirector;
    

    private void Awake() {
        spawnDirector = GetComponent<PlayableDirector>();
    }

    // Start is called before the first frame update
    private void Start()
    {    
        xStep = Mathf.Abs(FarLeftSpawn.position.x - FarRightSpawn.position.x);

        if (wave.spawnPoints - 1 <= 0) {
            xStep = xStep / 1.0f;
        } else {
            xStep = xStep / (wave.spawnPoints - 1);
        }      
    }

    public void StartSpawning() {
        spawnDirector.Play();
    }

    public void SpawnEnemy() {

        if (wave.lines.Length <= lineIndex) {
            Debug.Log(lineIndex + " doesn't exist");
            return;
        }

        Wave.Line currentLine = wave.lines[lineIndex];
        Wave.Line.Spawn[] lineSpawns = currentLine.spawns;

        if (lineSpawns.Length <= 0) {
            Debug.Log("No enemy to spawn at " + lineIndex );
            return;
        }

        for (int i = 0; i < lineSpawns.Length; i++) {
            float xSpawnPosition = FarLeftSpawn.position.x + (xStep *  (lineSpawns[i].spawnPosition - 1));
            Vector2 spawnLocation = new Vector2(xSpawnPosition, transform.position.y);
            EnemyShip spawnedEnemyShip = Instantiate(lineSpawns[i].enemyShip,
                spawnLocation,
                SpawnDirection.rotation);
        }

        lineIndex++;
 
    }
}
