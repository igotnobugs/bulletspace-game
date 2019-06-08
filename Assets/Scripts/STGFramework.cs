using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class STGFramework : MonoBehaviour {

    /* STG FRAMEWORK
     * A collection of functions for:
     * Player Movement and Enemy Movement
     * Spawning and shooting
     * Dialogue control
     * UI control
     * etc
     */

    private float fHDirection, fVDirection;
    private AudioSource aAudioSource;

    [HideInInspector]
    public bool bPlayerControlEnabled, bShootSwirl, bShootRight = true, bShootNow = false, bShootSoundNow = false;

    private float fShootTimeAc = 0.0f, fShootSoundTimeAc = 0.0f;
    private float fAngle = 0;
    private bool bReachedLimitLeft, bReachedLimitRight, bReachedLimitUp, bReachedLimitDown;

    private Canvas cSTGDialogueCanvas;
    private GameObject oSTGPortraitImage;
    private Text tSTGNameText, tSTGDialogueText;

    [HideInInspector]
    public int iRep;

    public Rigidbody2D rPlayer;



    // Use this for initialization
    void Start() {
        bPlayerControlEnabled = false;
        iRep = 0;
    }

    // Update is called once per frame
    void Update() {
    }

    #region -----------  Variables
    public static class GlobalVariables
    {
        public static int iScoreCounter;
        public static int iPlayerState;
    }
    #endregion

    #region -----------  Player Controls
    public void PlayerShipMoveLeft()
    {
        if (bPlayerControlEnabled == true)
        {
            //Left 0 
            if (((Input.GetKeyDown(KeyCode.A)) && (fHDirection >= 0) ||
                (Input.GetKey(KeyCode.A)) && (Input.GetKeyUp(KeyCode.D))) && 
                (!bReachedLimitLeft))
            {
                fHDirection = -1.0f;
                Quaternion target = Quaternion.Euler(0, -60, 0);
                this.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 50.0f);
            } 
        }
    }

    public void PlayerShipMoveRight()
    {
        if (bPlayerControlEnabled == true)
        {
            //Right 1
            if (((Input.GetKeyDown(KeyCode.D)) && (fHDirection <= 0) ||
                (Input.GetKey(KeyCode.D)) && (Input.GetKeyUp(KeyCode.A))) &&
                (!bReachedLimitRight))
            {
                fHDirection = 1.0f;
                Quaternion target = Quaternion.Euler(0, 60, 0);
                this.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 50.0f);
            }
        }      
    }

    public void PlayerShipMoveDown()
    {
        if (bPlayerControlEnabled == true)
        {
            //Down 2
            if (((Input.GetKeyDown(KeyCode.S)) && (fVDirection >= 0) ||
                (Input.GetKey(KeyCode.S)) && (Input.GetKeyUp(KeyCode.W))) &&
                (!bReachedLimitDown))

            {
                fVDirection = -1.0f;
            }
        }
    }

    public void PlayerShipMoveUp()
    {
        if (bPlayerControlEnabled == true)
        {
            //Up 3
            if (((Input.GetKeyDown(KeyCode.W)) && (fVDirection <= 0) ||
                (Input.GetKey(KeyCode.W)) && (Input.GetKeyUp(KeyCode.S))) &&
                (!bReachedLimitUp))
            {
                fVDirection = 1.0f;
            }
        }
    }

    public void PlayerShipNotMoving(Vector2 BottomLeftCorner, Vector2 UpperRightCorner)
    {
        if (rPlayer.transform.position.x < BottomLeftCorner.x)
        {
            bReachedLimitLeft = true;
        }
        else
        {
            bReachedLimitLeft = false;
        }

        if (rPlayer.transform.position.x > UpperRightCorner.x)
        {
            bReachedLimitRight = true;
        }
        else
        {
            bReachedLimitRight = false;
        }

        if (rPlayer.transform.position.y > UpperRightCorner.y)
        {
            bReachedLimitUp = true;
        }
        else
        {
            bReachedLimitUp = false;
        }

        if (rPlayer.transform.position.y < BottomLeftCorner.y)
        {
            bReachedLimitDown = true;
        }
        else
        {
            bReachedLimitDown = false;
        }


        if (((!Input.GetKey(KeyCode.A)) && (!Input.GetKey(KeyCode.D))) ||
            ((rPlayer.transform.position.x <= BottomLeftCorner.x) || (rPlayer.transform.position.x >= UpperRightCorner.x)))
        {
            fHDirection = 0;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 50.0f);
        }


        if (((!Input.GetKey(KeyCode.S)) && (!Input.GetKey(KeyCode.W))) ||
            ((rPlayer.transform.position.y <= BottomLeftCorner.y) || (rPlayer.transform.position.y >= UpperRightCorner.y)))
        {
            fVDirection = 0;
        }                 
    }

    public void PlayerMovement(float fSpeed)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            fVDirection = fVDirection / 2;
            fHDirection = fHDirection / 2;
        }

        rPlayer.velocity = new Vector2(fSpeed * fHDirection, fSpeed * fVDirection);      
    }


    public void EnablePlayerControls()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<STGFramework>().bPlayerControlEnabled = true;
    }

    public void DisablePlayerControls()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        Player.GetComponent<STGFramework>().bPlayerControlEnabled = false;
    }

    #endregion

    #region -----------  Spawning Commands
    public void SpawnPlayer(Rigidbody2D rObject, Vector2 vLocation, Quaternion Rotation, float xDirection, float yDirection, float fAdditionalSpeed = 0)
    {
        Rigidbody2D instObject = Instantiate(rObject, vLocation, Rotation);
        instObject.velocity = transform.TransformDirection(new Vector2(xDirection * fAdditionalSpeed, yDirection * fAdditionalSpeed));
        instObject.name = "Player";
    }

    public void SpawnPrefab(Rigidbody2D rObject, Vector2 vLocation, Quaternion Rotation, float xDirection, float yDirection, float fAdditionalSpeed = 0)
    {
        Rigidbody2D instObject = Instantiate(rObject, vLocation, Rotation);
        instObject.velocity = transform.TransformDirection(new Vector2(xDirection * fAdditionalSpeed, yDirection * fAdditionalSpeed));
    }
    #endregion

    #region -----------  Shooting Commands

    //Shoot a bullet
    public void ShootBullet(Rigidbody2D rBullet, float xDir, float yDir, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet = Instantiate(rBullet, transform.position, transform.rotation);
        instBullet.velocity = new Vector2(xDir * fSpeed, yDir * fSpeed);
    }

    //Shoot a burst bullet
    public void ShootConBullet(Rigidbody2D rBullet, float xDir, float yDir, float fFireRate = 10.0f, float fSpeed = 1.0f)
    {

        if (Time.deltaTime + fShootTimeAc > fFireRate)
        {
            fShootTimeAc = 0.0f;
            bShootNow = true;
        }
        else
        {
            fShootTimeAc += Time.deltaTime;
        }

        if (bShootNow)
        {
            ShootBullet(rBullet, xDir, yDir, fSpeed);
            bShootNow = false;
        }
    }

    public void ShootSoundBullet(AudioClip aShootSound, float fFireRate = 10.0f)
    {
        aAudioSource = this.GetComponent<AudioSource>();

        if (Time.deltaTime + fShootSoundTimeAc > fFireRate)
        {
            fShootSoundTimeAc = 0.0f;
            bShootSoundNow = true;
        }
        else
        {
            fShootSoundTimeAc += Time.deltaTime;
        }

        if (bShootSoundNow)
        {
            this.aAudioSource.PlayOneShot(aShootSound, 1.0f);
            bShootSoundNow = false;
        }
    }

    //Shoot bullet oscilating from center of object
    public void ShootOscBullet(Rigidbody2D rBullet, float xDir, float yDir, float fGunLoc, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet;
        
        if (!bShootRight) {
            fGunLoc = fGunLoc * 1;
            Vector2 vGunLocation = new Vector2(transform.position.x + fGunLoc, transform.position.y);
            instBullet = Instantiate(rBullet, vGunLocation, transform.rotation) as Rigidbody2D;
            instBullet.velocity = new Vector2(xDir * fSpeed, yDir * fSpeed);
            bShootRight = true;
        } else
        {
            fGunLoc = fGunLoc * -1;
            Vector2 vGunLocation = new Vector2(transform.position.x + fGunLoc, transform.position.y);
            instBullet = Instantiate(rBullet, vGunLocation, transform.rotation);
            instBullet.velocity = new Vector2(xDir * fSpeed, yDir * fSpeed);
            bShootRight = false;
        }
       
    }

    //Point self towards
    public void ShootAiming(string sTargetTag)
    {
        GameObject Target = GameObject.FindWithTag(sTargetTag);

        if (Target)
        {
            float xpos = Target.transform.position.x - this.transform.position.x;
            float ypos = Target.transform.position.y - this.transform.position.y;
            float angle = (Mathf.Atan2(ypos, xpos) * Mathf.Rad2Deg);

            //float xDir = Mathf.Cos(angle);
            //float yDir = Mathf.Sin(angle);
            this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            //Quaternion target = Quaternion.Euler(0, 0, angle);
            //this.transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
        }
    }

    //Shoots aimed at Target's name
    public void ShootAimed(Rigidbody2D rBullet, string sTargetTag, float fSpeed = 1.0f)
    {
        GameObject Target = GameObject.FindWithTag(sTargetTag);

        if (Target)
        {
            float xpos = Target.transform.position.x - this.transform.position.x;
            float ypos = Target.transform.position.y - this.transform.position.y;
            float angle = Mathf.Atan2(ypos, xpos);
            float xDir = Mathf.Cos(angle);
            float yDir = Mathf.Sin(angle);
            ShootBullet(rBullet, xDir, yDir, fSpeed);
        }       
    }


    //Lead targets --- WIP
    public void ShootAimedLeading(Rigidbody2D rBullet, string sTarget, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet;
        float xDir, yDir, angle;
        GameObject Target;
        Target = GameObject.Find(sTarget);

        float xpos = (Target.transform.position.x + fHDirection) - this.transform.position.x;
        float ypos = (Target.transform.position.y + fVDirection) - this.transform.position.y;

        Debug.Log(xpos);

        //distance = Mathf.Sqrt(Mathf.Pow(xpos,2.0f) + Mathf.Pow(ypos, 2.0f));

        angle = Mathf.Atan2(ypos, xpos);


        xDir = Mathf.Cos(angle);
        yDir = Mathf.Sin(angle);

        float newx = xDir * fSpeed;
        float newy = yDir * fSpeed;

        instBullet = Instantiate(rBullet, transform.position, transform.rotation) as Rigidbody2D;
        instBullet.velocity = new Vector2(newx, newy);
    }

    //Shoot in all directions at once
    public void ShootAround(Rigidbody2D rBullet, int iBulletCount, float fSpeed = 1.0f, int iReps = 1)
    {
        if (iRep < iReps)
        {
            for (float angle = 0.0f; angle < 360.0f; angle += (360.0f / iBulletCount))
            {
                var rad = angle * Mathf.Deg2Rad;
                float x = Mathf.Sin(rad);
                float y = Mathf.Cos(rad);
                ShootBullet(rBullet, x, y, fSpeed);
            }
            iRep++;
        }
    }

    //Shoot in a clockwise, by default, swirly pattern
    public void ShootAroundSwirl(Rigidbody2D rBullet, int iBulletCount, float fFireRate = 0.02f, float fSpeed = 1.0f, int iMaxRots = 1, bool bReverse = false)
    {
       if (Time.deltaTime + fShootTimeAc > fFireRate)
        {
            fShootTimeAc = 0.0f;
            if (!bReverse)
            {
                fAngle += (360.0f / iBulletCount);
            }
            else
            {
                fAngle -= (360.0f / iBulletCount);
            }
            bShootNow = true;
        }
        else
        {
            fShootTimeAc += Time.deltaTime;
        }

        if (bShootNow)
        {
            var rad = fAngle * Mathf.Deg2Rad;
            float x = Mathf.Sin(rad);
            float y = Mathf.Cos(rad);

            ShootBullet(rBullet, x, y, fSpeed);
            bShootNow = false;
        }
    }
    #endregion

    #region -----------  Movement Commands
    //Move a different object
    public void ObjectMoveTowards(Rigidbody2D rObject, Vector2 vLocation, float fAdditionalSpeed = 1.0f)
    {
        rObject.transform.position = Vector2.MoveTowards(new Vector2(rObject.transform.position.x, rObject.transform.position.y), vLocation, fAdditionalSpeed * Time.deltaTime);
    }

    //Move this object
    public void MoveTowards(Vector2 vLocation, float fAdditionalSpeed = 1.0f)
    {
        this.transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), vLocation, fAdditionalSpeed * Time.deltaTime);
    }
    #endregion

    #region -----------  Dialogue Commands
    public void DialogueSetup(Canvas cDialogueCanvas, GameObject oPortraitImage, Text tNameText, Text tDialogueText)
    {
        cSTGDialogueCanvas = cDialogueCanvas;
        oSTGPortraitImage = oPortraitImage;
        tSTGNameText = tNameText;
        tSTGDialogueText = tDialogueText;
    }

    public void DialogueShow(string sNameText, string sDialogueText)
    {
        cSTGDialogueCanvas.gameObject.SetActive(true);
        tSTGNameText.text = sNameText;
        tSTGDialogueText.text = sDialogueText;
    }

    public void DialogueHide(Canvas cDialogueCanvas)
    {
        cDialogueCanvas.gameObject.SetActive(false);
    }
    #endregion

    #region -----------  Score Counter Commands
    public void ScoretoAddtoCounter(Text tScoreCounter, int iScore)
    {
        GlobalVariables.iScoreCounter += iScore;
        tScoreCounter.text = "" + GlobalVariables.iScoreCounter;
    }

    public void ScoreResetZero(Text tScoreCounter)
    {
        GlobalVariables.iScoreCounter = 0;
        tScoreCounter.text = "" + GlobalVariables.iScoreCounter;
    }
    #endregion

    #region -----------  Random UI Commands
    public void UITextChange(Text UIText, string sText)
    {
        UIText.text = sText;
    }

    public void UIBossHealthPanelShow(GameObject pBossHealthPanel)
    {
        pBossHealthPanel.gameObject.SetActive(true);
    }

    public void UIBossHealthPanelHide(GameObject pBossHealthPanel)
    {
        pBossHealthPanel.gameObject.SetActive(false);
    }

    public void UIBossHealthBarSize(GameObject pBossHealthBar, float percentage)
    {
        pBossHealthBar.transform.localScale = new Vector2(percentage, 1);
    }
    #endregion
}
