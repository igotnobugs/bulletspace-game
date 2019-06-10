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


    // Use this for initialization
    void Start () {
        STGEngine = GetComponent<STGFramework>();
    }


	
	// Update is called once per frame
	void Update () {
    }



    #region Events for Stage Manipulation
    /// <summary>
    /// Starts the game by advancing it
    /// </summary>
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
    /// <summary>
    /// Use only to go back a stage or advance to another stage
    /// </summary>
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

    /// <summary> 
    /// Resets delay and advances the next stage, fEndTimes should be longer than other delays
    /// </summary> 
    public void EventStageEnd(float fEndTimes)
    {    
        if (fEndTimes < fGlobalDelay)
        {
            iGlobalStage += 1;
            fGlobalDelay = 0;

            //bEventSpawnPlayer = false;
            bEventSpawnA = false;
            bEventSpawnB = false;
            bEventSpawnC = false;
            bEventSpawnHorMult = false;
        }
    }

    /// <summary>
    /// Ends when the Target is dead, Goes to the Stage with a delay
    /// </summary> 
    public void EventIfDeadEnd(string sTargetTag, int iEventStage, float fDelay = 0)
    {
        GameObject Target = GameObject.FindWithTag(sTargetTag);
        if (!Target)
        {
            if ((iGlobalStage != iEventStage) && (fDelay < fGlobalDelay))
            {
                fGlobalDelay = 0;
                iGlobalStage = iEventStage;
            } else
            {
                Debug.Log("Current Stage is already the stage to go to.");
            }
        } else
        {
            fDelay += Time.deltaTime;
        }
    }
    #endregion

    #region Events for Single Spawning
    /// <summary>
    /// Spawn the player
    /// </summary>
    /// <param name="rObj">This should be the RigidBody2D of the Player</param>
    /// <param name="vLoc">Vector2 Location to Spawn the Player in.</param>
    /// <param name="qRot">Quaternion Rotation of the Player.</param>
    /// <param name="vDir">Vector2 Direction to Spawn the Player at</param>
    /// <param name="fAddSpd">Additional Speed for Direction</param>
    /// <param name="fEvDelay">Delay to Spawn the Player</param>
    public void EventSpawnPlayer(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnPlayer))
        {
            STGEngine.SpawnPlayer(rObj, vLoc, qRot, vDir, fAddSpd);
            Debug.Log("Event Player Spawn Success");
            bEventSpawnPlayer = true;
        } else
        {
            //Debug.Log("Event Player Spawn Failed");
        }
    }

    public void EventSpawnA(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnA))
        {
            STGEngine.SpawnPrefab(rObj, vLoc, qRot, vDir, fAddSpd);
            Debug.Log("Event Spawn A Success");
            bEventSpawnA = true;
        } else
        {
            //Debug.Log("Event Spawn A Failed" + " " + bEventSpawnA + " " + fEvDelay + " " + fGlobalDelay);
        }
    }
    public void EventSpawnB(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnB))
        {
            STGEngine.SpawnPrefab(rObj, vLoc, qRot, vDir, fAddSpd);
            Debug.Log("Event Spawn B Success");
            bEventSpawnB = true;
        } else
        {
            //Debug.Log("Event Spawn B Failed" + " " + bEventSpawnB + " " + fEvDelay + " " + fGlobalDelay);
        }
    }
    public void EventSpawnC(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnC))
        {
            STGEngine.SpawnPrefab(rObj, vLoc, qRot, vDir, fAddSpd);
            Debug.Log("Event Spawn C Success");
            bEventSpawnC = true;
        } else
        {
            //Debug.Log("Event Spawn C Failed" + " " + bEventSpawnC + " " + fEvDelay + " " + fGlobalDelay);
        }
    }
    #endregion

    #region Events for Multi Spawning
    public void EventSpawnHorMulti(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fSpace, float fCount, float fAddSpd, float fEvDelay)
    {
        if ((fEvDelay < fGlobalDelay) && (!bEventSpawnHorMult))
        {
            int i = 0;
            while ( i < fCount)
            {
                vLoc.x = vLoc.x + (fSpace * i);
                STGEngine.SpawnPrefab(rObj, vLoc, qRot, vDir, fAddSpd);
                i++;
            }
            bEventSpawnHorMult = true;
        }
    }
    #endregion
}
         