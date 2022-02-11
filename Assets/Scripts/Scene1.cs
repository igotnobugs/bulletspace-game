using UnityEngine;
using UnityEngine.UI;
using GameUtils;
using GlobalVars;

public class Scene1 : MonoBehaviour
{
    //Always needed for every Scene
    //private STGFramework STGEngine;
    //private EVFramework EVFrames;

    //Vectors to call
    public Vector2 vStartingLocation;
    public Vector2 vCenter;
    public float fTopSpawn;
    private Vector2 vStayStill = Util.Vec2(0, 0);
    private Vector2 vGoDown = Util.Vec2(0, -1.0f);
    //public Vector2 BottomLeftBorder = Util.Vec2(-4.5f, -4.5f);
    //public Vector2 UpperRightBorder = Util.Vec2(4.5f, 4.5f);
    public Quaternion FaceDown = new Quaternion(0, 0, 180, 0);

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

    //UI Things
    public Canvas cDialogueCanvas;
    public GameObject oPortraitImage1;
    public Text tNameText;
    public Text tDialogueText;
    public Text tPlayerState;
    public Text tScoreCounter;
    public GameObject pBossHealthPanel;
    public GameObject pBossHealthBar;
    public Image PlayerStateImage;
    public GameObject pPlayerGunHeatPanel;
    public GameObject pPlayerGunHeatBar;

    //Random Things
    //private bool bPlayerSpawned = false;
    public bool bResetOnGoing = false;
    //private float TimeT;
    //private int TimeTR;
    //private bool bScoreAdded = false;

    //Character Things
    //private readonly string sGreenAI = "<color=#21FF00>Green AI</color>";

    //Stage Scenes
    //private bool bGameStarted = false;
    //private bool bToldtoKillSelf = false;
    //private bool bKnowsWhatFDoes = false;
    //private bool bKilledBeforeKnowing = false;
    //private bool bBossHasArrived = false;

    //Debug Skips
    public bool bSkipToRubble = false;
    public bool bSkipToFight = false;
    public bool bSkipToRocketWave = false;
    public bool bSkipToBoss = false;


    public static KeyCode shieldKey = KeyCode.E;
    public static KeyCode suicideKey = KeyCode.F;
    public static KeyCode brakeKey = KeyCode.LeftShift;
    public static KeyCode quitKey = KeyCode.Escape;

    // Use this for initialization
    void Start() {
        //STGEngine = GetComponent<STGFramework>();
        //EVFrames = GetComponent<EVFramework>();
        aAudioSource = GetComponent<AudioSource>();

        //Limit Framerate
        //Application.targetFrameRate = 60;

        //Set the Player Border
        //GlobVars.BottomLeftBorder = BottomLeftBorder;
        //GlobVars.UpperRightBorder = UpperRightBorder;
    }

