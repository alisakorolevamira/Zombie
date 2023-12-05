using UnityEngine;
using UnityEngine.Events;

public class PlayersMoneyInteractor : Interactor
{
    public event UnityAction MoneyChanged;

    public int Money()
    {
        var playersMoneyRepository = Game.GetRepository<PlayersMoneyRepository>();
        return playersMoneyRepository.Money;
    }

    public bool IsEnoughMoney(int value)
    {
        var playersMoneyRepository = Game.GetRepository<PlayersMoneyRepository>();
        return playersMoneyRepository.Money >= value;
    }

    public void AddMoney(int value)
    {
        var playersMoneyRepository = Game.GetRepository<PlayersMoneyRepository>();

        playersMoneyRepository.Money += value;
        MoneyChanged?.Invoke();
        playersMoneyRepository.Save();
    }

    public void SpendMoney(int value)
    {
        var playersMoneyRepository = Game.GetRepository<PlayersMoneyRepository>();

        if (playersMoneyRepository.Money >= value)
        {
            playersMoneyRepository.Money -= value;
            MoneyChanged?.Invoke();
            playersMoneyRepository.Save();
        }
    }
}
