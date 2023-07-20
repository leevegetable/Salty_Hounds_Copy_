using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instanse;
    public PlayerController controller;
    public GameObject PlayerObject;
    public CameraData CMVCam;
    public SaveData autoSave;
    public int CurrentWeek = 0;
    public int CurrentDay = 0;
    public int CurrentHour = 0;
    public int CurrentMinute = 0;
    public int CurrentMapID = 0;
    private void Awake()
    {
        instanse = GetComponent<PlayerManager>();
        controller = PlayerObject.GetComponent<PlayerController>();
    }
}
