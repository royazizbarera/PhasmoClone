using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SpaceGameStop : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
#if UNITY_EDITOR 
            EditorApplication.isPaused = true;
#endif
        }
    }
}
