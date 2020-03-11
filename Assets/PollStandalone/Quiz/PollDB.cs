using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PollDB : MonoBehaviour
{
    public string baseURL = "http://order.promomat.ru/Poll/";
    public string pollsURL = "pollsGet.php";
    string questionsURL = "questionsGet.php";
    string optionsURL = "optionsGet.php";
    string votesURL = "votesPost.php";

    public PollData poll;
    private int questionCount;

    public void GetData()
    {
        if (Application.absoluteURL != "")
        {
            baseURL = Application.absoluteURL + "PollsSQL/";
        }

        pollsURL = baseURL + pollsURL;
        questionsURL = baseURL + questionsURL;
        optionsURL = baseURL + optionsURL;
        votesURL = baseURL + votesURL;

        StartCoroutine(ReadPolls());
    }

    private IEnumerator ReadPolls()
    {
        yield return new WaitForEndOfFrame();

        WWWForm form = new WWWForm();

        var download = UnityWebRequest.Post(pollsURL, form);

        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {
            string[] result = download.downloadHandler.text.Split("|"[0]);

            //for (int i = 0; i < result.Length - 1; i++)
            //{
                string[] x = result[0].Split(","[0]);
                //Debug.Log(x[0] + ", id: " + x[1]);
            //}
            StartCoroutine(ReadQuestions(x[0], Convert.ToInt32(x[1])));
        }

        yield break;
    }

    private IEnumerator ReadQuestions(string name, int id)
    {
        yield return new WaitForEndOfFrame();

        WWWForm form = new WWWForm();
        form.AddField("poll", id.ToString());

        var download = UnityWebRequest.Post(questionsURL, form);

        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {
            string[] result = download.downloadHandler.text.Split("|"[0]);

            poll = new PollData();

            for (int i = 0; i < result.Length - 1; i++)
            {
                string[] x = result[i].Split(","[0]);
                poll.AddQuestion(x[0], Convert.ToInt32(x[1]));
            }

            StartCoroutine(ReadOptions(id));
        }

        yield break;
    }

    private IEnumerator ReadOptions(int id)
    {
        yield return new WaitForEndOfFrame();

        WWWForm form = new WWWForm();
        form.AddField("poll", id.ToString());

        var download = UnityWebRequest.Post(optionsURL, form);

        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            Debug.Log("Error downloading: " + download.error);
        }
        else
        {
            string[] result = download.downloadHandler.text.Split("|"[0]);

            for (int i = 0; i < result.Length - 1; i++)
            {
                string[] x = result[i].Split(","[0]);
                poll.AddOption(Convert.ToInt32(x[2]), x[0], Convert.ToInt32(x[1]), Convert.ToInt32(x[3]));
            }
        }

        GetComponent<PollController>().SetPoll(poll);
        yield break;
    }

    public void SaveData(PollData _poll)
    {
        foreach(PollQuestion q in _poll.questions)
        {
            foreach(PollOption o in q.options)
            {
                if(o.voted == true)
                {
                    StartCoroutine(PostVote(_poll.id, o.id));
                }
            }
        }
    }

    private IEnumerator PostVote(int poll_id, int option_id)
    {
        yield return new WaitForEndOfFrame();

        WWWForm form = new WWWForm();
        form.AddField("option", option_id.ToString());

        var upload = UnityWebRequest.Post(votesURL, form);

        yield return upload.SendWebRequest();

        if (upload.isNetworkError || upload.isHttpError)
        {
            Debug.Log("Error sending: " + upload.error);
        }
        else
        {
            Debug.Log("Save successful!");
            Debug.Log(upload.downloadHandler.text);

            //for (int i = 0; i < result.Length - 1; i++)
            //{
            //string[] x = result[0].Split(","[0]);
            //Debug.Log(x[0] + ", id: " + x[1]);
            //}
            //StartCoroutine(ReadQuestions(x[0], Convert.ToInt32(x[1])));
        }

        yield break;
    }
}
