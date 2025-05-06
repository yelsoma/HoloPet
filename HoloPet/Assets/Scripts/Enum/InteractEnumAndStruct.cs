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
    public string interacterOptionName;
    public interactOptionE interacterOptionEnum;
    public float chance;
    public StateBase optionState;
}

//options for interactable Objects
[System.Serializable]
public struct InteractedOption
{
    public string interactedOptionName;
    public interactOptionE interactedOptionEnum;
    public float chance;
    public StateBase optionState;
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

