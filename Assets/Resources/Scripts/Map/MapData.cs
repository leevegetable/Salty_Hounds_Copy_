using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(InteractionContainer))]
public class MapData : MonoBehaviour
{
    public int MapCode;
    public int[] LinkedMap = new int[0];
    public MapLinkObject[] MapLinker = new MapLinkObject[0];
    public PolygonCollider2D CameraField;
    //public InteractionContainer interactionContainer;
    private bool activeSelf = false;
    public bool ActiveSelf
    {
        get
        {
            return activeSelf;
        }
        set
        {
            activeSelf = value;
        }
    }

    private void Start()
    {
        if(!MapManager.instance.Maps.ContainsKey(MapCode))
            MapManager.instance.Maps.Add(MapCode, this);
    }
}
