using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public int width;
    public int height;
    public string name;
    public Sprite texture;
}
