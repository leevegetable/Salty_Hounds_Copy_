using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[RequireComponent(typeof(InteractionContainer))]
public class MapData : MonoBehaviour
{
    public int MapCode;
    public string MapName;
    public int OpenH = 0;
    public int OpenM = 0;
    public int CloseH = 24;
    public int CloseM = 00;
    public int OpenTick { get { return TickbasePlaySystem.getTimeToTick(OpenH, OpenM); } }
    public int CloseTick { get { return TickbasePlaySystem.getTimeToTick(CloseH, CloseM); } }
    /// <summary>
    /// MapLinker [ MapCode ] [ MapLinkObject index ] = MapLinkObject
    /// </summary>
    public Dictionary<int, Dictionary<int, MapLinkObject>> MapLinkers = new Dictionary<int, Dictionary<int, MapLinkObject>>();
    public void AddMapLinker(int mapCode, int index, MapLinkObject mapLinker)
    {
        if (!MapLinkers.ContainsKey(mapCode))
        {
            MapLinkers.Add(mapCode, new Dictionary<int, MapLinkObject>());
            MapLinkers[mapCode].Add(index, mapLinker);
        }
        else
        {
            MapLinkers[mapCode].Add(index, mapLinker);
        }
    }
    public Vector2 GroundVector;
    public Vector2 getTarget(int currentMapCode, int index)
    {
        return MapLinkers[currentMapCode][index].getSpawnPoint;
    }
    public Vector2 getTarget()
    {
        return (Vector2)transform.position + GroundVector;
    }
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
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position + new Vector3(0,25,0), MapCode.ToString());
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere((Vector2)transform.position + GroundVector, 0.5f);
    }
#endif
}
