namespace SharedValues.Upgradable
{
    public abstract class SharedUpgradableSOBase : SharedSOBase
    {
        protected override string GetFilePath()
        {
            return k_sharedValueFilePath + "Upgradable\\";
        }

        protected override string GetTextureName()
        {
            return "mqMark";
        }
    }
}