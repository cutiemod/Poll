using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class QuestionObject : MonoBehaviour
{
    public Vector3 baseOptionPosition;
    public float distance = 1f;
    public Text headerText;
    public GameObject optionPrefab;
    private List<GameObject> options;
    private int totalVotes = 0;
    //private PollQuestion question;
    private QuizQuestion question;

    private bool answered = false;

    public Text answeredText;

    public void SetUp(QuizQuestion _question)
    {
        question = _question;
        options = new List<GameObject>();
        headerText.text = question.text;
        AddOptions(question);
    }

    private void AddOptions(QuizQuestion question)
    {
        foreach (QuizOption o in question.options)
        {
            GameObject oo = Instantiate(optionPrefab, transform);
            oo.GetComponent<OptionObject>().Set(o);
            //totalVotes += o.votes;
            oo.transform.position -= new Vector3(0, options.Count * distance, 0) - baseOptionPosition;
            options.Add(oo);
        }
    }

    public int GetTotalVotes()
    {
        return totalVotes;
    }

    public QuizQuestion GetQuestion()
    {
        return question;
    }

    public bool AnswerButtonPressed(bool last = false)
    {
        if (!answered)
        {
            answered = true;
            if (last)
            {
                answeredText.text = "Посмотреть результаты";
            }
            else
            {
                answeredText.text = "Следующий вопрос";
            }
        }
        else
        {
            answered = false;
            //answeredText.text = "Ответить";
        }

        return answered;
    }

    public void RevealAnswers()
    {
        foreach(GameObject og in options)
        {
            var oo = og.GetComponent<OptionObject>();

            oo.Evaluate();
        }
    }
}
