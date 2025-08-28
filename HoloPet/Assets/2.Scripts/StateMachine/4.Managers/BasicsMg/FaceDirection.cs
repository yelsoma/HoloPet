using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirectionManager : MonoBehaviour
{
    private bool isFaceRight;
    private Transform stateMachineTransform;
    private void Awake()
    {
        stateMachineTransform = transform.root;
    }
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
        stateMachineTransform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        isFaceRight = true;
    }
    public void SetFaceLeft()
    {
        stateMachineTransform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        isFaceRight = false;
    }
}
