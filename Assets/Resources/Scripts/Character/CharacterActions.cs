using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public CharacterObject characterObject;
    public AnimationController animController;
    public SpriteRenderer Renderer;
    public bool isAnimEnd = true;
    public bool isPlayer = false;

    private void Awake()
    {
        characterObject = GetComponent<CharacterObject>();
    }

    public void CharacterShutDown()
    {
        if (isPlayer)
        {
            UIManager.instance.callFade();
            PlayerManager.instance.controller.isControl = false;
            StartCoroutine(AfterAnimEnd_ShutDown());
        }
    }

    public void CharacterMove(float dir, float speed)
    {
        if (dir == 0)
        {
            animController.setInt("int", (int)speed);
            return;
        }
        else
        {
            if (dir > 0) characterObject.Dir = 1;
            else characterObject.Dir = -1;
        }

        animController.setInt("int", (int)speed);
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
    }

    public void CharacterChase(Vector2 targetPos, bool isRun)
    {
        StartCoroutine(MoveToTargetPos(targetPos, isRun));
    }

    public void MapMove(int currentMapCode, int mapCode,int index ,int vertical)
    {
        
        if (MapManager.instance.Maps[mapCode].OpenTick > TickbasePlaySystem.instance.CurrentTick ||
            MapManager.instance.Maps[mapCode].CloseTick <= TickbasePlaySystem.instance.CurrentTick)
        {
            if (isPlayer)
            {
                DialogueManager.Instance.CallDialogueData(null, null, DialogueManager.DialoguePath.Shop, 10000);
                return;
            }
        }
        if (isPlayer)
        {
            PlayerManager.instance.autoSave.curEnergy -= 1;
        }
        if (vertical != 0)
        {
            if (vertical == 1)
            {
                Debug.Log("Input Vertical: " + vertical);
                animController.setTrigger("UpWalkIn");
                StartCoroutine(AfterAnimEnd_MapMove("Woman-UpWalk", currentMapCode, mapCode, index));
            }
            else
            {
                Debug.Log("Input Vertical: " + vertical);
                animController.setTrigger("DownWalkIn");
                StartCoroutine(AfterAnimEnd_MapMove("Woman-DownWalk",currentMapCode, mapCode, index));
            }

        }
        else
        {
            transform.position = MapManager.instance.Maps[mapCode].getTarget(currentMapCode,index);
            if (isPlayer)
            {
                PlayerOption_MapMove(mapCode);
                if (PlayerManager.instance.autoSave.curEnergy < 0)
                {
                    PlayerManager.instance.controller.Actions.CharacterShutDown();
                    Debug.Log("Energy is empty");
                }
            }

        }
    }

    

    private void PlayerOption_MapMove(int mapCode)
    {
        if (isPlayer)
        {
            PlayerManager.instance.CMVCam.SetField(MapManager.instance.Maps[mapCode].CameraField);
            PlayerManager.instance.autoSave.MapID = mapCode;
            TickbasePlaySystem.instance.NextTick();
        }
    }

    IEnumerator MoveToTargetPos(Vector2 targetPos, bool isRun)
    {
        if (targetPos.x < characterObject.transform.position.x) characterObject.Dir = -1f;
        else characterObject.Dir = 1f;
        while (characterObject.transform.localPosition.x - targetPos.x < 0.1f)
        {
            if(isRun)
                transform.Translate(Vector2.right * characterObject.Dir * characterObject.RunSpeed * Time.deltaTime);
            else
                transform.Translate(Vector2.right * characterObject.Dir * characterObject.WalkSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator AfterAnimEnd_MapMove(string animationName, int currentMapCode, int mapCode, int index)
    {
        PlayerManager.instance.controller.isControl = false;
        isAnimEnd = false;
        while (!isAnimEnd)
        {

            yield return null;
            if (animController.isEnd(animationName))
            {
                isAnimEnd = true;
                transform.position = MapManager.instance.Maps[mapCode].getTarget(currentMapCode, index);
                PlayerOption_MapMove(mapCode);
            }
        }
        StartCoroutine(AfterAnimEnd_returnControl(animationName + "-Out"));
    }

    IEnumerator AfterAnimEnd_ShutDown()
    {
        isAnimEnd = false;
        while (!isAnimEnd)
        {

            if (UIManager.instance.fade.animController.isEnd("UI_FadeOut"))
            {
                isAnimEnd = true;
                break;
            }
            yield return null;
        }
        Debug.Log("Coroutine");
        transform.position = MapManager.instance.Maps[101].getTarget();
        PlayerOption_MapMove(101);
        PlayerManager.instance.autoSave.curEnergy = PlayerManager.instance.autoSave.maxEnergy;
        if (TickbasePlaySystem.instance.CurrentDay < 6)
        {
            if (TickbasePlaySystem.getTickToTime(TickbasePlaySystem.instance.CurrentTick)[0] > 0)
                TickbasePlaySystem.instance.SetTime(TickbasePlaySystem.instance.CurrentWeek, TickbasePlaySystem.instance.CurrentDay + 1, 6, 0);
            else
                TickbasePlaySystem.instance.SetTime(TickbasePlaySystem.instance.CurrentWeek, TickbasePlaySystem.instance.CurrentDay, 6, 0);
        }
        else
        {
            TickbasePlaySystem.instance.SetTime(TickbasePlaySystem.instance.CurrentWeek + 1, 0, 6, 0);
        }
        isAnimEnd = false;
        while (!isAnimEnd)
        {

            if (UIManager.instance.fade.animController.isEnd("UI_FadeIn"))
            {
                isAnimEnd = true;
                break;
            }
            yield return null;
        }
        UIManager.instance.unCallFade();
        PlayerManager.instance.controller.isControl = true;


    }
    IEnumerator AfterAnimEnd_returnControl(string animationName)
    {
        isAnimEnd = false;
        PlayerManager.instance.controller.isControl = false;
        while (!isAnimEnd)
        {
            yield return null;
            if (animController.isEnd(animationName))
            {
                PlayerManager.instance.controller.isControl = true;
                isAnimEnd = true;
            }
        }

    }
}
