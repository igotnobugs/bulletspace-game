using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFramework : MonoBehaviour
{

    /* AI FRAMEWORK
    * A collection of functions for oranizing sequences mostly for movement or decisions
    */

    private STGFramework STGEngine;


    [HideInInspector]
    public float fAiDelay;
    [HideInInspector]
    public int iMovementStage;


    // Use this for initialization
    void Start()
    {
        STGEngine = GetComponent<STGFramework>();
    }



    // Update is called once per frame
    void Update()
    {
    }


    #region Events for Object Manipulation
    //Starts Movement side of the framework
    public void EventAIStart()
    {
        if (iMovementStage == 0)
        {
            iMovementStage = 1;
        }

        if (iMovementStage > 0)
        {
            fAiDelay += Time.deltaTime;
        }
    }

    ////Use only to go back a move or advance to another move
    public void EventMoveStart(int iMoveStage)
    {
        if (iMovementStage != iMoveStage)
        {
            iMovementStage = iMoveStage;
        }
    }

    //Resets delay and advances the next stage, fEndTimes should be longer than other delays
    public void EventMoveEnd(float fEndTimes)
    {
        if (fEndTimes < fAiDelay)
        {
            iMovementStage += 1;
            fAiDelay = 0;
            STGEngine.iRep = 0;
        }
    }
    #endregion
}
