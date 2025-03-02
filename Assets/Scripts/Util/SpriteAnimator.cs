using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    SpriteRenderer spriteRenderer;
    List<Sprite> frames;
    float frameRate;

    int curentFrame;
    float timer;

    public SpriteAnimator(SpriteRenderer spriteRenderer, List<Sprite> frames, float frameRate=0.16f)
    {
        this.spriteRenderer = spriteRenderer;
        this.frames = frames;
        this.frameRate = frameRate;
    }

    public void Start()
    {
        curentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[0];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if (timer > frameRate)
        {
            curentFrame = (curentFrame + 1) % frames.Count;
            spriteRenderer.sprite = frames[curentFrame];
            timer -= frameRate;
        }
    }

    public List<Sprite> Frames
    {
        get
        {
            return frames;
        }
    }
}
