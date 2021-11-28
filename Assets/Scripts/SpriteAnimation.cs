using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public List<Sprite> passedSprites = new List<Sprite>();

    public Image _Image;

    public float FrameDelay = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Animate() 
    {
        foreach(Sprite _sprite in passedSprites)
        {
            _Image.sprite = _sprite;
            yield return new WaitForSeconds(FrameDelay);
        }
    }

    public void StartAnimation()
    {
        
        StartCoroutine(Animate());
    }

    public void SetSprites(List<Sprite> Sprites)
    {
        passedSprites = Sprites;
        _Image.sprite = passedSprites[0];
    }

}
