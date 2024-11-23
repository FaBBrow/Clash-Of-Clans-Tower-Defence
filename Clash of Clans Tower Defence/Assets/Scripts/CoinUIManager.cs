using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager instance;
  [SerializeField] public GameObject coinUI;

  [SerializeField]
  private TextMeshProUGUI coinutext;

  [SerializeField] private TextMeshProUGUI coinUIAnimText;

  private void Start()
  {
      instance = this;
      coinutext.text = GameManager.instance.Coin.ToString("0");
     
  }

  public void changeUI(int value)
  {
      coinutext.text = GameManager.instance.Coin.ToString("0");
      StartCoroutine(coinaddedanim(value));
      coinUI.transform.DOShakeScale(1f, 1f, 10);
  }

  public IEnumerator coinaddedanim(int value)
  {
      if (value>0)
      {
          coinUIAnimText.gameObject.SetActive(true);
          coinUIAnimText.text = value.ToString("+0");
      }
      else
      {
          coinUIAnimText.gameObject.SetActive(true);
          coinUIAnimText.text = value.ToString("-0");
      }

      yield return new WaitForSeconds(2f);
      coinUIAnimText.gameObject.SetActive(false);
      coinUIAnimText.text = null;
  }
  
}
