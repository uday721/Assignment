namespace EmbodiedAssignment
{
    interface IBlender
    {
        float Blend2(float a, float b, float alpha, float exp);
        float Blend23(float a, float b, float alpha, float exp);
        float Blend3(float a, float b, float alpha, float exp);
        bool Blend4(float a, float b, MathLib.SpringParams parameters, float deltaTime, out float outResult);
        bool Blend5(MathLib.Vector a, MathLib.Vector b, MathLib.SpringParams parameters, float deltaTime, out MathLib.Vector outResult);
        bool Blend6(MathLib.Orientation a, MathLib.Orientation b, MathLib.SpringParams parameters, float deltaTime, out MathLib.Orientation outResult);
    }
}