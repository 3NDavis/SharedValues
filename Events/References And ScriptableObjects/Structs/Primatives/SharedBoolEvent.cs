using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Primatives/Bool", fileName = "SharedEvt_Bool_Name")]
    public class SharedBoolEvent : SharedEvent<bool>
    {
        protected override string GetTextureName()
        {
            return "Bool";
        }
    }


    [Serializable]
    public class SharedBoolEventReference : SharedEventReference<bool>
    {
        
    }
}