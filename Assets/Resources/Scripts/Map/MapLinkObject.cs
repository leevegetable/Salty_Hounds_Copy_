using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapLinkObject : MonoBehaviour
{
    public MapData MyMap;
    public enum Dir {vertical, left, right }
    public Dir MyDir;
    [SerializeField]
    private Vector2 playerSpawnPoint;
    public Vector2 getSpawnPoint { get { return (Vector2)transform.position + playerSpawnPoint; }}

    private Interaction interact;

    private void Awake()
    {
        interact = GetComponent<Interaction>();
        MyMap = transform.parent.GetComponent<MapData>();
    }

    private void Start()
    {
        MyMap.MapLinker[getMyDir()] = this;
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
                        if (MyMap.LinkedMap[0] != -1)
                        {
                            nextmap(0, 1, target);
                        }
                    }
                    else
                    {
                        if (MyMap.LinkedMap[1] != -1)
                        {
                            nextmap(1, -1, target);
                        }
                    }
                }
                break;
            case Dir.left:
                nextmap(2, 0, target);
                break;
            case Dir.right:
                nextmap(3, 0, target);
                break;

        }
    }

    private void nextmap(int id,int vertical, GameObject target)
    {
        if (MyMap.LinkedMap[id] != -1)
        {
            if (target.CompareTag("Player"))
            {
                PlayerManager.instanse.controller.Actions.MapMove(MyMap.LinkedMap[id], vertical, getNextDir());
            }
        }
    }

    private int getMyDir()
    {
        switch (MyDir)
        {
            case Dir.vertical:
                return 0;
            case Dir.left:
                return 1;
            case Dir.right:
                return 2;
            default:
                return -1;
        }
    }

    private int getNextDir()
    {
        switch (MyDir)
        {
            case Dir.vertical:
                return 0;
            case Dir.left: 
                return 2;
            case Dir.right:
                return 1;
            default:
                return -1;
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
