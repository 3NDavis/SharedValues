using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Complex/Vector2", fileName = "SharedVal_V2_Name")]
    public class SharedVector2 : SharedStruct<Vector2>
    {
        protected override string GetTextureName()
        {
            return "V2";
        }
    }

    [Serializable]
    public class SharedVector2Reference : SharedValueReference<Vector2>
    {

    }
}