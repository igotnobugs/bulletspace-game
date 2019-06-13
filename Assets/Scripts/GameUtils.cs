using UnityEngine;

namespace GameUtils
{
    public class Util : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static Vector2 Vec2(float x, float y)
        {
            return new Vector2(x, y);
        }

        public static Vector3 Vec3(float x,float y, float z)
        {
            return new Vector3(x, y, z);
        }

        public static float GetLength(Vector2 Vec2)
        {
            float length = Mathf.Sqrt((Vec2.x* Vec2.x) + (Vec2.y * Vec2.y));
            return length;
        }

        public static float GetLength(Vector3 Vector)
        {
            float length = Mathf.Sqrt((Vector.x * Vector.x) + (Vector.y * Vector.y) + (Vector.z * Vector.z));
            return length;
        }

        public static float Constrain(float val, float min, float max)
        {
            if (val <= min) return min;
            else if (val >= max) return max;
            else return val;
        }
    }
}
