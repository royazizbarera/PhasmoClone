using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private bool isMainCamera = true;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject boardCamera;
    public void ChangeCamera()
    {
        if (isMainCamera)
        {
            mainCamera.SetActive(false);
            boardCamera.SetActive(true);
            isMainCamera = false;
        }
        else
        {
            mainCamera.SetActive(true);
            boardCamera.SetActive(false);
            isMainCamera = true;
        }
    }

}
