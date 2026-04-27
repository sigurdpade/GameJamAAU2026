using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite fullheart, halfheart, emptyheart;
    Image heartImage;
    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetheartImage(Heartstatus satus)
    {
        switch (satus)
        {
            case Heartstatus.empty:
                heartImage.sprite = emptyheart; break;
            case Heartstatus.half:
                heartImage.sprite = halfheart; break;
            case Heartstatus.full:
                heartImage.sprite = fullheart; break;
        }
    }

    public enum Heartstatus
    {
        empty = 0,
        half = 1,
        full = 2,

    }
}
