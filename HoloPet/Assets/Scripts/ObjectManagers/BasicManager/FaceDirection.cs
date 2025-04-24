using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    private bool isFaceRight;
    private void Start()
    {
        isFaceRight = true;
    }
    public bool GetIsFaceRight()
    {
        return isFaceRight;
    }
    public void SetFaceRight()
    {       
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        isFaceRight = true;
    }
    public void SetFaceLeft()
    {       
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        isFaceRight = false;
    }
}
