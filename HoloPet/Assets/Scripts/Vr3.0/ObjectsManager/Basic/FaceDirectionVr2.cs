using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirectionVr2 : MonoBehaviour
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
        if (!isFaceRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            isFaceRight = true;

        }        
    }
    public void SetFaceLeft()
    {
        if (isFaceRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            isFaceRight = false;
        }
    }
}
