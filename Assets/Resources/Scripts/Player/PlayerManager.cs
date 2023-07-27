using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerController controller;
    public GameObject PlayerObject;
    public CameraData CMVCam;
    public SaveData autoSave = new SaveData();
    private void Awake()
    {
        instance = GetComponent<PlayerManager>();
        if(PlayerObject != null)
            controller = PlayerObject.GetComponent<PlayerController>();
    }
}
