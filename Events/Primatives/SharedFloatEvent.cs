using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Primatives/Float", fileName = "SharedEvt_Float_Name")]
    public class SharedFloatEvent : SharedEvent<float>
    {
        
    }

    [Serializable]
    public class SharedFloatEventReference : SharedEventReference<float>
    {
        
    }
}