using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;


public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;
    public GameObject selectedBuildArea;
    public Action onclikBuildArea;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    public bool onButton;
    

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))

            if (Physics.Raycast(ray, out raycastHit))
            {
               
                if (raycastHit.transform.CompareTag("buildarea"))
                {
                    if (raycastHit.transform.childCount == 2&& selectedBuildArea!=raycastHit.transform.gameObject)
                    {
                        if (selectedBuildArea!=null)
                        {
                            selectedBuildArea.GetComponent<IBuildArea>().disappearUI();
                        }
                        selectedBuildArea = raycastHit.transform.gameObject;
                        onclikBuildArea?.Invoke();
                        changeCamera(selectedBuildArea.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>());
                        selectedBuildArea.GetComponent<IBuildArea>().showUI();
                    }
                    else if (selectedBuildArea==raycastHit.transform.gameObject)
                    {
                        changeCamera(cameras[0]);
                        selectedBuildArea.GetComponent<IBuildArea>().disappearUI();
                        selectedBuildArea = null;
                    }

                



                }
                else if (raycastHit.transform.CompareTag("Button"))
                {
                   
                    raycastHit.transform.GetComponent<IBuilder>().Build();
                    if (selectedBuildArea.transform.childCount!=2)
                    {
                        selectedBuildArea.GetComponent<IBuildArea>().disappearUI();
                    }
                    
                    
                }

                else
                {
                    if (selectedBuildArea!=null)
                    { selectedBuildArea.GetComponent<IBuildArea>().disappearUI();
                        
                    }
                   
                    selectedBuildArea = null;
                    changeCamera(cameras[0]);
                }
               
              
              
             


            }

    }

    public void changeCamera(CinemachineVirtualCamera camera)
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }

        camera.Priority = 1;
    }

}