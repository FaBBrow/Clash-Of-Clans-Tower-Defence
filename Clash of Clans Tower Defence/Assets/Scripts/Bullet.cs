using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private float _damage;

  public float Damage
  {
    get => _damage;
    set => _damage = value;
  }


  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Enemy"))
    {
      
      other.GetComponent<IDamageable>().takedamage(Damage);
      gameObject.SetActive(false);
    }
  }
}
