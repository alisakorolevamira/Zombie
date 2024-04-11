namespace Architecture.ServicesInterfaces.UI
{
    public interface IStarCountService : IService
    {
        int CountStars(int meduimScore, int highScore);
    }
}