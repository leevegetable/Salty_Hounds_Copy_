using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public Dictionary<int,MapData> Maps = new Dictionary<int,MapData>();
    public int CurrentActiveMap;
    private void Awake()
    {
        instance = this;
        Maps = new Dictionary<int, MapData>();
    }
}
