namespace Architecture.ServicesInterfaces.UI
{
    public interface IStarCountService : IService
    {
        int CountStars(int mediumScore, int highScore);
    }
}