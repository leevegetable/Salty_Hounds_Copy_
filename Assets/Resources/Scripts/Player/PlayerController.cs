using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AnimationController AnimController;
    public CharacterActions Actions;
    public SpriteRenderer Renderer;
    public bool isControl = false;

    [SerializeField]
    private float WalkSpeed = 4.0f;
    [SerializeField]
    private float RunSpeed = 6.0f;
    private float MoveSpeed = 0f;
    [SerializeField]
    public Interaction interactiveTarget;
    private float contactObstacleNormal = 0;
    void Update()
    {
        if (isControl)
        {
            if (ControlManager.instance.isSelect || ControlManager.instance.getVerticalDir != 0)
            {
                if (interactiveTarget != null)
                    Intreacting();
            }
            if (ControlManager.instance.getHorizontalDir != 0 && !checkObstacle())
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    MoveSpeed = RunSpeed;
                else
                    MoveSpeed = WalkSpeed;
            }
            else
                MoveSpeed = 0;
            Actions.CharacterMove(ControlManager.instance.getHorizontalDir, MoveSpeed);

        }
    }

    public void Intreacting()
    {
         if (interactiveTarget.key == Interaction.keyType.select && ControlManager.instance.isSelect)
         {
             interactiveTarget.Interacting(gameObject);
         }
         else if(interactiveTarget.key == Interaction.keyType.DirectionKey && ControlManager.instance.getVerticalDir != 0)
         {
             interactiveTarget.inputValue = ControlManager.instance.getVerticalDir;
             interactiveTarget.Interacting(gameObject);
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ContactPoint2D contactPoint = collision.contacts[0];
            contactObstacleNormal = contactPoint.normal.x * -1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            contactObstacleNormal = 0;
        }
    }

    private bool checkObstacle()
    {
        float dir = ControlManager.instance.getHorizontalDir;
        float obstacleNormal = contactObstacleNormal;
        if (dir < 0)
            dir = -1;
        else if (dir > 0)
            dir = 1;
        if (obstacleNormal < 0)
            obstacleNormal = -1;
        else if(obstacleNormal > 0)
            obstacleNormal = 1;

        if (obstacleNormal == 0) return false;
        else
        {
            if (obstacleNormal == dir) return true;
            else return false;
        }
    }
}
