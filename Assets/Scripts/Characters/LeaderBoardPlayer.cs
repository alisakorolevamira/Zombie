namespace Scripts.Characters
{
    public class LeaderBoardPlayer
    {
        public LeaderBoardPlayer(int rank, string name, int score)
        {
            Rank = rank;
            Name = name;
            Score = score;
        }

        public int Rank { get; private set; }
        public string Name { get; private set; }
        public int Score { get; private set; }
    }
}
