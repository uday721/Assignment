using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbodiedAssignment
{
    class Blender : IBlender
    {     
        //Blend ease-in smoothly approaching b.
        public float Blend2(float a, float b, float alpha, float exp)
        {
            float result = MathLib.BezierCurveEaseIn(a,b,alpha,exp);
            return result;
        }
        
        //Blend ease-out departing from a.
        public float Blend3(float a, float b, float alpha, float exp)
        {
            float result = MathLib.BezierCurveEaseOut(a, b, alpha, exp);
            return result;
        }

        // Blend ease-out departing from a and ease-in approaching b.
        public float Blend23(float a, float b, float alpha, float exp)
        {
            float result = MathLib.BezierCurveEaseInOut(a,b,alpha,exp);
            return result;
        }

        //
        // Blends A to B using spring damping
        // A			Value to blend from.
        // B			Value to blend to.
        // Params		Spring parameters
        // DeltaTime	Elapsed time
        // OutResult	The resulting blend value.
        //
        // @returns true if the blend is done by checking if A is equals (or close) to B
        // 
        public bool Blend4(float a, float b, MathLib.SpringParams parameters, float deltaTime, out float outResult)
        {
            //NOTE: It is *NOT* necessary to implement this functionality.
            //		It only exist for the purpose of this exercise. 
            //      You may OPTIONALLY implement it at your discretion,
            //      if you're knowledgeable about the subject.
            //		However, your refactored code should support the data
            //		and interface for spring damping blend.

            outResult = 0.0f;

            return false;
        }

        //
        // Blends A to B using spring damping
        // A			Value to blend from.
        // B			Value to blend to.
        // Params		Spring parameters
        // DeltaTime	Elapsed time
        // OutResult	The resulting blend value.
        //
        // @returns true if the blend is done by checking if A is equals (or close) to B
        // 
        public bool Blend5(MathLib.Vector a, MathLib.Vector b, MathLib.SpringParams parameters, float deltaTime, out MathLib.Vector outResult)
        {
            //NOTE: It is *NOT* necessary to implement this functionality.
            //		It only exist for the purpose of this exercise. 
            //      You may OPTIONALLY implement it at your discretion,
            //      if you're knowledgeable about the subject.
            //		However, your refactored code should support the data
            //		and interface for spring damping blend.

            outResult = new MathLib.Vector();

            return false;
        }

        //
        // Blends A to B using spring damping
        // A			Value to blend from.
        // B			Value to blend to.
        // Params		Spring parameters
        // DeltaTime	Elapsed time
        // OutResult	The resulting blend value.
        //
        // @returns true if the blend is done by checking if A is equals (or close) to B
        // 
        public bool Blend6(MathLib.Orientation a, MathLib.Orientation b, MathLib.SpringParams parameters, float deltaTime, out MathLib.Orientation outResult)
        {
            //NOTE: It is *NOT* necessary to implement this functionality.
            //		It only exist for the purpose of this exercise. 
            //      You may OPTIONALLY implement it at your discretion,
            //      if you're knowledgeable about the subject.
            //		However, your refactored code should support the data
            //		and interface for spring damping blend.

            outResult = new MathLib.Orientation();

            return false;
        }

        public struct BlendParameters
        {
            public ECameraBlendType blendType;
            public MathLib.SpringParams springParameters;
            public float blendTime;
            public float blendExponent;
        }

        MathLib.TPOV BlendPOVs(MathLib.TPOV a, MathLib.TPOV b, float alpha)
        {
            MathLib.TPOV result;

            result.position.x = MathLib.Lerp(a.position.x, b.position.x, alpha);
            result.position.y = MathLib.Lerp(a.position.y, b.position.y, alpha);
            result.position.z = MathLib.Lerp(a.position.z, b.position.z, alpha);

            result.orientation = MathLib.OrientationLerp(a.orientation, b.orientation, alpha);
            result.FOV = MathLib.Lerp(a.FOV, b.FOV, alpha);

            return result;
        }
    }
}
