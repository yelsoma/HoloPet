using UnityEngine;
using System;
public class AFKManager : MonoBehaviour
{
    private int worldLevel;
    private int homeLevel;
    private int coin;

    public int GetWorldLevel => worldLevel;
    public int GetHomeLevel => homeLevel;
    public int GetCoin => coin;
    public event EventHandler OnCoinGain;
    public event EventHandler OnLevelUp;
    public event EventHandler OnWorldChange;

    [SerializeField] private float coinInterval = 5f; // seconds per coin tick
    private float timer;

    public void SetData(int world, int home, int coin)
    {
        worldLevel = world;
        homeLevel = home;
        this.coin = coin;
        OnCoinGain?.Invoke(this, EventArgs.Empty);
        OnLevelUp?.Invoke(this, EventArgs.Empty);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= coinInterval)
        {
            timer -= coinInterval;
            AddAfkCoinTick();
        }
    }

    private void AddAfkCoinTick()
    {
        int gain = GetAfkCoinGain();
        AddCoin(gain);
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        OnCoinGain?.Invoke(this, EventArgs.Empty);
    }

    public bool TryLevelUpHome()
    {
        int cost = GetHomeLevelCost(homeLevel);

        if (coin < cost)
            return false;

        coin -= cost;
        homeLevel++;
        OnLevelUp?.Invoke(this, EventArgs.Empty);

        return true;
    }

    public void IncreaseWorldLevel()
    {
        worldLevel++;
        OnWorldChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetHomeLevelCost(int homeLevel)
    {
        float maxTime = 600f;      // 10 minutes cap
        float softness = 8f;       // how fast it reaches the cap

        float timeSeconds =
            maxTime * (1f - Mathf.Exp(-homeLevel / softness));

        return Mathf.RoundToInt(timeSeconds * homeLevel);
    }

    public int GetAfkCoinGain()
    {
        return worldLevel;
    }
}
