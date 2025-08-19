using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InteracterOption
{
    [SerializeField] private string interacterOptionName;
    [SerializeField] private interactOptionEnum interacterOptionEnum;
    [SerializeField] private float chance;
    [SerializeField] private StateBase optionState;

    public string GetInteracterOptionName
    {
        get { return interacterOptionName; }
    }

    public interactOptionEnum GetInteracterOptionEnum
    {
        get { return interacterOptionEnum; }
    }

    public float GetChance
    {
        get { return chance; }
    }

    public StateBase GetOptionState
    {
        get { return optionState; }
    }
}

[System.Serializable]
public struct InteractedOption
{
    [SerializeField] private string interactedOptionName;
    [SerializeField] private interactOptionEnum interactedOptionEnum;
    [SerializeField] private float chance;
    [SerializeField] private StateBase optionState;

    public string GetInteractedOptionName
    {
        get { return interactedOptionName; }
    }

    public interactOptionEnum GetInteractedOptionEnum
    {
        get { return interactedOptionEnum; }
    }

    public float GetChance
    {
        get { return chance; }
    }

    public StateBase GetOptionState
    {
        get { return optionState; }
    }
}

public struct BothInteractOption
{
    private InteracterOption interacterOption;
    private InteractedOption interactedOption;
    private float addedChance;

    public void SetInteracterOption(InteracterOption interacterOption)
    {
        this.interacterOption = interacterOption;
    }
    public void SetInteractedOption(InteractedOption interactedOption)
    {
        this.interactedOption = interactedOption;
    }
    public InteracterOption GetInteracterOption()
    {
        return interacterOption;
    }
    public InteractedOption GetInteractedOption()
    {
        return interactedOption;
    }
    public void SetAddedChance(float addedChance)
    {
        this.addedChance = addedChance;
    }
    public float GetAddedChance()
    {
        return addedChance;
    }
}

