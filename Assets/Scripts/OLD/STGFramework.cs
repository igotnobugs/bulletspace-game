using UnityEngine;
using UnityEngine.UI;
using GlobalVars;

public class STGFramework : MonoBehaviour
{
    //STG Framework
    //private float fHDirection, fVDirection;
    private AudioSource aAudioSource;

    [HideInInspector]
    public bool bPlayerControlEnabled, bShootSwirl, bShootRight = true;
    [HideInInspector]

    private float fShootTimeAc = 0.0f, fShootSoundTimeAc = 0.0f;
    private float fAngle = 0;

    //For Dialogue Box 
    private Canvas cSTGDialogueCanvas;
    private GameObject oSTGPortraitImage;
    private Text tSTGNameText, tSTGDialogueText;

    public Rigidbody2D rPlayer;

    // Use this for initialization
    void Start() {
        bPlayerControlEnabled = false;
    }

    // Update is called once per frame
    void Update() {
    }

    #region -----------  Spawning Commands
    public void SpawnPlayer(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fAddSpd = 0)
    {
        Rigidbody2D instObject = Instantiate(rObj, vLoc, qRot);
        instObject.velocity = transform.TransformDirection(new Vector2(vDir.x * fAddSpd, vDir.y * fAddSpd));
        instObject.name = "Player";
    }

    public void SpawnPrefab(Rigidbody2D rObj, Vector2 vLoc, Quaternion qRot, Vector2 vDir, float fAddSpd = 0)
    {
        Rigidbody2D instObject = Instantiate(rObj, vLoc, qRot);
        instObject.velocity = transform.TransformDirection(new Vector2(vDir.x * fAddSpd, vDir.y * fAddSpd));
    }
    #endregion

    #region -----------  Special Commands
    public void ActivateShield(Rigidbody2D shieldObject, Vector2 vectorLocation, Quaternion qRot)
    {
        Rigidbody2D instObject = Instantiate(shieldObject, vectorLocation, qRot);
        //instObject.velocity = transform.TransformDirection(new Vector2(vDir.x * fAddSpd, vDir.y * fAddSpd));
    }
    #endregion

    #region -----------  Shooting Commands

    //Shoot a bullet
    public void ShootBullet(Rigidbody2D rBullet, Vector2 vDir, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet = Instantiate(rBullet, transform.position, transform.rotation);
        instBullet.velocity = new Vector2(vDir.x * fSpeed, vDir.y * fSpeed);
    }

    //Shoot a burst bullet
    public void ShootConBullet(Rigidbody2D rBullet, Vector2 vDir, float fFireRate = 10.0f, float fSpeed = 1.0f)
    {
        if (Time.deltaTime + fShootTimeAc > fFireRate)
        {
            fShootTimeAc = 0.0f;
            ShootBullet(rBullet, vDir, fSpeed);
        }
        else
        {
            fShootTimeAc += Time.deltaTime;
        }
    }

    public void ShootSoundBullet(AudioClip aShootSound, float fFireRate = 10.0f)
    {
        aAudioSource = this.GetComponent<AudioSource>();
        if (Time.deltaTime + fShootSoundTimeAc > fFireRate)
        {
            fShootSoundTimeAc = 0.0f;
            this.aAudioSource.PlayOneShot(aShootSound, 1.0f);
        }
        else
        {
            fShootSoundTimeAc += Time.deltaTime;
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
            this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
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
            Vector2 vDir;
            vDir.x = Mathf.Cos(angle);
            vDir.y = Mathf.Sin(angle);
            ShootBullet(rBullet, vDir, fSpeed);
        }       
    }

    //Shoots aimed consecutively at target
    public void ShootConAimed(Rigidbody2D rBullet, string sTargetTag, float fFireRate = 10.0f, float fSpeed = 5.0f)
    {
        GameObject Target = GameObject.FindWithTag(sTargetTag);
        if ((Time.deltaTime + fShootTimeAc > fFireRate) && Target)
        {
            fShootTimeAc = 0.0f;
            float xpos = Target.transform.position.x - this.transform.position.x;
            float ypos = Target.transform.position.y - this.transform.position.y;
            float angle = Mathf.Atan2(ypos, xpos);
            Vector2 vDir;
            vDir.x = Mathf.Cos(angle);
            vDir.y = Mathf.Sin(angle);
            ShootBullet(rBullet, vDir, fSpeed);
        }
        else
        {
            fShootTimeAc += Time.deltaTime;
        }
    }

    //Lead targets --- WIP
    /*
    public void ShootAimedLeading(Rigidbody2D rBullet, string sTarget, float fSpeed = 1.0f)
    {
        Rigidbody2D instBullet;
        float xDir, yDir, angle;
        GameObject Target;
        Target = GameObject.Find(sTarget);

        float xpos = (Target.transform.position.x + fHDirection) - this.transform.position.x;
        float ypos = (Target.transform.position.y + fVDirection) - this.transform.position.y;

        //Debug.Log(xpos);

        //distance = Mathf.Sqrt(Mathf.Pow(xpos,2.0f) + Mathf.Pow(ypos, 2.0f));

        angle = Mathf.Atan2(ypos, xpos);


        xDir = Mathf.Cos(angle);
        yDir = Mathf.Sin(angle);

        float newx = xDir * fSpeed;
        float newy = yDir * fSpeed;

        instBullet = Instantiate(rBullet, transform.position, transform.rotation) as Rigidbody2D;
        instBullet.velocity = new Vector2(newx, newy);
    }
    */

    //Shoot in all directions at once
    public void ShootAround(Rigidbody2D rBullet, int iBulletCount, float fSpeed = 1.0f, int iReps = 1)
    {
        int rep = 0;
        if (rep < iReps)
        {
            for (float angle = 0.0f; angle < 360.0f; angle += (360.0f / iBulletCount))
            {
                var rad = angle * Mathf.Deg2Rad;
                Vector2 vDir;
                vDir.x = Mathf.Sin(rad);
                vDir.y = Mathf.Cos(rad);
                ShootBullet(rBullet, vDir, fSpeed);
            }
            rep++;
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

            var rad = fAngle * Mathf.Deg2Rad;
            Vector2 vDir;
            vDir.x = Mathf.Sin(rad);
            vDir.y = Mathf.Cos(rad);
            ShootBullet(rBullet, vDir, fSpeed);
        }
        else
        {
            fShootTimeAc += Time.deltaTime;
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
        GlobVars.ScoreCounter += iScore;
        tScoreCounter.text = "" + GlobVars.ScoreCounter;
    }

    public void ScoreResetZero(Text tScoreCounter)
    {
        GlobVars.ScoreCounter = 0;
        tScoreCounter.text = "" + GlobVars.ScoreCounter;
    }
    #endregion

    #region -----------  Random UI Commands
    public void UITextChange(Text UIText, string sText)
    {
        UIText.text = sText;
    }

    public void UIPanelShow(GameObject pUIPanel)
    {
        pUIPanel.gameObject.SetActive(true);
    }

    public void UIPanelHide(GameObject pUIPanel)
    {
        pUIPanel.gameObject.SetActive(false);
    }

    public void UIBarSize(GameObject pBar, Vector2 vScale)
    {
        pBar.transform.localScale = vScale;
    }
    #endregion

    /// <summary>
    /// Quit Game
    /// </summary>
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
