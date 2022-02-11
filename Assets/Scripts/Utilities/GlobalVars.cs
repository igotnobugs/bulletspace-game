using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalVars
{
    public class GlobVars : MonoBehaviour
    {
        public static int ScoreCounter;
        public static bool PlayerState;

        //Control Vars

        public static KeyCode kShield = KeyCode.E;
        public static KeyCode kSuicide = KeyCode.F;
        public static KeyCode kBrake = KeyCode.LeftShift;
        public static KeyCode kQuit = KeyCode.Escape;
    }
}
