using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostUI : MonoBehaviour
{
    [SerializeField] private Button_UI homeButton;

    private void Start()
    {
        homeButton.ClickFunc = () =>
        {
            GameController.Instance.ResetBattleField();   
            GameController.Instance.ClearTempItem();
            gameObject.SetActive(false);
        };
    }
}
