namespace Scripts.Progress
{
    public class ProgressData
    {
        public int PlayerMoney = 0;
        public int PlayerLevel = 2;
        public int PlayerScore = 0;
        public int ZombieHealth = 500;
        public int ZombieMoneyReward = 10;
        public int ZombieScoreReward = 100;
        public int AddSitizenCardPrice = 10;
        public int MergeCardPrice = 50;
        public int AddSpeedCardPrice = 40;
        public int DoubleRewardCardPrice = 100;

        public void Update(PlayerProgress playerProgress, ZombieProgress zombieProgress, CardsPricesProgress cardsPricesProgress)
        {
            PlayerMoney = playerProgress.Money;
            PlayerLevel = playerProgress.Level;
            PlayerScore = playerProgress.Score;
            ZombieHealth = zombieProgress.Health;
            ZombieMoneyReward = zombieProgress.MoneyReward;
            ZombieScoreReward = zombieProgress.ScoreReward;
            AddSitizenCardPrice = cardsPricesProgress.AddSitizenCardPrice;
            MergeCardPrice = cardsPricesProgress.MergeCardPrice;
            AddSpeedCardPrice = cardsPricesProgress.AddSpeedCardPrice;
            DoubleRewardCardPrice = cardsPricesProgress.DoubleRewardCardPrice;
        }
    }
}