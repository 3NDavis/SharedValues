using System;
using UnityEngine;

namespace SharedValues
{
    [CreateAssetMenu(menuName = "Shared Values/Variables/Complex/Quaternion", fileName = "SharedVal_Qtrn_Name")]
    public class SharedQuaternion : SharedStruct<Quaternion>
    {

    }

    [Serializable]
    public class SharedQuaternionReference : SharedValueReference<Quaternion>
    {

    }
}