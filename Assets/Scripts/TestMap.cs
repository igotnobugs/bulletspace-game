/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{

    //Always needed for every Scene
    private STGFramework STGEngine;
    private EVFramework EVFrames;

    //Vectors to call
    public Vector2 vSpawningLocation = new Vector2(7.2f, -5.0f);
    public Vector2 vStartingLocation = new Vector2(7.2f, 4.0f);
    public Vector2 vTopRightLocation = new Vector2(2.45f, 14.5f);
    public Vector2 vCenter = new Vector2(7.2f, 7.2f);

    //Audio to Play
    private AudioSource aAudioSource;
    public AudioClip BGM1;
    public AudioClip BGM2;

    //RigidBodies to Spawn
    public Rigidbody2D rPlayer;
    public Rigidbody2D rRubble;
    public Rigidbody2D rEnemy;
    public Rigidbody2D rEnemy2;
    public Rigidbody2D rEnemy3;
    public Rigidbody2D rBoss;


    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
        EVFrames = GetComponent<EVFramework>();
        aAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //Start the game
        EVFrames.EventGameStart();


        switch (EVFrames.iGlobalStage)
        {
            case 1: //Stage Set Up 
                EVFrames.EventSpawnPlayer(rPlayer, vStartingLocation, transform.rotation, 0, 0, 0, 0);
                EVFrames.EventSpawnPlayer(rEnemy, vCenter, transform.rotation, 0, 0, 0, 0);
                //EVFrames.EventStageEnd(0);
                break;
            case 2:
                break;
        }
    }
}
*/