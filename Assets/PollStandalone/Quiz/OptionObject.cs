using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionObject : MonoBehaviour
{
    public Text textObject;
    public SpriteRenderer bg;
    public SpriteRenderer outline;

    //private PollOption option;
    private QuizOption option;

    public void Set(QuizOption _option)
    {
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
            SetBGColor(Color.white);
        }
        else
        {
            SetBGColor(Color.green);
        }

        option.Pick();
    }

    public void Evaluate()
    {
        bool correct = option.correct;
        bool picked = option.picked;

        outline.enabled = true;

        if (correct)
        {
            outline.color = Color.green;
        }
        else
        {
            outline.color = Color.red;
        }

        if (correct && picked)
        {
            SetBGColor(Color.green);
        }
        else if (!correct && picked)
        {
            SetBGColor(Color.red);
        }
        else if (correct && !picked)
        {
            SetBGColor(Color.yellow);
        }
        else
        {
            SetBGColor(Color.gray);
        }
    }

    private void SetBGColor(Color c)
    {
        c.a = bg.color.a;
        bg.color = c;
    }
}
