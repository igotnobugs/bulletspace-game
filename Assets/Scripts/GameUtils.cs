using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtils
{
    public class GameUtils : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public static float Constrain(float val, float min, float max)
        {
            if (val <= min) return min;
            else if (val >= max) return max;
            else return val;
        }
    }
}
