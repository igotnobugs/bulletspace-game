using UnityEngine;

public class AIFramework : MonoBehaviour
{

    // AI FRAMEWORK
    [HideInInspector]
    public float fAiDelay;
    [HideInInspector]
    public int iMovementStage;

    // Use this for initialization
    void Start()
    {
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

    private void EventCommonReset(int advanceStages = 1)
    {
        iMovementStage += advanceStages;
        fAiDelay = 0;
    }

    ////Use only to go back a move or advance to another move
    public void EventMoveStart(int iMoveStage)
    {
        if (iMovementStage != iMoveStage)
        {
            EventCommonReset(iMoveStage);
        }
    }

    //Resets delay and advances the next stage, fEndTimes should be longer than other delays
    public void EventMoveEnd(float fEndTimes, int advanceStages = 1)
    {
        if (fEndTimes < fAiDelay)
        {
            EventCommonReset(advanceStages);
        }
    }
    /// <summary>
    /// Advance stages when object reached the destination
    /// </summary>
    public void EventMoveLocEnd(Vector3 vectorDestination, int advanceStages = 1)
    {
        if (this.transform.position == vectorDestination)
        {
            EventCommonReset(advanceStages);
        }
    }
    #endregion
}
