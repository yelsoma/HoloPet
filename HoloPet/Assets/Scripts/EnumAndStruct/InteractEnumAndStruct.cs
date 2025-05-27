using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//option tags that interacter do
public enum interactOptionE
{
    hit,
    happyChat,
    sit,
    use,
    grab
}

//options for interacter
[System.Serializable]
public struct InteracterOption
{
    [SerializeField] private string interacterOptionName;
    [SerializeField] private interactOptionE interacterOptionEnum;
    [SerializeField] private float chance;
    [SerializeField] private StateBase optionState;

    public string GetInteracterOptionName
    {
        get { return interacterOptionName; }
    }

    public interactOptionE GetInteracterOptionEnum
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

//options for interactable Objects
[System.Serializable]
public struct InteractedOption
{
    [SerializeField] private string interactedOptionName;
    [SerializeField] private interactOptionE interactedOptionEnum;
    [SerializeField] private float chance;
    [SerializeField] private StateBase optionState;

    public string GetInteractedOptionName
    {
        get { return interactedOptionName; }
    }

    public interactOptionE GetInteractedOptionEnum
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

