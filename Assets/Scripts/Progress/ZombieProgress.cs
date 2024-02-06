namespace Scripts.Progress
{
    public class ZombieProgress
    {
        public int Health;
        public int MoneyReward;
        public int ScoreReward;

        public ZombieProgress(int health, int moneyReward, int scoreReward)
        {
            Health = health;
            MoneyReward = moneyReward;
            ScoreReward = scoreReward;
        }
    }
}
