namespace Scripts.Architecture.Services
{
    public interface IStarCountService : IService
    {
        int CountStars(int meduimScore, int highScore);
    }
}