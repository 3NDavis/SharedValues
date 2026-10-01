using UnityEngine;
using SharedValues.Upgradable;

namespace SharedValues.Upgradable.VisualTreeAsset
{
    public class UpgradableTest : MonoBehaviour
    {
        [SerializeField] private UpgradableSharedFloat upgradeFloat;
        [SerializeField] private UpgradableSharedInt upgradeInt;
        [SerializeField] private UpgradableSharedVector upgradeVector2;
    }
}
