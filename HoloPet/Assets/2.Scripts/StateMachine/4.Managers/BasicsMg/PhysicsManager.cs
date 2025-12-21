using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    private Transform selfTransform;
    [SerializeField] private float gravity;
    [SerializeField] private float speed;
    [SerializeField] private float jumpGravity;
    private float upPower;
    private float fallSpeed;
    private float maxFallSpeed = 9;

    private void Awake()
    {
        StateMachineBase stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        selfTransform = stateMachine.transform;
    } 
    public void MoveUp(float speed)
    {
        selfTransform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
    }
    public void MoveDown(float speed)
    {
        selfTransform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
    }
    public void MoveRight(float speed)
    {
        selfTransform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
    public void MoveLeft(float speed)
    {
        selfTransform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
    public void MoveUpMultiply(float speedMultiply)
    {
        selfTransform.position = new Vector2(transform.position.x, transform.position.y + speed * speedMultiply * Time.deltaTime);
    }
    public void MoveDownMultiply(float speedMultiply)
    {
        selfTransform.position = new Vector2(transform.position.x, transform.position.y - speed * speedMultiply * Time.deltaTime);
    }
    public void MoveRightMultiply(float speedMultiply)
    {
        selfTransform.position = new Vector2(transform.position.x + speed * speedMultiply * Time.deltaTime, transform.position.y);
    }
    public void MoveLeftMultiply(float speedMultiply)
    {
        selfTransform.position = new Vector2(transform.position.x - speed * speedMultiply * Time.deltaTime, transform.position.y);
    }
    public void SetJump(float upPower)
    {
        this.upPower = upPower;
    }
    public bool KeepJump()
    {
        if(upPower > 0f)
        {
            selfTransform.position = new Vector2(transform.position.x, transform.position.y + upPower * Time.deltaTime);
            upPower -= jumpGravity * Time.deltaTime;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetFall()
    {
        fallSpeed = 0f;
    }
    public void KeepFall()
    {
        if(fallSpeed <= maxFallSpeed)
        {
            selfTransform.position = new Vector2(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);
            fallSpeed += gravity * Time.deltaTime;
        }
        else
        {
            selfTransform.position = new Vector2(transform.position.x, transform.position.y - maxFallSpeed * Time.deltaTime);
        }
    }
    public void KeepFall(float multiply)
    {
        if (fallSpeed <= maxFallSpeed)
        {
            selfTransform.position = new Vector2(transform.position.x, transform.position.y - fallSpeed * Time.deltaTime);
            fallSpeed += gravity * multiply* Time.deltaTime;
        }
        else
        {
            selfTransform.position = new Vector2(transform.position.x, transform.position.y - maxFallSpeed * Time.deltaTime);
        }
    }
    public float GetGravity()
    {
        return gravity;
    }
}
