using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirectionManager : MonoBehaviour
{
    private Transform stateMachineTransform;
    private void Awake()
    {
        StateMachineBase stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        stateMachineTransform = stateMachine.transform;
    }
    private void Start()
    {
        SetFaceRight();
    }
    public bool GetIsFaceRight()
    {
        return stateMachineTransform.right.x > 0;
    }
    public void SetFaceRight()
    {
        stateMachineTransform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
    }
    public void SetFaceLeft()
    {
        stateMachineTransform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
    }
}
