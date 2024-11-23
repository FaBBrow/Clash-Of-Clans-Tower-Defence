using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] public GameObject Mainland;
    public static GameManager instance;
    [SerializeField] public GameObject arrowpool;
    [SerializeField] public GameObject orbsPool;
    [SerializeField] private int coin=25;
    private int health = 3;

    public int Coin
    {
        get => coin;
        set => coin = value;
    }

    public int Health
    {
        get => health;
        set => health = value;
    }

    public void takeCoin(int money)
    {
        coin += money;
        CoinUIManager.instance.changeUI(money);
       
    }

    public void takeDamage(int value)
    {
        health += value;
        HealthUIManager.instance.changeUı();
        
    }

    private void Awake()
    {
        instance = this;
    }
}
