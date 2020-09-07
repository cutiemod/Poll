using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class QuizData
{
    public uint id;
    public string name;

    public List<QuizQuestion> questions;

    public QuizData(string _name = "", uint _id = 0)
    {
        name = _name;
        id = _id;
        questions = new List<QuizQuestion>();
    }

    public void AddQuestion(string _text, uint _id, ushort _correctCount = 1)
    {
        questions.Add(new QuizQuestion(_text, _id, _correctCount));
    }

    public void AddOption(uint q_id, string _text, uint _id, bool _correct)
    {
        foreach (QuizQuestion q in questions)
        {
            if (q.id == q_id)
            {
                q.AddOption(_text, _id, _correct);
                return;
            }
        }

        Debug.LogFormat("Question ID: {0} not found", q_id);
    }

    public void PickOption(uint q_id, uint _id)
    {
        foreach (QuizQuestion q in questions)
        {
            if (q.id == q_id)
            {
                foreach (QuizOption o in q.options)
                {
                    if (o.id == _id)
                    {
                        o.picked = true;
                        return;
                    }
                }
            }
        }
    }
}

[Serializable]
public class QuizQuestion
{
    public uint id;
    public string text;
    public ushort correctCount;

    public List<QuizOption> options;

    public QuizQuestion(string _text, uint _id, ushort _correctCount)
    {
        text = _text;
        id = _id;
        correctCount = _correctCount;
        options = new List<QuizOption>();
    }

    public void AddOption(string _text, uint _id, bool _correct)
    {
        options.Add(new QuizOption(_text, _id, _correct));
    }
}

[Serializable]
public class QuizOption
{
    public uint id;
    public string text;
    public bool correct;
    public bool picked;

    public QuizOption(string _text, uint _id, bool _correct)
    {
        text = _text;
        id = _id;
        correct = _correct;
        picked = false;
    }

    public void Pick()
    {
        picked = !picked;
    }
}