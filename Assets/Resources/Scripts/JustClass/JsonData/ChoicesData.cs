using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoicesData
{
    public int ID;
    public bool isQuit;
    public Choice[] Options;
    public ChoicesData()
    {
        ID = -1;
        isQuit = false;
        Options = null;
    }

    public ChoicesData(int id, bool quit, Choice[] options)
    {
        ID = id;
        isQuit = quit;
        Options = options;
    }
}
