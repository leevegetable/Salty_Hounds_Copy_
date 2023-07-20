using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;
    public bool isControl = true;
    public bool isSelect = false;
    public bool isEscape = false;
    public int dirX = 0;
    public float getVerticalDir = 0;
    public float getHorizontalDir = 0;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isControl)
        {
#if UNITY_STANDALONE_WIN
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isSelect = true;
            }
            else
            {
                isSelect = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isEscape = true;
            }
            else
            {
                isEscape = false;
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                dirX = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dirX = -1;
            }
            else
            { 
                dirX = 0;
            }

            //여기 입력 속도가 너무 빠름
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                getVerticalDir = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                getVerticalDir = -1;
            }
            else
            {
                getVerticalDir = 0;
            }
#endif
            getHorizontalDir = Input.GetAxis("Horizontal");
        }
    }
}
