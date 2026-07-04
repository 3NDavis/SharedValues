using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Primatives/Int", fileName = "SharedEvt_Int_Name")]
    public class SharedIntEvent : SharedEvent<int>
    {
        
    }


    [Serializable]
    public class SharedIntEventReference : SharedEventReference<int>
    {
        
    }
}