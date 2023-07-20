using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraData : MonoBehaviour
{
    public CinemachineVirtualCamera CMVCam;
    public CinemachineConfiner Confiner;
    public CinemachinePixelPerfect PixelPerfect;

    public void SetField(PolygonCollider2D field)
    {
        Confiner.m_BoundingShape2D = field;
    }
}
