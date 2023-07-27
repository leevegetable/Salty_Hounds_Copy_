using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapLinkObject : MonoBehaviour
{
    [HideInInspector]
    public MapData MyMap;
    public int index;
    public int[] MapCode;
    public enum Dir {vertical, horizontal}
    public Dir MyDir;
    [SerializeField]
    private Vector2 playerSpawnPoint;
    public Vector2 getSpawnPoint { get { return (Vector2)transform.position + playerSpawnPoint; }}

    private Interaction interact;

    private void Awake()
    {
        interact = GetComponent<Interaction>();
        MyMap = transform.parent.GetComponent<MapData>();
        for (int i = 0; i < MapCode.Length; i++)
        {
            if (MapCode[i] != -1)
                MyMap.AddMapLinker(MapCode[i], index, this);
        }
    }

    private void Start()
    {
        interact.Interactions += NextMap;
        if (MyDir == Dir.vertical)
        {
            interact.type = Interaction.Type.trigger;
            interact.key = Interaction.keyType.DirectionKey;
        }
        else
        {
            interact.type = Interaction.Type.contact;
        }
    }

    public void NextMap(GameObject target)
    {
        switch (MyDir)
        {
            case Dir.vertical:
                if (interact.inputValue != 0)
                {
                    if (interact.inputValue == 1)
                    {
                        if (MapCode[0] != -1)
                        {
                            nextmap(MapCode[0], 1, target);
                        }
                    }
                    else
                    {
                        if (MapCode[1] != -1)
                        {
                            nextmap(MapCode[1], -1, target);
                        }
                    }
                }
                break;
            case Dir.horizontal:
                nextmap(MapCode[0], 0, target);
                break;
        }
    }

    private void nextmap(int targetMapCode,int vertical, GameObject target)
    {
        if (targetMapCode != -1)
        {
            if (target.CompareTag("Player"))
            {
                PlayerManager.instance.controller.Actions.MapMove(MyMap.MapCode ,targetMapCode, index, vertical);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
         Gizmos.color = Color.blue;
         Gizmos.DrawSphere((Vector2)transform.position + playerSpawnPoint, 0.5f);
    }
#endif
}
