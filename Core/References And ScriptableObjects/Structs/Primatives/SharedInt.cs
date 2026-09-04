using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Primatives/Int", fileName = "SharedVal_Int_Name")]
    public class SharedInt : SharedStruct<int>
    {
        protected override string GetTextureName()
        {
            return "Int";
        }
    }

    [Serializable]
    public class SharedIntReference : SharedValueReference<int>
    {

    }
}