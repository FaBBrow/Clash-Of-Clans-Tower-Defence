using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] private List<Waves> WavesList;
  private List<GameObject> currentList;
  public int listindex = 0;
  public int wavelistindex=0;

  private void Start()
  {
    startWave();
  }

  public void startWave()
  {
    currentList=  takeWave(listindex);
    spawnObject();


  }


  public List<GameObject> takeWave(int value )
  {

    listindex++;
    return WavesList[value].list;
    

  }

  IEnumerator spawntimer()
  {
    if (currentList.Count == wavelistindex)
    {
      StartCoroutine(nextList());
      yield break;
    }
    else
    {
      yield return new WaitForSeconds(3f);
      wavelistindex++;
      spawnObject();
    }
      
  }




  public void spawnObject()
  {
    Instantiate(currentList[wavelistindex],transform.position,quaternion.identity);
    StartCoroutine(spawntimer());
  }
  IEnumerator nextList()
  {

    yield return new WaitForSeconds(10f);
    listindex++;
    startWave();


  }
  
}
  
  

