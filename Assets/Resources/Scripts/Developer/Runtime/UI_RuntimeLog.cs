using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_RuntimeLog : MonoBehaviour
{
    public static UI_RuntimeLog instance;
    public TMP_Text Log;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public void CallLog(string text)
    {
        if (Log.text.Split('\n').Length > 15)
        {
            Log.text = "";
        }
        Log.text += '\n';
        Log.text += text;
    }
    // Update is called once per frame
    
}
