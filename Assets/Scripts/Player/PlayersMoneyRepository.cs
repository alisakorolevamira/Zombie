using UnityEngine;

public class PlayersMoneyRepository : Repository
{
    public int Money;
    private const string _key = "PlayersMoney";
    private readonly int _defaultValue = 0;

    public override void Initialize()
    {
        Money = PlayerPrefs.GetInt( _key, _defaultValue);
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(_key, Money);
        PlayerPrefs.Save();
    }

    public override void Reset()
    {
        PlayerPrefs.DeleteKey(_key);
    }
}
