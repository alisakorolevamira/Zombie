using UnityEngine;

public class ZombiesRewardInteractor : Interactor
{
    private int _reward = 10;
    private readonly int _coefficientOfChangingReward = 2;
    public int Reward { get { return _reward; } }

    public void DoubleReward()
    {
        _reward *= _coefficientOfChangingReward;
    }

    public void GiveRewardToPlayer()
    {
        var playersMoneyInteractor = Game.GetInteractor<PlayersMoneyInteractor>();
        playersMoneyInteractor.AddMoney(_reward);
    }
}
