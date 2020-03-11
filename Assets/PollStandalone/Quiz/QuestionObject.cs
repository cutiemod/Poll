using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionObject : MonoBehaviour
{
    public float distance = 1f;
    public Text headerText;
    public GameObject optionPrefab;
    private List<GameObject> options;
    private int totalVotes = 0;
    private PollQuestion question;

    public void SetUp(PollQuestion _question)
    {
        question = _question;
        options = new List<GameObject>();
        headerText.text = question.text;
        AddOptions(question);
    }

    private void AddOptions(PollQuestion question)
    {
        foreach (PollOption o in question.options)
        {
            GameObject oo = Instantiate(optionPrefab, transform);
            oo.GetComponent<OptionObject>().Set(o);
            totalVotes += o.votes;
            oo.transform.position -= new Vector3(0, options.Count * distance, 0);
            options.Add(oo);
        }
    }

    public int GetTotalVotes()
    {
        return totalVotes;
    }

    public PollQuestion GetQuestion()
    {
        return question;
    }
}
