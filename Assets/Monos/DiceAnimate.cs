using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DiceAnimate : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int currentSpriteNum;
    public Sprite[] dieSprites;
    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = dieSprites[dieSprites.Length];
        //currentSpriteNum = dieSprites.Length;
    }

    public void NextFace(int increment = -1)
    {
        for(int i = 0; i < increment; i++)
        {
            if (currentSpriteNum > 0 && currentSpriteNum < dieSprites.Length)
            {
                currentSpriteNum += increment;
                spriteRenderer.sprite = dieSprites[currentSpriteNum];
                
            }
        }//test
    }
}
