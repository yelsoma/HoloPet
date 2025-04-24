using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public void MoveUp(float speed)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
    }
    public void MoveDown(float speed)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
    }
    public void MoveRight(float speed)
    {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
    public void MoveLeft(float speed)
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }    
}
