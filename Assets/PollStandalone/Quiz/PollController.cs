using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PollController : MonoBehaviour
{
    //public float revealTime;
    //public float questionDelayTime;
    //public float questionChangeTime;

    ////Dictionary<int, int> pollData;

    //public float distance;

    //public GameObject startScreen;
    //public GameObject endScreen;

    //public GameObject questionPrefab;

    //private List<GameObject> questions;

    //private int currentQuestion;

    //private string currentAnswers;

    //private WebGM GM;
    //private PollDB db;

    //private PollData poll;

    //void Start()
    //{
    //    StartCoroutine(GetPollData());
    //}

    //private IEnumerator GetPollData()
    //{
    //    db = GetComponent<PollDB>();
    //    db.GetData();

    //    //startScreen.GetComponentInChildren<Text>().text = db.baseURL;

    //    while (poll == null)
    //    {
    //        yield return new WaitForEndOfFrame();
    //    }

    //    Debug.Log("Loaded!");
    //    CreateQuestions();

    //    yield return new WaitForSeconds(0.5f);

    //    currentQuestion = -1;
    //    GoToNextQuestion();

    //    yield break;
    //}

    //public void SetPoll(PollData _poll)
    //{
    //    poll = _poll;

    //    startScreen.GetComponentInChildren<Text>().text = "Готово!";
    //}

    //private void CreateQuestions()
    //{
    //    questions = new List<GameObject>();

    //    foreach (PollQuestion q in poll.questions)
    //    {
    //        GameObject newQuestion = Instantiate(questionPrefab);
    //        newQuestion.transform.parent = transform;
    //        newQuestion.transform.position += new Vector3(distance * (questions.Count + 1), 0, 0);

    //        questions.Add(newQuestion);
    //        newQuestion.GetComponent<QuestionObject>().SetUp(q);
    //    }

    //    endScreen.transform.position = new Vector3(distance * (questions.Count + 1), 0, 0);
    //}

    //private void Update()
    //{
    //    if (Input.anyKeyDown/* && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended*/)
    //    {
    //        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
    //        if (hit && hit.collider != null)
    //        {
    //            CustomButton cb = hit.collider.GetComponent<CustomButton>() ?? null;
    //            if (cb != null && cb.value == "0")
    //            {
    //                OptionIsClicked(cb);
    //            }
    //        }
    //    }
    //}

    //private void GoToNextQuestion()
    //{
    //    currentQuestion++;

    //    if (currentQuestion == questions.Count)
    //    {
    //        EndPoll();
    //    }
    //    else if (currentQuestion >= 0)
    //    {
    //        currentAnswers += ",";
    //        StartCoroutine(PutNewQuestionIn());
    //    }
    //}

    //private void EndPoll()
    //{
    //    GetComponent<PollDB>().SaveData(poll);
    //    // Write the new answer string to the file
    //    //db.SaveData(dataFileName, currentAnswers);

    //    // Thank you everyone
    //    StartCoroutine(EndAnimation());
    //}

    //private IEnumerator EndAnimation()
    //{
    //    float elapsedTime = 0;

    //    Vector3 startPosition = transform.position;
    //    Vector3 endPosition = startPosition - Vector3.right * distance;

    //    while (elapsedTime < revealTime)
    //    {
    //        transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / revealTime);

    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    transform.position = endPosition;

    //    //GM.currentGameLine = 7;
    //    //GM.GivePrize(true);

    //    yield return new WaitForSeconds(questionDelayTime);

    //    //GM.GoToScene(afterWinScene);

    //    yield break;
    //}

    //private IEnumerator PutNewQuestionIn()
    //{
    //    float elapsedTime = 0;

    //    Vector3 startPosition = transform.position;
    //    Vector3 endPosition = startPosition - Vector3.right * distance;

    //    while (elapsedTime < revealTime)
    //    {
    //        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, elapsedTime / revealTime));

    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    transform.position = endPosition;

    //    yield break;
    //}

    //private void OptionIsClicked(CustomButton option)
    //{
    //    foreach(PollOption o in poll.questions[currentQuestion].options)
    //    {
    //        if(o.id == option.GetComponent<OptionObject>().GetID())
    //        {
    //            o.Vote();
    //        }
    //    }
    //    StartCoroutine(RevealResutls(option));
    //}

    //private IEnumerator RevealResutls(CustomButton clicked)
    //{
    //    CustomButton[] options = clicked.transform.parent.GetComponentsInChildren<CustomButton>();

    //    int c = 0;

    //    foreach (CustomButton cb in options)
    //    {
    //        cb.value = c.ToString();
    //        c++;
    //    }

    //    currentAnswers += clicked.value;

    //    List<CustomButton> orderedOptions = new List<CustomButton>();

    //    // Calculate the offset of option buttons so we can order them and calculate their target positions
    //    int numberOfOptions = options.Length;

    //    float optionsBlockHeight = -options[options.Length - 1].transform.localPosition.y;

    //    float optionsBlockOffset = optionsBlockHeight / (numberOfOptions - 1);

    //    int totalPolled = questions[currentQuestion].GetComponent<QuestionObject>().GetTotalVotes() + 1;

    //    Dictionary<int, int> questionData = GetVotesByQuestion(questions[currentQuestion].GetComponent<QuestionObject>().GetQuestion());

    //    questionData[Array.IndexOf(options, clicked)] += 1;

    //    foreach (KeyValuePair<int, int> item in questionData.OrderByDescending(key => key.Value))
    //    {
    //        orderedOptions.Add(options[item.Key]);
    //    }

    //    int i = 0;

    //    foreach (CustomButton o in orderedOptions)
    //    {
    //        float percentage = Mathf.Floor(100 * questionData[Convert.ToInt32(o.GetComponent<CustomButton>().value)] / totalPolled);

    //        if (orderedOptions.IndexOf(o) == 0 && percentage != 100 * questionData[Convert.ToInt32(o.GetComponent<CustomButton>().value)] / totalPolled)
    //        {
    //            percentage += 1;
    //        }

    //        StartCoroutine(RevealAnswerPercentage(o, percentage, /*transform.position.y*/ - optionsBlockOffset * i));
    //        i++;
    //    }

    //    yield return new WaitForSeconds(questionDelayTime);

    //    GoToNextQuestion();

    //    yield break;
    //}

    //private Dictionary<int, int> GetVotesByQuestion(PollQuestion question)
    //{
    //    Dictionary<int, int> votes = new Dictionary<int, int>();

    //    int i = 0;

    //    foreach(PollOption o in question.options)
    //    {
    //        votes.Add(i, o.votes);
    //        i++;
    //    }

    //    return votes;
    //}

    //private IEnumerator RevealAnswerPercentage(CustomButton answer, float percentage, float endY)
    //{
    //    answer.GetComponent<Collider2D>().enabled = false;

    //    SpriteRenderer percentageBar = answer.GetComponentInChildren<SpriteRenderer>();
    //    Text percentageText = answer.GetComponentInChildren<Text>();

    //    percentageText.text = percentage + "%";

    //    float elapsedTime = 0;

    //    Color tempColor;

    //    Vector3 startPosition = answer.transform.localPosition;
    //    Vector3 endPosition = new Vector3(startPosition.x, endY, startPosition.z);

    //    Vector3 startScale = percentageBar.transform.localScale;
    //    Vector3 endScale = new Vector3(startScale.x * percentage / 100, startScale.y, startScale.z);

    //    while (elapsedTime < revealTime)
    //    {
    //        percentageBar.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / revealTime);
    //        answer.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / revealTime);

    //        tempColor = percentageText.color;
    //        tempColor.a = Mathf.Lerp(0, 1, elapsedTime / revealTime);
    //        percentageText.color = tempColor;

    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    answer.transform.localPosition = endPosition;
    //    percentageBar.transform.localScale = endScale;

    //    tempColor = percentageText.color;
    //    tempColor.a = 1;
    //    percentageText.color = tempColor;

    //    yield break;
    //}
}
