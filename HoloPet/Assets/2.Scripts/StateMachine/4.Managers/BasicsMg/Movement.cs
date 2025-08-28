using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Transform selfTransform;

    private void Awake()
    {
        selfTransform = transform.root;
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
}
