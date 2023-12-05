using UnityEngine;

public class ZombiesHealthRepository : Repository
{
    public readonly int MaximumHealth = 100;
    public int Health = 100;
    private const string _key = "ZombiesHealth";

    public override void Initialize()
    {
        var health = PlayerPrefs.GetInt(_key, MaximumHealth);

        if (health > 0)
        {
            Health = health;
        }
        
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(_key, Health);
        PlayerPrefs.Save();
    }

    public override void Reset()
    {
        PlayerPrefs.DeleteKey(_key);
    }
}
