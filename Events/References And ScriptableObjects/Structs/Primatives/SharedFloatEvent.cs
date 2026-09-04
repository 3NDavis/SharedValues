using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Primatives/Float", fileName = "SharedEvt_Float_Name")]
    public class SharedFloatEvent : SharedEvent<float>
    {
        protected override string GetTextureName()
        {
            return "Float";
        }
    }

    [Serializable]
    public class SharedFloatEventReference : SharedEventReference<float>
    {
        
    }
}