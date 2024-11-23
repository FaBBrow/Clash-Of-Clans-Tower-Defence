using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WizardBuilder : MonoBehaviour,IBuilder
{
    [SerializeField] private int Price;
    [SerializeField] private GameObject WizarPrefab;
    [SerializeField] private Vector3 ofsetXYZ;
    [SerializeField] private Image colorimage;
    [SerializeField] private TextMeshProUGUI textmexbuyprice;

    private void Start()
    {
        textmexbuyprice.text = Price.ToString();
    }

    public void Build()
    {
        if (GameManager.instance.Coin>=Price)
        {
            GameManager.instance.Coin -= Price;
            CoinUIManager.instance.changeUI(-Price);
            GameObject build = Instantiate(WizarPrefab, SelectionManager.instance.selectedBuildArea.transform.position,
                Quaternion.identity);
            build.transform.SetParent(SelectionManager.instance.selectedBuildArea.transform);
            build.transform.localPosition = ofsetXYZ;
            ChangeColorMoney();
        }
      
    }

    public void ChangeColorMoney()
    {
        if (GameManager.instance.Coin>=Price)
        {
            colorimage.DOColor(Color.white, 0);
        }
        else
        {
            colorimage.DOColor(Color.black, 0);
        }
    }
}
