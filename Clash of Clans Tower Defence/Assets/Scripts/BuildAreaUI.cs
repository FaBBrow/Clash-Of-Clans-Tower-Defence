using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BuildAreaUI : MonoBehaviour,IBuildArea
{
    [SerializeField] private List<GameObject> UIimage; 
    public void showUI()
    {
        foreach (var VARIABLE in UIimage)
        {
            VARIABLE.SetActive(true);
            VARIABLE.transform.DOScale(new Vector3(1, 1, 1), 1);
            VARIABLE.GetComponent<IBuilder>().ChangeColorMoney();
        }
    }

    public void disappearUI()
    {
        foreach (var VARIABLE in UIimage)
        {
           
            
                VARIABLE.transform.DOScale(new Vector3(0, 0, 0), 1).OnComplete(() => VARIABLE.SetActive(false));
            


                
            
        }
    }
}
