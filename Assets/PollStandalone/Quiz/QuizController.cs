using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public float revealTime;
    public float questionDelayTime;
    public float questionChangeTime;

    //Dictionary<int, int> pollData;

    public float distance;

    public GameObject startScreen;
    public GameObject endScreen;

    public GameObject questionPrefab;

    private List<GameObject> questions;

    private int currentQuestion;

    private string currentAnswers;

    private WebGM GM;
    private DataManager quizManager;

    private QuizData quiz;

    void Start()
    {
        GetData();
    }

    private void GetData()
    {
        quizManager = GetComponent<DataManager>();
        quiz = quizManager.GetData();

        CreateQuestions();

        currentQuestion = -1;
        GoToNextQuestion();
    }

    public void SetQuiz(QuizData _quiz)
    {
        quiz = _quiz;

        startScreen.GetComponentInChildren<Text>().text = "Готово!";
    }

    private void CreateQuestions()
    {
        questions = new List<GameObject>();

        foreach (QuizQuestion q in quiz.questions)
        {
            GameObject newQuestion = Instantiate(questionPrefab);
            newQuestion.transform.parent = transform;
            newQuestion.transform.position += new Vector3(distance * (questions.Count + 1), 0, 0);

            questions.Add(newQuestion);
            newQuestion.GetComponent<QuestionObject>().SetUp(q);
        }

        endScreen.transform.position = new Vector3(distance * (questions.Count + 1), 0, 0);
    }

    private void Update()
    {
        if (Input.anyKeyDown/* && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended*/)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit && hit.collider != null)
            {
                CustomButton cb = hit.collider.GetComponent<CustomButton>() ?? null;
                if (cb != null)
                {
                    if (cb.value == "0")
                    {
                        OptionIsClicked(cb);
                    } 
                    else if(cb.value == "next")
                    {
                        NextIsClicked();
                    }
                }
            }
        }
    }

    private void NextIsClicked()
    {
        var qo = questions[currentQuestion].GetComponent<QuestionObject>();
        bool answered = qo.AnswerButtonPressed(currentQuestion == questions.Count-1);
        if (answered)
        {
            RevealCorrectAnswers(qo);
        }
        else
        {
            GoToNextQuestion();
        }

        
    }

    private void GoToNextQuestion()
    {
        currentQuestion++;

        if (currentQuestion == questions.Count)
        {
            EndPoll();
        }
        else if (currentQuestion >= 0)
        {
            currentAnswers += ",";
            StartCoroutine(PutNewQuestionIn());
        }
    }

    private void RevealCorrectAnswers(QuestionObject qo)
    {
        qo.RevealAnswers();
    }

    private void EndPoll()
    {
        GetComponent<DataManager>().SaveData(quiz);
        // Write the new answer string to the file
        //db.SaveData(dataFileName, currentAnswers);

        // Thank you everyone
        StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        float elapsedTime = 0;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - Vector3.right * distance;

        while (elapsedTime < revealTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / revealTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        //GM.currentGameLine = 7;
        //GM.GivePrize(true);

        yield return new WaitForSeconds(questionDelayTime);

        //GM.GoToScene(afterWinScene);

        yield break;
    }

    private IEnumerator PutNewQuestionIn()
    {
        float elapsedTime = 0;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - Vector3.right * distance;

        while (elapsedTime < revealTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, elapsedTime / revealTime));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        yield break;
    }

    private void OptionIsClicked(CustomButton option)
    {
        foreach(QuizOption o in quiz.questions[currentQuestion].options)
        {
            var oo = option.GetComponent<OptionObject>();
            if (o.id == oo.GetID())
            {
                oo.Pick();
            }
        }

        //StartCoroutine(RevealResutls(option));
    }
}
