using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionObject : MonoBehaviour
{
    public Text textObject;

    private PollOption option;

    public void Set(PollOption _option)
    {
        option = _option;
        textObject.text = option.text;
    }

    public int GetID()
    {
        return option.id;
    }

    public int GetVotes()
    {
        return option.votes;
    }
}
