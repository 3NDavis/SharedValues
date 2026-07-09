using System.Collections.Generic;
using SharedValues.Attributes;
using UnityEngine;

namespace SharedValues.Upgradable
{
    /// <summary>
    /// A shared value that has modifiers applied to it
    /// </summary>
    public abstract class UpgradableSharedValue<T,M,V>
    where T : struct where M : ValueModifier where V : ValueModifications<T,M>
    {
        [SerializeField] private T baseValue;
        [Tooltip("<b>Playmode Only!</b> The value that the shared value reference is using, only updates when accessed.")]
        [ReadOnly]
        [SerializeField] private T postModificationValue;

        [SerializeField] V modifiers;

        public T GetValue(Dictionary<ValueModifier, float> modificationsDomains)
        {
            postModificationValue = modifiers.ApplyModifications(baseValue, modificationsDomains);
            return postModificationValue;
        }
    }
}