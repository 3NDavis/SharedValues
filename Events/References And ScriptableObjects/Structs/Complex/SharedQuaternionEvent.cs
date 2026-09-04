using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Complex/Quaternion", fileName = "SharedEvt_Qtrn_Name")]
    public class SharedQuaternionEvent : SharedEvent<Quaternion>
    {
        protected override string GetTextureName()
        {
            return "Quat";
        }
    }


    [Serializable]
    public class SharedQuaternionEventReference : SharedEventReference<Quaternion>
    {
        
    }
}