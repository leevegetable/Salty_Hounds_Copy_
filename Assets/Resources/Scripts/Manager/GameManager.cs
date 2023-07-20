using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public SystemOption SystemOptions = new SystemOption();
    public GameSetting Setting = new GameSetting();
    private void Awake()
    {
        instance = this;
    }
}
