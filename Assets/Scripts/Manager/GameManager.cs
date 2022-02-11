using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtils;
using UnityEngine.Timeline;
public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    public PlayerShip playerShip;
    public Dialogue startingDialogue;
    public DialogueManager dialogueManager;
    public SpawnManager spawnManager;
    public Vector2 vStartingLocation;


    [Header("Debug")]
    //Debug Skips
    public bool skipToRubble = false;
    public bool skipToFight = false;
    public bool skipToRocketWave = false;
    public bool skipToBoss = false;

    public PlayerShip spawnedPlayerShip;

    // Start is called before the first frame update
    void Start()
    {
        spawnedPlayerShip = Instantiate(playerShip, vStartingLocation, Quaternion.identity);
        dialogueManager.TriggerDialogue(startingDialogue, () =>
            spawnManager.StartSpawning()
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
