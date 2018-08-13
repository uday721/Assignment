using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace EmbodiedAssignment
{
    class Camera : ICamera
    {
        private float blendTimeLeft;
        private float blendTimeLeftVT;

        private MathLib.ViewTarget currentVT;
        private MathLib.ViewTarget desiredVT;

        public FPendingCameraMode pendingCameraMode;
        public FPendingViewTarget pendingViewTarget;
        public CameraParameters cameraParameters;

        public ECameraMode currentCameraMode = ECameraMode.FirstPersonMode;
        
        public struct CameraParameters
        {
            public float desiredFOV;
            //First person parameters
            public MathLib.Vector firstPersonEyesPositionOffset;
            //Third person simple parameters
            public MathLib.Vector camOffset;
            public float camDistance;
            //Director camera parameters
            public MathLib.Vector camPosition;
            public MathLib.Vector lookAtTarget; 
        };

        public struct FPendingCameraMode
        {
            public bool bPendingActive;
            public ECameraMode cameraMode;
            public CameraParameters parameters;
            public Blender.BlendParameters blendParameters;
        };

        public struct FPendingViewTarget
        {
            public bool bPendingActive;
            public MathLib.Vector targetPosition;
            public MathLib.Orientation targetOrientation;
            public Blender.BlendParameters blendParameters;
        };

        private void SetCameraMode(ref FPendingCameraMode newCameraMode)
        {
            pendingCameraMode = newCameraMode;
            pendingCameraMode.bPendingActive = true;

            currentCameraMode = pendingCameraMode.cameraMode;
            cameraParameters = newCameraMode.parameters;
            blendTimeLeft = pendingCameraMode.blendParameters.blendTime;
        }

        private void SetViewTarget(ref FPendingViewTarget newViewTarget)
        {
            pendingViewTarget = newViewTarget;
            pendingViewTarget.bPendingActive = true;
        }

        private void DesiredVT(ECameraMode currentCameraMode)
        {
            if (currentCameraMode == ECameraMode.FirstPersonMode)
            {
                desiredVT.POV.position = MathLib.AddVectors(currentVT.targetPosition, MathLib.VectorOrientationTransform(cameraParameters.firstPersonEyesPositionOffset, currentVT.targetOrientation));
                desiredVT.POV.orientation = currentVT.targetOrientation;
            }
            if (currentCameraMode == ECameraMode.ThirdPersonSimple)
            {
                desiredVT.POV.orientation = currentVT.targetOrientation;
                desiredVT.POV.position = MathLib.AddVectors(currentVT.targetPosition, MathLib.VectorOrientationTransform(cameraParameters.camOffset, currentVT.targetOrientation));
                desiredVT.POV.position = MathLib.SubtractVectors(desiredVT.POV.position, MathLib.VectorUniformScale(MathLib.OrientationToVector(currentVT.targetOrientation), cameraParameters.camDistance));
            }
            if (currentCameraMode == ECameraMode.DirectorCam)
            {
                float alpha = 1;
                desiredVT.POV.orientation = MathLib.OrientationLerp(currentVT.targetOrientation, MathLib.VectorToOrientation(cameraParameters.lookAtTarget), alpha);
                desiredVT.POV.position = MathLib.AddVectors(currentVT.targetPosition, MathLib.VectorOrientationTransform(cameraParameters.camPosition, currentVT.targetOrientation));
            }
        }

        private float PCT(ECameraBlendType blendType, float alpha, IBlender blender)
        {
            float pct = 0.0f;

            if (blendType == ECameraBlendType.EBlendType_1)
            {
                pct = 1.0f;
                blendTimeLeftVT = 0.0f;
            }
            if (blendType == ECameraBlendType.EBlendType_2)
            {
                pct = MathLib.Lerp(0.0f, 1.0f, alpha);
            }
            if (blendType == ECameraBlendType.EBlendType_3)
            {
                pct = blender.Blend2(0.0f, 1.0f, alpha, pendingViewTarget.blendParameters.blendExponent);
            }
            if (blendType == ECameraBlendType.EBlendType_4)
            {
                pct = blender.Blend3(0.0f, 1.0f, alpha, pendingViewTarget.blendParameters.blendExponent);
            }
            if (blendType == ECameraBlendType.EBlendType_5)
            {
                pct = blender.Blend23(0.0f, 1.0f, alpha, pendingViewTarget.blendParameters.blendExponent);
            }
            return pct;
        }

        private void UpdateCamera(float deltaTime)
        {
            Blender blender = new Blender();
            if (pendingViewTarget.bPendingActive)
            {
                bool bIsBlendDone = false;

                if (pendingViewTarget.blendParameters.blendType == ECameraBlendType.EBlendType_6)
                {
                    bool bTargetPositionDone = blender.Blend5(currentVT.targetPosition, pendingViewTarget.targetPosition, pendingViewTarget.blendParameters.springParameters, deltaTime, out currentVT.targetPosition);
                    bool bTargetOrientDone = blender.Blend6(currentVT.targetOrientation, pendingViewTarget.targetOrientation, pendingViewTarget.blendParameters.springParameters, deltaTime, out currentVT.targetOrientation);
                    if (bTargetPositionDone && bTargetOrientDone)
                    {
                        bIsBlendDone = true;
                    }
                }
                else
                {
                    blendTimeLeft -= deltaTime;
                    if (blendTimeLeft > 0.0f)
                    {
                        float duration = (pendingViewTarget.blendParameters.blendTime - blendTimeLeftVT) / pendingViewTarget.blendParameters.blendTime;
                        float pct = PCT(pendingViewTarget.blendParameters.blendType, duration, blender);

                        currentVT.targetPosition.x = MathLib.Lerp(currentVT.targetPosition.x, pendingViewTarget.targetPosition.x, pct);
                        currentVT.targetPosition.y = MathLib.Lerp(currentVT.targetPosition.y, pendingViewTarget.targetPosition.y, pct);
                        currentVT.targetPosition.z = MathLib.Lerp(currentVT.targetPosition.z, pendingViewTarget.targetPosition.z, pct);
                        currentVT.targetOrientation = MathLib.OrientationLerp(currentVT.targetOrientation, pendingViewTarget.targetOrientation, pct);
                    }
                    else
                    {
                        bIsBlendDone = true;
                    }
                }
                desiredVT.POV.FOV = cameraParameters.desiredFOV;

                DesiredVT(currentCameraMode);

                if (pendingCameraMode.bPendingActive)
                {
                    if (pendingCameraMode.blendParameters.blendType == ECameraBlendType.EBlendType_6)
                    {
                        bool bDesiredPositionDone = blender.Blend5(currentVT.POV.position, desiredVT.POV.position, pendingCameraMode.blendParameters.springParameters, deltaTime, out desiredVT.POV.position);
                        bool bDesiredOrientationDone = blender.Blend6(currentVT.POV.orientation, desiredVT.POV.orientation, pendingCameraMode.blendParameters.springParameters, deltaTime, out desiredVT.POV.orientation);
                        bool bDesiredFovDone = blender.Blend4(currentVT.POV.FOV, desiredVT.POV.FOV, pendingCameraMode.blendParameters.springParameters, deltaTime, out desiredVT.POV.FOV);

                        if (bDesiredPositionDone && bDesiredOrientationDone && bDesiredFovDone)
                        {
                            pendingCameraMode.bPendingActive = false;
                        }
                    }
                }
                else
                {
                    blendTimeLeft -= deltaTime;

                    if (blendTimeLeft > 0.0f)
                    {
                        float duration = (pendingCameraMode.blendParameters.blendTime - blendTimeLeft) / pendingCameraMode.blendParameters.blendTime;
                        float pct = PCT(pendingViewTarget.blendParameters.blendType, duration, blender);
                    }
                    else
                    {
                        pendingCameraMode.bPendingActive = false;
                    }
                }
            }
            currentVT = desiredVT;
        }

        public void TestCamera()
        {       
            FPendingViewTarget newViewTarget = new FPendingViewTarget();
            newViewTarget.targetPosition.x = 1000.0f;
            newViewTarget.targetPosition.y = 2000.0f;
            newViewTarget.targetPosition.z = 3000.0f;
            newViewTarget.targetOrientation.pitch = 0.0f;
            newViewTarget.targetOrientation.yaw = 0.0f;
            newViewTarget.targetOrientation.roll = 0.0f;
            newViewTarget.blendParameters.blendType = ECameraBlendType.EBlendType_3;
            newViewTarget.blendParameters.blendTime = 1.0f;
            SetViewTarget(ref newViewTarget);

            FPendingCameraMode newCameraMode = new FPendingCameraMode();
            newCameraMode.cameraMode = ECameraMode.ThirdPersonSimple;
            newCameraMode.parameters.camDistance = 2000.0f;
            newCameraMode.parameters.camOffset.x = 0.0f;
            newCameraMode.parameters.camOffset.y = 20.0f;
            newCameraMode.parameters.camOffset.z = 70.0f;
            newCameraMode.parameters.desiredFOV = 75.0f;
            newCameraMode.blendParameters.blendType = ECameraBlendType.EBlendType_2;
            newCameraMode.blendParameters.blendTime = 1.0f;
            SetCameraMode(ref newCameraMode);

            TimeSpan timeDelta = TimeSpan.FromSeconds(1.0f / 60.0f);
            while (true)
            {                
                UpdateCamera(timeDelta.Seconds);
                Thread.Sleep(timeDelta.Milliseconds);                
            }
        }           
    }
}
