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
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dieSprites[dieSprites.Length-1];
        currentSpriteNum = dieSprites.Length-1;
    }

    public IEnumerator CycleFace(int increment = -1, float faceDelay = .3f)
    {
        if (increment != 0)
        {
            int targetFace = currentSpriteNum + increment;
            int value; // good name
            if (increment < 0)
            {
                value = -1;
            }
            else
            {
                value = 1;
            }
            while (currentSpriteNum != targetFace)
            {

                if (currentSpriteNum > 0 && currentSpriteNum < dieSprites.Length)
                {
                    currentSpriteNum += value;
                    spriteRenderer.sprite = dieSprites[currentSpriteNum];
                    yield return new WaitForSeconds(faceDelay);
                }
            }
        }
        
    }
    public void ShowFace(int face, float faceDelay = .3f)
    {
        Debug.Log(face - currentSpriteNum);
        StartCoroutine(CycleFace(face - currentSpriteNum,faceDelay));
    }
}
