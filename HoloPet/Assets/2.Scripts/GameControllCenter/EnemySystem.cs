using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemies;
    public event EventHandler OnEnemyExist;
    public event EventHandler OnEnemyGone;
    
}
