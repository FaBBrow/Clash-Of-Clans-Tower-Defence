using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private List<Image> healths;
    public static HealthUIManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        changeUı();
    }


    public void changeUı()
    {
        for (int i = 0; i < healths.Count; i++)
        {
            if (i >= GameManager.instance.Health)
            {
                healths[i].DOFade(0, 1);
                healths[i].transform.parent.transform.DOShakeScale(1, 1);
            }
        }
    }

}
