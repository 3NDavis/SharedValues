using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Primatives/Bool", fileName = "SharedVal_Bool_Name")]
    public class SharedBool : SharedStruct<bool>
    {

    }

    [Serializable]
    public class SharedBoolReference : SharedValueReference<bool>
    {

    }
}