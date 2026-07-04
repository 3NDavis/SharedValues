using UnityEngine;
using SharedValues.Enumerators;

namespace SharedValues.Upgradable
{
    [CreateAssetMenu(menuName = "Shared Values/Upgradable/ModsDomains", fileName = "SharedDictionary_ModDomains_Name")]
    public class SharedModsDomainsValue : SharedDictionary<ValueModifier, float>
    {

    }

    [System.Serializable]
    public class SharedModsDomainsReference : SharedDictionaryReference<ValueModifier, float>
    {
        
    }
}