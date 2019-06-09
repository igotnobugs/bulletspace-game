using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlanetScript : MonoBehaviour {

    private STGFramework STGEngine;
    private AIFramework AIFrames;

    // Use this for initialization
    void Start () {
        STGEngine = GetComponent<STGFramework>();
        AIFrames = GetComponent<AIFramework>();
    }

    // Update is called once per frame
    void Update()
    {
        AIFrames.EventAIStart();
        switch (AIFrames.iMovementStage)
        {
            case 1:
                STGEngine.MoveTowards(new Vector2(0, -9.0f), 5.0f);
                AIFrames.EventMoveEnd(2.0f);
                break;
            case 2:
                STGEngine.MoveTowards(new Vector2(0, -9.2f), 0.02f);
                AIFrames.EventMoveEnd(10.0f);
                break;
            case 3:
                STGEngine.MoveTowards(new Vector2(0, -9.0f), 0.02f);
                AIFrames.EventMoveEnd(10.0f);
                break;
            case 4:
                AIFrames.EventMoveStart(2);
                break;
        }
    }
}
