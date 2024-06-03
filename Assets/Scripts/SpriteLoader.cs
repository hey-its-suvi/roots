using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{
    private Dictionary<string, Sprite> sprites;

    void Awake()
    {
            LoadSprites();
    }

    void LoadSprites()
    {
        sprites = new Dictionary<string, Sprite>();
        Sprite[] spritesArray = Resources.LoadAll<Sprite>("Sprites");

        foreach (var sprite in spritesArray)
        {
            sprites[sprite.name] = sprite;
        }
        Debug.Log("Loaded "+ sprites.Count +" Sprites");
    }
     
    public Sprite getSprite(string spriteName)
    {
        return sprites.ContainsKey(spriteName) ? sprites[spriteName] : null;
    }
   
}
