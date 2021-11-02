using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimation : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();

    public Image _Image;

    public float FrameDelay = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        _Image.sprite = Sprites[0];
    }

    IEnumerator Animate() 
    {
        foreach(Sprite _sprite in Sprites)
        {
            _Image.sprite = _sprite;
            yield return new WaitForSeconds(FrameDelay);
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(Animate());
    }


}
