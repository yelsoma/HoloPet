using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartFxTest : MonoBehaviour
{
    public GameObject ss;
    public ParticleSystem ParticleSystem;
    public DriveMax_Cart driveMax;
    public Drive_Cart drive;
    public GameObject driveBreak;
    public GameObject driveBreakMounted;
    public MountableManager mountableManager;

    private void Awake()
    {
        drive.OnEnterState += Drive_OnEnterState;
        drive.OnExitState += Drive_OnExitState;
        driveMax.OnEnterState += DriveMax_OnEnterState;
        driveMax.OnExitState += DriveMax_OnExitState;
        mountableManager.OnChangeMounted += MountableManager_OnChangeMounted;
    }

    private void MountableManager_OnChangeMounted(object sender, System.EventArgs e)
    {
        if (mountableManager.GetIsMounted())
        {
            driveBreakMounted.SetActive(false);
        }
        else
        {
            driveBreakMounted.SetActive(true);          
        }
    }

    private void DriveMax_OnExitState(object sender, System.EventArgs e)
    {
        ParticleSystem.Clear();
        ss.SetActive(false);
        driveBreak.SetActive(false);
    }

    private void DriveMax_OnEnterState(object sender, System.EventArgs e)
    {
        ss.SetActive(true);
        var main = ParticleSystem.main;
        main.simulationSpeed = 3f;
        ParticleSystem.Play();
        driveBreak.SetActive(true);
    }

    private void Drive_OnExitState(object sender, System.EventArgs e)
    {
        ParticleSystem.Clear();
        ss.SetActive(false);
        driveBreak.SetActive(false);
    }

    private void Drive_OnEnterState(object sender, System.EventArgs e)
    {
        ss.SetActive(true);
        var main = ParticleSystem.main;
        main.simulationSpeed = 1.5f;
        ParticleSystem.Play();
        driveBreak.SetActive(true);
    }
}
