using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public Dictionary<int,CharacterObject> CharacterContainer = new Dictionary<int,CharacterObject>();

    private void Awake()
    {
        instance = this;
    }

    public void add(CharacterObject obj)
    {
        if (CharacterContainer.ContainsKey(obj.GetInstanceID())) return;
        CharacterContainer.Add(obj.GetInstanceID(), obj);
    }

    public CharacterObject getCharacter(int id)
    {
        if (CharacterContainer.ContainsKey(id)) return CharacterContainer[id];
        else return null;
    }
}
