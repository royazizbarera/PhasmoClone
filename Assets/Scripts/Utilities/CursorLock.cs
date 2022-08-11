using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class CursorLock : MonoBehaviour
    {
        private bool cursorLock;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLock = true;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (cursorLock)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    cursorLock = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    cursorLock = true;
                }
            }
        }
    }
}