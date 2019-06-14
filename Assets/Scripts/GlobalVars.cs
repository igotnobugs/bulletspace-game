using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalVars
{
    public class GlobVars : MonoBehaviour
    {
        public static Vector2 BottomLeftBorder = new Vector2(-4.5f, -4.5f);
        public static Vector2 UpperRightBorder = new Vector2(4.5f, 4.5f);
        public static int ScoreCounter;
        public static bool PlayerState;

        //Control Vars

        public static KeyCode kShoot = KeyCode.Space;
        public static KeyCode kLeft = KeyCode.A;
        public static KeyCode kRight = KeyCode.D;
        public static KeyCode kUp = KeyCode.W;
        public static KeyCode kDown = KeyCode.S;
        public static KeyCode kShield = KeyCode.E;
        public static KeyCode kSuicide = KeyCode.F;
        public static KeyCode kBrake = KeyCode.LeftShift;
        public static KeyCode kQuit = KeyCode.Escape;
    }
}
