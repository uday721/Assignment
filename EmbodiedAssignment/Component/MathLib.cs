using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbodiedAssignment
{
    class MathLib
    {
        public struct Vector
        {
            public float x, y, z;
        };

        public struct Orientation
        {
            public float pitch, yaw, roll;
        };       

        public struct TPOV
        {
            public Vector position;
            public Orientation orientation;
            public float FOV;
        };

        public struct ViewTarget
        {
            public TPOV POV;
            public Vector targetPosition;
            public Orientation targetOrientation;
        };

        public struct SpringParams
        {
            /** 
			 * Stiffness of the spring, how hard is it to extend.  
			 * The higher it is, the more fixed it will be. 
			 * A number between 1 and 20 is recommended.
			 */
            public float SpringK;

            /** 
			 * Damping is the spring internal friction, or how much it resists the "boinggggg" effect. 
			 * Too high and you'll lose it!  Shouldn't be much larger than SpringK.
			 * A number between 1 and 20 is recommended.
			 */
            public float DampK;

            /**
			 * Mass of whatever the spring is pulling, how hard is it to move.
			 * if over 120 and it'll be very heavy to move
			 */
            public float Mass;
        };


        //External fuction used for convenience of this assignments.
        public static extern Vector AddVectors(Vector a, Vector b);
        public static extern Vector SubtractVectors(Vector a, Vector b);
        public static extern Vector VectorUniformScale(Vector a, float scale);     
        public static extern Vector VectorOrientationTransform(Vector vec, Orientation inOrient);
        public static extern Vector OrientationToVector(Orientation inOrient);
        public static extern Orientation VectorToOrientation(Vector a);
        public static extern Orientation OrientationLerp(Orientation a, Orientation b, float alpha);
        public static extern float Lerp(float a, float b, float alpha);
        public static extern float BezierCurveEaseIn(float startPoint, float EndPoint, float alpha, float exp);
        public static extern float BezierCurveEaseOut(float startPoint, float EndPoint, float alpha, float exp);
        public static extern float BezierCurveEaseInOut(float startPoint, float EndPoint, float alpha, float exp);

    }
}
