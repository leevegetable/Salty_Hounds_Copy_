using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionContainer : MonoBehaviour
{
    private Dictionary<int, InteractionObject> interactions = new Dictionary<int, InteractionObject>();

    //private void Awake()
    //{
    //    transform.GetComponent<MapData>().interactionContainer = this;
    //}

    //public void Add(InteractionObject interaction)
    //{
    //    interactions.Add(interaction.gameObject.GetInstanceID(),interaction);
    //}

    //public InteractionObject getInteraction(int instanceID)
    //{
    //    if (interactions[instanceID] == null) return null;
    //    return interactions[instanceID];
    //}
}
