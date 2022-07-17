using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DiceAnimate : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    int _currentSpriteNum = 0;
    int currentSpriteNum {
        get
        {
            return _currentSpriteNum;
        }
        set
        {
            _currentSpriteNum = value;
            spriteRenderer.sprite = dieSprites[_currentSpriteNum];
        }
    }
    public Sprite[] dieSprites;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
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
        var originalSpriteNum = currentSpriteNum;
        for (int i = 0; i < rolls; i++)
        {
            currentSpriteNum = Random.Range(1, dieSprites.Length - 1);
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(delay * 1.5f);
        currentSpriteNum = originalSpriteNum;
    }
    public IEnumerator PlayDeath(float delay = .5f)
    {
        currentSpriteNum = 0;
        yield return new WaitForSeconds(.5f);
    }
}
