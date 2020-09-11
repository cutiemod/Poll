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
    //private int totalVotes = 0;
    //private PollQuestion question;
    private QuizQuestion question;

    private bool answered = false;

    public Text answeredText;
    public SpriteRenderer answerButtonSprite;
    public Color answeredWhite;
    public Color answeredGrey;

    public void SetUp(QuizQuestion _question)
    {
        question = _question;
        options = new List<GameObject>();
        headerText.text = question.text;
        AddOptions(question);
    }

    private void AddOptions(QuizQuestion question)
    {
        int n = 0;

        foreach (QuizOption o in question.options)
        {
            byte type = 1;
            n++;
            if (n == 1)
            {
                type = 0;
            }
            else if (n == question.options.Count)
            {
                type = 2;
            }

            GameObject oo = Instantiate(optionPrefab, transform);
            oo.GetComponent<OptionObject>().Set(o, type);
            //totalVotes += o.votes;
            oo.transform.position -= new Vector3(0, options.Count * distance, 0) - baseOptionPosition;
            options.Add(oo);
        }
    }

    //public int GetTotalVotes()
    //{
    //    return totalVotes;
    //}

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

    public ushort RevealAnswers()
    {
        DisableAnswering();

        ushort goal = question.correctCount;
        ushort gotCorrect = 0;

        foreach (GameObject og in options)
        {
            var oo = og.GetComponent<OptionObject>();

            gotCorrect += oo.Evaluate();
        }

        if (goal == gotCorrect)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void CheckIfAnyOptionIsPicked()
    {
        ushort picked = 0;

        foreach(GameObject og in options)
        {
            bool thisPicked = og.GetComponent<OptionObject>().Picked();

            if (thisPicked)
            {
                picked++;
            }
        }

        if(picked > 0)
        {
            EnableNext(true);
            answerButtonSprite.color = answeredWhite;
            answeredText.text = "Ответить";
        }
        else
        {
            EnableNext(false);
            answerButtonSprite.color = answeredGrey;
            answeredText.text = "Выберите ответ";
        }
    }

    private void DisableAnswering()
    {
        foreach (GameObject og in options)
        {
            og.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void EnableNext(bool flag = true)
    {
        answeredText.GetComponentInParent<Collider2D>().enabled = flag;
    }
}
