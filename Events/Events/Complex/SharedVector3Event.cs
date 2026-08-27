using System;
using UnityEngine;

namespace SharedValues.Events
{
    
    [CreateAssetMenu(menuName = "Shared Values/Events/Complex/Vector3", fileName = "SharedEvt_V3_Name")]
    public class SharedVector3Event : SharedEvent<Vector3>
    {
        
    }


    [Serializable]
    public class SharedVector3EventReference : SharedEventReference<Vector3>
    {
        
    }
}