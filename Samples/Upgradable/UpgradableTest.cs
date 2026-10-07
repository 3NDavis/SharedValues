using UnityEngine;
using SharedValues.Upgradable;

namespace SharedValues.Samples
{
    public class UpgradableTest : MonoBehaviour
    {
        [SerializeField] private UpgradableSharedFloat upgradeFloat;
        [SerializeField] private UpgradableSharedInt upgradeInt;
        [SerializeField] private UpgradableSharedVector upgradeVector2;
    }
}
