namespace Scripts.Progress
{
    public class PlayerProgress
    {
        public int Money;
        public string Level;
        public int Score;

        public PlayerProgress(string level, int money, int score)
        {
            Level = level;
            Money = money;
            Score = score;
        }
    }
}