    // Update is called once per frame
    void Update() {

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    STGEngine.Quit();
        //}

        //For all Stages
        /*
        if (bPlayerSpawned)
        {

            //GameObject Player = GameObject.FindWithTag("Player");
            //if (!Player)
            //{
            //    EVFrames.EventStageStart(80); //GameOverStage
            //    bPlayerSpawned = false;
            //}

            //PlayerController PlayerHeat = Player.GetComponent<PlayerController>();
            //float HeatPercentage = (PlayerHeat.heatSink / PlayerHeat.HEATSINK_MAX);
            //STGEngine.UIBarSize(pPlayerGunHeatBar, Util.Vec2(1, HeatPercentage));

            //Scoring system add 1 point every 2 seconds
            TimeT += Time.deltaTime;
            TimeTR = Mathf.FloorToInt(TimeT);
            if (TimeTR % 2 != 0)
            {
                bScoreAdded = false;
            }
            if ((TimeTR % 2 == 0) && (!bScoreAdded) && (EVFrames.iGlobalStage > 9))
            {
                STGEngine.ScoretoAddtoCounter(tScoreCounter, 1);
                TimeTR++;
                bScoreAdded = true;
            }
        }

        //Boss has arrived then show the UI
        if (bBossHasArrived)
        {
            GameObject Boss = GameObject.Find("Boss");
            if (Boss)
            {
                Boss1Script BossHealth = Boss.GetComponent<Boss1Script>();
                if (BossHealth.iHealth >= 0)
                {
                    STGEngine.UIPanelShow(pBossHealthPanel);
                    float BossPercentage = (BossHealth.iHealth / BossHealth.iMaxHealth);
                    STGEngine.UIBarSize(pBossHealthBar, Util.Vec2(BossPercentage, 1));
                }
            }
            else
            {
                STGEngine.UIPanelHide(pBossHealthPanel);
                bBossHasArrived = false;
            }
        }

        #region Scene Level
        //Start the game
        EVFrames.EventGameStart();
        switch (EVFrames.iGlobalStage)
        {
            case 1: //Stage Set Up 
                aAudioSource.clip = BGM1;
                aAudioSource.Play();
                STGEngine.DialogueSetup(cDialogueCanvas, oPortraitImage1, tNameText, tDialogueText);

                //Reset 
                STGEngine.UIPanelHide(pBossHealthPanel);
                STGEngine.ScoreResetZero(tScoreCounter);
                bResetOnGoing = false;
                bToldtoKillSelf = false;
                STGEngine.DialogueHide(cDialogueCanvas);
                bBossHasArrived = false;

                EVFrames.EventStageEnd(0);
                break;
            case 2:
                EVFrames.EventSpawnPlayer(rPlayer, vStartingLocation, transform.rotation, Util.Vec2 (0, 0) , 0, 0);
                bPlayerSpawned = true;

                if (bSkipToRubble)
                { EVFrames.EventStageStart(9); }
                else if (bSkipToFight)
                { EVFrames.EventStageStart(12); }
                else if (bSkipToRocketWave)
                { EVFrames.EventStageStart(27); }
                else if (bSkipToBoss)
                { EVFrames.EventStageStart(40); }
                else
                {
                    STGEngine.DialogueShow(sGreenAI, "Greetings! \nHow are you today?");
                    EVFrames.EventStageEnd(4);
                }
               
                break;
            case 3:
                if (bGameStarted)
                {
                    EVFrames.EventStageStart(8);
                }
                else
                {
                    STGEngine.DialogueShow(sGreenAI, "Do you remember how to use \n<b>W A S D</b> to move your ship? \nWhy don't you try it right now?");
                    EVFrames.EventStageEnd(7);
                }
                break;
            case 4:
                STGEngine.DialogueShow(sGreenAI, "Next, \nHold <b>Space</b> to shoot \nYou have <i>infinite</i> ammo, so don't worry.");
                EVFrames.EventStageEnd(6);
                break;
            case 5:
                if ((bKilledBeforeKnowing) || (bKnowsWhatFDoes))
                {
                    STGEngine.DialogueShow(sGreenAI, "Lastly, \nPress <b>F</b> to...\nI think you know what it does.");
                }
                else
                {
                    STGEngine.DialogueShow(sGreenAI, "Lastly, \nPress <b>F</b> to <b>kill</b> yourself. \nDo it right now.");
                    bToldtoKillSelf = true;
                }
                EVFrames.EventStageEnd(5);
                break;
            case 6:
                STGEngine.DialogueHide(cDialogueCanvas);
                if ((bKilledBeforeKnowing) || (bKnowsWhatFDoes))
                {
                    EVFrames.EventStageStart(8);
                } else
                {
                    EVFrames.EventStageEnd(3);
                }
                break;
            case 7:
                STGEngine.DialogueShow(sGreenAI, "Hmmph. \nDon't forget to fire up your <b>Force Field</b> when it gets tough by pressing <b>E</b>.");
                EVFrames.EventStageEnd(6);
                break;
            case 8:
                STGEngine.DialogueShow(sGreenAI, "Try to not get yourself killed this time alright?\nThere's some junk from WW3 for you to shoot at.");
                bGameStarted = true;
                bToldtoKillSelf = false;
                EVFrames.EventStageEnd(5);
                break;
            case 9:
                STGEngine.DialogueHide(cDialogueCanvas);
                EVFrames.EventSpawnA(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 1.0f, 2);
                EVFrames.EventSpawnC(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 1.0f, 4);
                EVFrames.EventStageEnd(9);
                break;
            case 10:
                EVFrames.EventSpawnA(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 2.0f, 0);
                EVFrames.EventSpawnB(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 1.0f, 2);
                EVFrames.EventSpawnC(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 3.0f, 4);
                EVFrames.EventStageEnd(9);
                break;
            case 11:
                EVFrames.EventSpawnA(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 3.0f, 2);
                EVFrames.EventSpawnC(rRubble, Util.Vec2(Random.Range(3.0f, 11.0f), fTopSpawn), transform.rotation, vGoDown, 1.0f, 4);
                EVFrames.EventStageEnd(9);
                break;
            case 12:
                aAudioSource.clip = BGM2;
                aAudioSource.Play();
                STGEngine.DialogueShow(sGreenAI, "Wait!");
                EVFrames.EventStageEnd(2);
                break;
            case 13:
                STGEngine.DialogueShow(sGreenAI, "Something different is up ahead.");
                EVFrames.EventStageEnd(6);
                break;
            case 14:
                STGEngine.DialogueShow(sGreenAI, "Watch out!");
                EVFrames.EventStageEnd(2);
                break;
            case 15: // 1st Beat
                STGEngine.DialogueShow(sGreenAI, "Watch out!\n\nPirates!");
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(0.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 16:
                STGEngine.DialogueHide(cDialogueCanvas);
                EVFrames.EventSpawnHorMulti(rEnemy, Util.Vec2(-1.0f, fTopSpawn), FaceDown, vGoDown, 2.0f, 2, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 17:
                
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(-2.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(2.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(2);
                break;
            case 18: // 2nd Beat
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(3.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(11.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 19:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(4.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(10.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 20:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(5.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(9.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(2);
                break;
            case 21: // 3rd Beat
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(5.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(9.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 22: 
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(5.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(9.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 23: 
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(5.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(9.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(2);
                break;
            case 24: // 4th Beat
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(6.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(8.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 25: 
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(6.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(8.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 26: 
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(7.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(2);
                break;
            case 27:
                EVFrames.EventSpawnA(rEnemy2, Util.Vec2(3.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy2, Util.Vec2(11.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 3);
                EVFrames.EventStageEnd(5);
                break;
            case 28:
                EVFrames.EventSpawnA(rEnemy2, Util.Vec2(3.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy2, Util.Vec2(11.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 3);
                EVFrames.EventStageEnd(5);
                break;
            case 29:
                EVFrames.EventSpawnA(rEnemy2, Util.Vec2(3.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy2, Util.Vec2(11.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 3);
                EVFrames.EventStageEnd(5);
                break;
            case 30:
                EVFrames.EventSpawnA(rEnemy2, Util.Vec2(3.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy2, Util.Vec2(11.2f, fTopSpawn), FaceDown, vGoDown, 1.0f, 3);
                EVFrames.EventStageEnd(10);
                break;
            case 31: //Extended Wave
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(3.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(11.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 32:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(4.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(10.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 33:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(5.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(9.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 34:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(6.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(8.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 35:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(7.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(7.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 36:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(8.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(6.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 37:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(9.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(5.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 38:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(10.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(4.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(1);
                break;
            case 39:
                EVFrames.EventSpawnA(rEnemy, Util.Vec2(11.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventSpawnB(rEnemy, Util.Vec2(3.0f, fTopSpawn), FaceDown, vGoDown, 1.0f, 0);
                EVFrames.EventStageEnd(5);
                break;
            case 40: //Boss
                EVFrames.EventSpawnA(rBoss, Util.Vec2(0, fTopSpawn), FaceDown, vStayStill, 0, 0);
                bBossHasArrived = true;
                //GameObject Boss = GameObject.FindWithTag("Boss");
                EVFrames.EventIfDeadEnd("Enemy", 90, 1);
                //EVFrames.EventStageEnd(1);
                break;
            case 41: //Boss
                break;
            case 42:
                STGEngine.DialogueShow(sGreenAI, "Hey \n\nThe music is about to end.");
                EVFrames.EventStageEnd(10);
                break;
            case 43:
                STGEngine.DialogueHide(cDialogueCanvas);
                EVFrames.EventStageEnd(10);
                break;
            case 44:
                STGEngine.DialogueShow(sGreenAI, "Hurry it up. What are you doing?.");
                EVFrames.EventStageEnd(10);
                break;
            case 45:
                STGEngine.DialogueShow(sGreenAI, ":|");
                break;

            case 80: //Game Over State
                if (!bGameStarted)
                {
                    STGEngine.DialogueShow(sGreenAI, "Try not to press random buttons alright? \n\nRetry in 3 seconds");
                    bKilledBeforeKnowing = true;
                    EVFrames.EventStageEnd(3);
                }

                if (bToldtoKillSelf)
                {
                    STGEngine.DialogueShow(sGreenAI, "What an idiot... \n\nRetry in 3 seconds");
                    bKnowsWhatFDoes = true;
                    EVFrames.EventStageEnd(3);
                }

                if ((bGameStarted) && (!bToldtoKillSelf))
                {
                    STGEngine.DialogueShow(sGreenAI, "Hello? \nAre you there? \n\nRetry in 3 seconds");
                    EVFrames.EventStageEnd(3);
                }
                break;
            case 81:
                bResetOnGoing = true;
                EVFrames.EventStageStart(1);             
                break;

            case 90: //Victory
                STGEngine.DialogueShow(sGreenAI, "You did it!\n\nCongratulations. Hooray.");
                EVFrames.EventStageEnd(5);
                break;
            case 91:
                STGEngine.DialogueHide(cDialogueCanvas);
                EVFrames.EventStageEnd(5);
                break;
            case 92:
                STGEngine.DialogueShow(sGreenAI, "The game is over.");
                EVFrames.EventStageEnd(5);
                break;
            case 93:
                STGEngine.DialogueShow(sGreenAI, "Press F");
                EVFrames.EventStageEnd(5);
                break;
            case 94:
                STGEngine.DialogueShow(sGreenAI, ":)");
                EVFrames.EventStageEnd(5);
                break;
        }
        #endregion
        */
    }

}
