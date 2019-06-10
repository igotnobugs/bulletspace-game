using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVFramework : MonoBehaviour
{
    /* EV FRAMEWORK
    * A collection of functions for oranizing sequences mostly for scenes/stages
    */

    private STGFramework STGEngine;
    private bool bEventSpawnPlayer = false;
    private bool bEventSpawnA = false, bEventSpawnB = false, bEventSpawnC = false;
    private bool bEventSpawnHorMult = false;

    //public int iThingsToSpawn;

    [HideInInspector]
    public float fGlobalDelay, fEnemyAround;
    [HideInInspector]
    public int iTime, iGlobalDelay, iGlobalStage;
    [HideInInspector]
    public bool bEventRunning;
    [HideInInspector]


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

            bEventSpawnPlayer = false;
            bEventSpawnA = false;
            bEventSpawnB = false;
            bEventSpawnC = false;
            bEventSpawnHorMult = false;
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
    public void EventSpawnPlayer(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, float fXDir, float fYDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnPlayer))
        {
            STGEngine.SpawnPlayer(rObj, vLoc, qRot, fXDir, fYDir, fAddSpd);
            Debug.Log("Event Player Spawn Success");
            bEventSpawnPlayer = true;
        } else
        {
            //Debug.Log("Event Player Spawn Failed");
        }
    }

    public void EventSpawnA(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, float fXDir, float fYDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnA))
        {
            STGEngine.SpawnPrefab(rObj, vLoc, qRot, fXDir, fYDir, fAddSpd);
            Debug.Log("Event Spawn A Success");
            bEventSpawnA = true;
        } else
        {
            //Debug.Log("Event Spawn A Failed" + " " + bEventSpawnA + " " + fEvDelay + " " + fGlobalDelay);
        }
    }
    public void EventSpawnB(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, float fXDir, float fYDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnB))
        {
            STGEngine.SpawnPrefab(rObj, vLoc, qRot, fXDir, fYDir, fAddSpd);
            Debug.Log("Event Spawn B Success");
            bEventSpawnB = true;
        } else
        {
            //Debug.Log("Event Spawn B Failed" + " " + bEventSpawnB + " " + fEvDelay + " " + fGlobalDelay);
        }
    }
    public void EventSpawnC(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, float fXDir, float fYDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnC))
        {
            STGEngine.SpawnPrefab(rObj, vLoc, qRot, fXDir, fYDir, fAddSpd);
            Debug.Log("Event Spawn C Success");
            bEventSpawnC = true;
        } else
        {
            //Debug.Log("Event Spawn C Failed" + " " + bEventSpawnC + " " + fEvDelay + " " + fGlobalDelay);
        }
    }
    #endregion

    public void EventSpawnHorMulti(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, float fXDir, float fYDir, float fSpace, float fCount, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnHorMult))
        {
            int i = 0;
            while ( i < fCount)
            {
                vLoc.x = vLoc.x + (fSpace * i);
                STGEngine.SpawnPrefab(rObj, vLoc, qRot, fXDir, fYDir, fAddSpd);
                i++;
            }
            bEventSpawnHorMult = true;
        }
    }
}
         