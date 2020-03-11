using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollData
{
    public string name;
    public int id;

    public List<PollQuestion> questions;

    public PollData(string _name = "", int _id = -1)
    {
        name = _name;
        id = _id;
        questions = new List<PollQuestion>();
    }
    
    public void AddQuestion(string _text, int _id)
    {
        questions.Add(new PollQuestion(_text, _id));
    }

    public void AddOption(int q_id, string _text, int _id, int _votes)
    {
        foreach(PollQuestion q in questions)
        {
            if(q.id == q_id)
            {
                q.AddOption(_text, _id, _votes);
            }
        }
    }
}

public class PollQuestion
{
    public string text;
    public int id;

    public List<PollOption> options;

    public PollQuestion(string _text = "", int _id = -1)
    {
        text = _text;
        id = _id;
        options = new List<PollOption>();
    }

    public void AddOption(string _text, int _id, int _votes)
    {
        options.Add(new PollOption(_text, _id, _votes));
    }
}

public class PollOption
{
    public string text;
    public int id;
    public int votes;
    public bool voted;

    public PollOption(string _text, int _id = -1, int _votes = -1, bool _voted = false)
    {
        text = _text;
        id = _id;
        votes = _votes;
        voted = _voted;
    }

    public void Vote()
    {
        voted = true;
    }
}