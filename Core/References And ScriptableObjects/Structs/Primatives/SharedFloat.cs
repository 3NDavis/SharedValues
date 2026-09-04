using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Primatives/Float", fileName = "SharedVal_Float_Name")]
    public class SharedFloat : SharedStruct<float>
    {
        protected override string GetTextureName()
        {
            return "Float";
        }
    }

    [Serializable]
    public class SharedFloatReference : SharedValueReference<float>
    {

    }
}