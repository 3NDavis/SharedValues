using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Complex/Vector3", fileName = "SharedVal_V3_Name")]
    public class SharedVector3 : SharedStruct<Vector3>
    {
        protected override string GetTextureName()
        {
            return "V3";
        }
    }

    [Serializable]
    public class SharedVector3Reference : SharedValueReference<Vector3>
    {

    }
}