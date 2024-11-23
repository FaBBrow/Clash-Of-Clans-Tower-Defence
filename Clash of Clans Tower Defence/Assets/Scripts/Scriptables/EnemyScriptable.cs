using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "enemy",menuName = "Character/Enemy")]
public class EnemyScriptable :ScriptableObject
{
  [SerializeField]private float health;
  [SerializeField] private float speed;
  [SerializeField] private int coinOnDeath;

  public float Health
  {
    get => health;
    set => health = value;
  }

  public float Speed
  {
    get => speed;
    set => speed = value;
  }

  public int CoinOnDeath
  {
    get => coinOnDeath;
    set => coinOnDeath = value;
  }
}
