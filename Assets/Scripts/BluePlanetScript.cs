using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlanetScript : MonoBehaviour {

    private STGFramework STGEngine;
    private EVFramework EVFrames;

    // Use this for initialization
    void Start () {
        STGEngine = GetComponent<STGFramework>();
        EVFrames = GetComponent<EVFramework>();
    }

    // Update is called once per frame
    void Update()
    {
        EVFrames.EventAIStart();
        switch (EVFrames.iMovementStage)
        {
            case 1:
                STGEngine.MoveTowards(new Vector2(0, -9.0f), 5.0f);
                EVFrames.EventMoveEnd(2.0f);
                break;
            case 2:
                STGEngine.MoveTowards(new Vector2(0, -9.2f), 0.02f);
                EVFrames.EventMoveEnd(10.0f);
                break;
            case 3:
                STGEngine.MoveTowards(new Vector2(0, -9.0f), 0.02f);
                EVFrames.EventMoveEnd(10.0f);
                break;
            case 4:
                EVFrames.EventMoveStart(2);
                break;
        }
    }
}
