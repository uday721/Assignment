using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbodiedAssignment
{
    class ECameraMode
    {
        protected string cameraMode;
        protected ECameraMode(string name)
        {
            cameraMode = name;            
        }

        public static ECameraMode FirstPersonMode = new ECameraMode("FirstPersonMode");
        public static ECameraMode ThirdPersonSimple = new ECameraMode("ThirdPersonSimple");
        public static ECameraMode DirectorCam = new ECameraMode("DirectorCam");

        public override string ToString()
        {
            return cameraMode;
        }

        public static implicit operator string (ECameraMode @enum)
        {
            return @enum.cameraMode;
        }

    }
}
