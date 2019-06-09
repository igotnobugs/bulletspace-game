using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVFramework : MonoBehaviour {

    /* EV FRAMEWORK
    * A collection of functions for oranizing sequences mostly for scenes/stages
    */

    private STGFramework STGEngine;
    private bool bEventSpawnA, bEventSpawnB, bEventSpawnC;
    

    [HideInInspector]
    public float fGlobalDelay, fEnemyAround;
    [HideInInspector]
    public int iTime, iGlobalDelay, iGlobalStage;
    [HideInInspector]
    public bool bEventRunning;


    // Use this for initialization
    void Start () {
        STGEngine = GetComponent<STGFramework>();
	    bEventRunning = false;
    }


	
	// Update is called once per frame
	void Update () {
    }



    #region Events for Stage Manipulation
    //Starts the game by advancing it
    public void EventGameStart()
    {
        if (iGlobalStage == 0)
        {
            iGlobalStage = 1;
        }

        if (iGlobalStage > 0)
        {
            //fTime += Time.deltaTime;
            //iGlobalDelay = Mathf.RoundToInt(fTime);
            fGlobalDelay += Time.deltaTime;
            //Debug.Log(fGlobalDelay);
        }
    }

    //Use only to go back a stage or advance to another stage
    public void EventStageStart(int iEventStage) 
    {
        fGlobalDelay = 0;
        if (iGlobalStage != iEventStage)
        {
            iGlobalStage = iEventStage;
        } else 
	    {
            Debug.Log("Stage already running");
	    }     
    }

    //Resets delay and advances the next stage, fEndTimes should be longer than other delays
    public void EventStageEnd(float fEndTimes)
    {    
        if (fEndTimes < fGlobalDelay)
        {
            iGlobalStage += 1;
            fGlobalDelay = 0;

            bEventSpawnA = false;
            bEventSpawnB = false;
            bEventSpawnC = false;
        }
    }

    //Only ends event and start the corresponding stage if no Event is running
    public void EventIfEnd (int iEventStage)
    {    
        if ((iGlobalStage != iEventStage) && (bEventRunning == false))
        {
            iGlobalStage = iEventStage;
        }
    }

    #endregion

    #region Events for Single Spawning
    public void EventSpawnPlayer(Rigidbody2D rObject, Vector2 vLocation, Quaternion Rotation, float xDirection, float yDirection, float fAdditionalSpeed, float fEventDelay)
    {

        if ((fEventDelay < fGlobalDelay) && (!bEventSpawnA))
        {
            STGEngine.SpawnPlayer(rObject, vLocation, Rotation, xDirection, yDirection, fAdditionalSpeed);
            bEventSpawnA = true;
        }
    }

    public void EventSpawnA(Rigidbody2D rObject, Vector2 vLocation, Quaternion Rotation, float xDirection, float yDirection, float fAdditionalSpeed, float fEventDelay)
    {
        if ((fEventDelay < fGlobalDelay) && (!bEventSpawnA))
        {
            STGEngine.SpawnPrefab(rObject, vLocation, Rotation, xDirection, yDirection, fAdditionalSpeed);
            bEventSpawnA = true;
        }
    }
    public void EventSpawnB(Rigidbody2D rObject, Vector2 vLocation, Quaternion Rotation, float xDirection, float yDirection, float fAdditionalSpeed, float fEventDelay)
    {
        if ((fEventDelay < fGlobalDelay) && (!bEventSpawnB))
        {
            STGEngine.SpawnPrefab(rObject, vLocation, Rotation, xDirection, yDirection, fAdditionalSpeed);
            bEventSpawnB = true;
        }
    }
    public void EventSpawnC(Rigidbody2D rObject, Vector2 vLocation, Quaternion Rotation, float xDirection, float yDirection, float fAdditionalSpeed, float fEventDelay)
    {
        if ((fEventDelay < fGlobalDelay) && (!bEventSpawnC))
        {
            STGEngine.SpawnPrefab(rObject, vLocation, Rotation, xDirection, yDirection, fAdditionalSpeed);
            bEventSpawnC = true;
        }
    }
    #endregion

}
