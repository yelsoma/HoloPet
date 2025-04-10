using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

public class FaceDirection : MonoBehaviour
{
    [SerializeField] ObjectData data;
    private void Start()
    {
        data.OnChangeFaceDirection += Data_OnChangeFaceDirection;
    }

    private void Data_OnChangeFaceDirection(object sender, System.EventArgs e)
    {
        if (data.GetIsFaceRight())
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        }
        else
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
        }
    }   
}
