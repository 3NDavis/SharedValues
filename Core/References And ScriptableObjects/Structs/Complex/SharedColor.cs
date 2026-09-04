using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Complex/Color", fileName = "SharedVal_Color_Name")]
    public class SharedColor : SharedStruct<Color>
    {
        protected override string GetTextureName()
        {
            return "Color";
        }
    }

    [Serializable]
    public class SharedColorReference : SharedValueReference<Color>
    {

    }
}