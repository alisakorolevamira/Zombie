namespace Scripts.Progress
{
    public class PlayerProgress
    {
        public int Money;
        public int Level;
        public int Score;

        public PlayerProgress(int level, int money, int score)
        {
            Level = level;
            Money = money;
            Score = score;
        }
    }
}
