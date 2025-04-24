using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLayerManager : MonoBehaviour
{
    [SerializeField] private SpriteLayerManager[] topSprites;
    [SerializeField] private SpriteLayerManager[] botSprites;

    public void SetNewLayer()
    {
        AddBotSprites();
        AddTopSprites();
    }

    // pull to top
    public void PullToTop()
    {
        // get self ObjectLayerManager and chid ObjectLayerManager
        ObjectLayerManager[] myChildObLayerManagers = transform.GetComponentsInChildren<ObjectLayerManager>();
        //loop every one and pull to top
        foreach (ObjectLayerManager objectLayerManager in myChildObLayerManagers)
        {
            objectLayerManager.PullSelfBotAndMountTop();
        }
    }
    public void PullSelfBotAndMountTop()
    {
        PullBotLayer();
        // check is mounting 
        if (transform.TryGetComponent(out IMountingAbility mountingAbility) && mountingAbility.GetIsMounting())
        {
            //  pull my mount's top sprite
            mountingAbility.GetMount().GetTransform().GetComponent<ObjectLayerManager>().PullTopLayer();
        }
        // check is mounted
        if (transform.TryGetComponent(out IMountable mountable) && mountable.GetIsMounted())
        {
            //  my mounter will pull my top
            return;
        }
        PullTopLayer();
    }
    public void PullBotLayer()
    {
        RemoveBotSprite();
        AddBotSprites();
    }
    public void PullTopLayer()
    {
        RemoveTopSprite();
        AddTopSprites();
    }

    //instert after someone
    public void InsertTo(ObjectLayerManager targetObLayerManager)
    {
        // get self ObjectLayerManager and chid ObjectLayerManager
        ObjectLayerManager[] myChildObLayerManagers = transform.GetComponentsInChildren<ObjectLayerManager>();
        //loop every one and insert
        int insertI = targetObLayerManager.GetLastTopLayer();
        foreach (ObjectLayerManager objectLayerManager in myChildObLayerManagers)
        {
            objectLayerManager.InsertSelfAndMountTop(insertI + 1);
        }
    }
    public int InsertSelfAndMountTop(int i)
    {
         int nextI = InsertBotTo(i);
        // check is mounting 
        if (transform.TryGetComponent(out IMountingAbility mountingAbility) && mountingAbility.GetIsMounting())
        {
            // insert my mount's top sprite
            int nextNextI = mountingAbility.GetMount().GetTransform().GetComponent<ObjectLayerManager>().InsertTopTo(nextI);
            nextI = nextNextI;
        }
        // check is mounted
        if (transform.TryGetComponent(out IMountable mountable) && mountable.GetIsMounted())
        {
            //  my mounter will pull my top
            return nextI;
        }
        int nextNextNextI = InsertTopTo(nextI);
        return nextNextNextI;
    }
    public int InsertTopTo(int i)
    {
        RemoveTopSprite();
        return InsertTopSprite(i);
    }
    public int InsertBotTo(int i)
    {
        RemoveBotSprite();
        return InsertBotSprite(i);
    }
    public int GetLastTopLayer()
    {
        if(topSprites.Length > 0)
        {
            return topSprites[topSprites.Length - 1].GetLayer();
        }
        return botSprites[botSprites.Length - 1].GetLayer();
    }
    public int GetMainLayer()
    {
        if(botSprites.Length > 0)
        {
            return botSprites[0].GetLayer();
        }
        return topSprites[0].GetLayer();
    }

    //package method
    private void AddTopSprites()
    {
        if (topSprites == null)
        {
            return;
        }
        foreach (SpriteLayerManager spriteLayerManager in topSprites)
        {
            LayerCenter.AddToLayerCenter(spriteLayerManager);
        }
    }
    private void AddBotSprites()
    {
        if (botSprites == null)
        {
            return;
        }
        foreach (SpriteLayerManager spriteLayerManager in botSprites)
        {
            LayerCenter.AddToLayerCenter(spriteLayerManager);
        }
    }
    private void RemoveTopSprite()
    {
        //Remove top
        foreach (SpriteLayerManager spriteLayerManager in topSprites)
        {
            LayerCenter.RemoveFromLayerCenter(spriteLayerManager);
        }
    }
    private void RemoveBotSprite()
    {
        //Remove bot
        foreach (SpriteLayerManager spriteLayerManager in botSprites)
        {
            LayerCenter.RemoveFromLayerCenter(spriteLayerManager);
        }
    }
    private int InsertTopSprite(int i)
    {
        //Remove top
        foreach (SpriteLayerManager spriteLayerManager in topSprites)
        {
            LayerCenter.InsertToLayerCenter(i, spriteLayerManager);
            i++;
        }
        return i;
    }
    private int InsertBotSprite(int i)
    {
        //Remove  bot
        foreach (SpriteLayerManager spriteLayerManager in botSprites)
        {
            LayerCenter.InsertToLayerCenter(i, spriteLayerManager);
            i++;
        }
        return i;
    }
}
