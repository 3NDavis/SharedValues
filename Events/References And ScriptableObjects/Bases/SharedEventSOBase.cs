using System;
using UnityEngine;

namespace SharedValues.Events
{
    public abstract class SharedEventSOBase : SharedSOBase
    {
        protected override string GetFilePath()
        {
            return k_sharedValueFilePath + "Events\\";
        }
    }
}