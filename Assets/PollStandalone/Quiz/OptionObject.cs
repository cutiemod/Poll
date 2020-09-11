using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionObject : MonoBehaviour
{
    public Text textObject;
    public SpriteRenderer bg;
    public SpriteRenderer outline;

    public Sprite[] outlineSprites;

    public Sprite[] bgSprites;

    public Color outlineBlue;
    public Color outlineGreen;
    public Color outlineRed;
    public Color bgWhite;
    public Color bgRed;

    //private PollOption option;
    private QuizOption option;

    public void Set(QuizOption _option, byte type)
    {
        outline.sprite = outlineSprites[type];
        bg.sprite = bgSprites[type];

        option = _option;
        textObject.text = option.text;
    }

    public uint GetID()
    {
        return option.id;
    }

    public bool CorrectAnswer()
    {
        return option.correct;
    }

    public bool Picked()
    {
        return option.picked;
    }

    public void Pick()
    {
        if (Picked())
        {
            outline.enabled = false;
        }
        else
        {
            SetOutlineColor(outlineBlue);
        }

        option.Pick();
    }

    public ushort Evaluate()
    {
        bool correct = option.correct;
        bool picked = option.picked;

        if (correct)
        {
            SetOutlineColor(outlineGreen);
        } 
        else if (picked && !correct)
        {
            SetOutlineColor(outlineRed);
        }

        if (correct && picked)
        {
            //SetBGColor(bgWhite);
            return 1;
        }
        else if (correct && !picked)
        {
            SetBGColor(bgRed);
        } 
        else
        {
            //SetBGColor(bgWhite);
        }
        return 0;
    }

    private void SetBGColor(Color c)
    {
        c.a = bg.color.a;
        bg.color = c;
    }

    private void SetOutlineColor(Color c)
    {
        outline.enabled = true;
        c.a = bg.color.a;
        outline.color = c;
    }
}
