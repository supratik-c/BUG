using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public List<Sprite> passedSprites = new List<Sprite>();

    public Image _Image;

    public float FrameDelay = 0.1f;

    public bool Running;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Animate() 
    {
        Running = true;
        foreach(Sprite _sprite in passedSprites)
        {
            _Image.sprite = _sprite;
            yield return new WaitForSeconds(FrameDelay);
        }
        Running = false;
    }

    public void StartAnimation()
    {
        if (Running) 
        {
            StopCoroutine(Animate());
            _Image.sprite = passedSprites[0];
        }
        
        StartCoroutine(Animate());
    }

    public void SetSprites(List<Sprite> Sprites)
    {
        passedSprites = Sprites;
        _Image.sprite = passedSprites[0];
    }

}
