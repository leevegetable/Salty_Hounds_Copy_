using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterActions))]
public abstract class CharacterObject : MonoBehaviour
{
    public int CharacterCode;
    public AnimationController AnimController;
    public CharacterActions Actions;
    public SpriteRenderer Renderer;
    public bool isControl = false;
    public Interaction interactiveTarget;
    private float contactObstacleNormal = 0;
    protected float dir = 0f;
    [SerializeField]
    protected float walkSpeed = 4.0f;
    [SerializeField]
    protected float runSpeed = 8.0f;
    protected float moveSpeed = 0f;
    [SerializeField]
    protected int mapCode = 0;

    private void Start()
    {
        CharacterManager.instance.add(this);
    }

    public float Dir 
    {
        get 
        {
            return dir; 
        } 
        set 
        { 
            dir = value;
            if (dir > 0 && Renderer.flipX) Renderer.flipX = false;
            else if (dir < 0 && !Renderer.flipX) Renderer.flipX = true;
        } 
    }
    public float WalkSpeed { get { return walkSpeed; } protected set { walkSpeed = value; } }
    public float RunSpeed { get { return runSpeed; } protected set { runSpeed = value; } }
    public float MoveSpeed { get { return moveSpeed; } protected set { moveSpeed = value; } }
    public int MapCode { get {  return mapCode; } protected set {  mapCode = value; } }

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

    public bool checkObstacle()
    {
        float dir_ = Dir;
        float obstacleNormal = contactObstacleNormal;
        if (dir_ < 0)
            dir_ = -1;
        else if (dir_ > 0)
            dir_ = 1;
        if (obstacleNormal < 0)
            obstacleNormal = -1;
        else if (obstacleNormal > 0)
            obstacleNormal = 1;

        if (obstacleNormal == 0) return false;
        else
        {
            if (obstacleNormal == dir_) return true;
            else return false;
        }
    }
}
