namespace Systems.Upgrades
{
    public interface IUpgrade
    {
        public void LoadData(UpgradesData data);
        
        public void SetUpgradeData();
    }
}
