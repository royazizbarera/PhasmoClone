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
            EditorApplication.isPaused = true;
        }
    }
}
