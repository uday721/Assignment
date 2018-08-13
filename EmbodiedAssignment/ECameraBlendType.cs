using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbodiedAssignment
{
    class ECameraBlendType
    {
        protected string blendStateName;
        protected ECameraBlendType(string name)
        {
            blendStateName = name;
        }

        public static ECameraBlendType EBlendType_1 = new ECameraBlendType("EBlendType_1");
        public static ECameraBlendType EBlendType_2 = new ECameraBlendType("EBlendType_2");
        public static ECameraBlendType EBlendType_3 = new ECameraBlendType("EBlendType_3");
        public static ECameraBlendType EBlendType_4 = new ECameraBlendType("EBlendType_4");
        public static ECameraBlendType EBlendType_5 = new ECameraBlendType("EBlendType_5");
        public static ECameraBlendType EBlendType_6 = new ECameraBlendType("EBlendType_6");

        public override string ToString()
        {
            return blendStateName;
        }

        public static implicit operator string (ECameraBlendType @enum)
        {
            return @enum.blendStateName;
        }
    }
}
