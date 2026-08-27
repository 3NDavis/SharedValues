using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Complex/Vector2", fileName = "SharedEvt_V2_Name")]
    public class SharedVector2Event : SharedEvent<Vector2>
    {
        
    }


    [Serializable]
    public class SharedVector2EventReference : SharedEventReference<Vector2>
    {
        
    }
}