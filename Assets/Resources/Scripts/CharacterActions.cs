using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public AnimationController animController;
    public SpriteRenderer Renderer;
    public bool isAnimEnd = true;
    public bool isPlayer = false;


    public void CharacterMove(float dir, float speed)
    {
        if (dir == 0)
        {
            animController.setInt("int", (int)speed);
            return;
        }
        if (dir > 0 && Renderer.flipX) Renderer.flipX = false;
        else if (dir < 0 && !Renderer.flipX) Renderer.flipX = true;
        animController.setInt("int", (int)speed);
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
    }

    public void CharacterChase()
    {
        
    }

    public void MapMove(int mapCode,int vertical, int pos)
    {
        if (vertical != 0)
        {
            if (vertical == 1)
            {
                animController.setTrigger("UpWalkIn");
                StartCoroutine(AfterAnimEnd_MapMove("Woman-UpWalk", mapCode, pos));
            }
            else
            {
                animController.setTrigger("DownWalkIn");
                StartCoroutine(AfterAnimEnd_MapMove("Woman-DownWalk", mapCode, pos));
            }

        }
        else
        {
            transform.position = MapManager.instance.Maps[mapCode].MapLinker[pos].getSpawnPoint;
            PlayerOption_MapMove(mapCode);
        }
    }

    private void PlayerOption_MapMove(int mapCode)
    {
        if (isPlayer)
        {
            PlayerManager.instanse.CMVCam.SetField(MapManager.instance.Maps[mapCode].CameraField);
            PlayerManager.instanse.CurrentMapID = mapCode;
        }
    }


    IEnumerator AfterAnimEnd_MapMove(string animationName,int mapCode, int pos)
    {
        PlayerManager.instanse.controller.isControl = false;
        isAnimEnd = false;
        while (!isAnimEnd)
        {

            yield return null;
            if (animController.isEnd(animationName))
            {
                isAnimEnd = true;
                transform.position = MapManager.instance.Maps[mapCode].MapLinker[pos].getSpawnPoint;
                PlayerOption_MapMove(mapCode);
            }
        }
        StartCoroutine(AfterAnimEnd_returnControl(animationName + "-Out"));
    }
    IEnumerator AfterAnimEnd_returnControl(string animationName)
    {
        isAnimEnd = false;
        while (!isAnimEnd)
        {

            yield return null;
            if (animController.isEnd(animationName))
            {
                PlayerManager.instanse.controller.isControl = true;
                isAnimEnd = true;
            }
        }

    }
}
