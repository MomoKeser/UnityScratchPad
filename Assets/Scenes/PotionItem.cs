using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : Item
{
    public override void UseEffect()
    {
        Debug.Log("Consume Potion!"); 
    }
}
