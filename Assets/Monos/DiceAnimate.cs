using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DiceAnimate : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
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
        yield return PlayRoll();
        if (increment != 0) // would be cool to make the delay be fast at first and slow down near the end.
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
                currentSpriteNum = Mathf.Clamp(currentSpriteNum + value, 0, dieSprites.Length - 1);
                spriteRenderer.sprite = dieSprites[currentSpriteNum];
                yield return new WaitForSeconds(faceDelay);
            }
        }
        
    }
    public void ShowFace(int face, float faceDelay = .3f)
    {
        Debug.Log(face - currentSpriteNum);
        StartCoroutine(CycleFace(face - currentSpriteNum,faceDelay));
    }

    public IEnumerator PlayRoll(int rolls = 6, float delay = .12f)
    {
        for (int i = 0; i < rolls; i++)
        {
            spriteRenderer.sprite = dieSprites[Random.Range(1, dieSprites.Length - 1)];
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delay * 1.5f);
        spriteRenderer.sprite = dieSprites[currentSpriteNum];
    }
    public IEnumerator PlayDeath(float delay = .5f)
    {
        spriteRenderer.sprite = dieSprites[0];
        yield return new WaitForSeconds(.5f);
    }
}
