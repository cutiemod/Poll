using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GMPanels : MonoBehaviour
{
    public float restartAfter = 2f;
    public float quitAfter = 2f;

    public float buttonGrowthPerSecond = 1f;

    public Text topPanelText;
    public GameObject panelTop;
    public GameObject panelBottom;

    private WebGM GM;

    private float buttonHeldFor = 0;
    private Vector3 defaultButtonScale;

    private void Awake()
    {
        GM = GetComponent<WebGM>();
    }

    protected void Update()
    {
        CheckIfHeldDown();
    }

    protected void CheckIfHeldDown()
    {
        if (Input.anyKey/* && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended*/)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit && hit.collider != null) //something is hit
            {
                //if (hit.collider.gameObject == restartButton) //
                //{
                //    buttonHeldFor += Time.deltaTime;

                //    restartButton.transform.localScale = new Vector3(restartButton.transform.localScale.x + buttonGrowthPerSecond / restartAfter * Time.deltaTime,
                //                                                     restartButton.transform.localScale.y + buttonGrowthPerSecond / restartAfter * Time.deltaTime,
                //                                                     restartButton.transform.localScale.z);

                //    if (buttonHeldFor >= restartAfter)
                //    {
                //        string v = restartButton.GetComponent<CustomButton>().value;
                //        if (v != "")
                //        {
                //            GM.GoToScene(v);
                //        }
                //        else
                //        {
                //            Debug.Log("No parameter set!");
                //        }

                //        restartButton.GetComponent<Collider2D>().enabled = false;
                //        restartButton.transform.localScale = defaultButtonScale;

                //        //buttonHeldFor = 0;
                //    }
                //}
                //else if (hit.collider.gameObject == exitButton)
                //{
                //    buttonHeldFor += Time.deltaTime;

                //    exitButton.transform.localScale = new Vector3(exitButton.transform.localScale.x + buttonGrowthPerSecond / quitAfter * Time.deltaTime,
                //                                                  exitButton.transform.localScale.y + buttonGrowthPerSecond / quitAfter * Time.deltaTime,
                //                                                  exitButton.transform.localScale.z);

                //    if (buttonHeldFor >= quitAfter)
                //    {
                //        GM.GoToScene(exitButtonScene, 1, true);

                //        exitButton.GetComponent<Collider2D>().enabled = false;
                //        exitButton.transform.localScale = defaultButtonScale;

                //        //buttonHeldFor = 0;
                //    }
                //}
                //else if (buttonHeldFor != 0)
                //{
                //    restartButton.transform.localScale = defaultButtonScale;
                //    exitButton.transform.localScale = defaultButtonScale;

                //    //buttonHeldFor = 0;
                //}
            }
        }
        //else if (Input.GetMouseButtonUp(0) && buttonHeldFor > 0)
        //{
        //    restartButton.transform.localScale = defaultButtonScale;
        //    exitButton.transform.localScale = defaultButtonScale;
        //    restartButton.GetComponent<Collider2D>().enabled = true;
        //    exitButton.GetComponent<Collider2D>().enabled = true;

        //    buttonHeldFor = 0;
        //}
    }

    internal void EnablePanels(int count = 0, bool enableFlag = true)
    {
        //EnableExitButton(true); // enable if it's possible

        switch (count)
        {
            case -1:
                panelBottom.SetActive(enableFlag);
                if(enableFlag)
                    panelBottom.GetComponent<Animator>().Play("normal");

                break;
            case 1:
                topPanelText.gameObject.SetActive(enableFlag);
                panelTop.SetActive(enableFlag);
                if (enableFlag)
                {
                    panelTop.GetComponent<Animator>().Play("normal");
                }
                break;
            default:
                topPanelText.gameObject.SetActive(enableFlag);
                panelBottom.SetActive(enableFlag);
                if (enableFlag)
                {
                    panelBottom.GetComponent<Animator>().Play("normal");
                }
                panelTop.SetActive(enableFlag);
                if (enableFlag)
                {
                    panelTop.GetComponent<Animator>().Play("normal");
                }
                break;
        }
    }

    internal void SetText(string text, int size = 0, float time = 0)
    {
        if (Mathf.Approximately(time, 0))
        {
            StopCoroutine(DisplayNewText(text, size, time));
            topPanelText.text = text;
            if (size != 0)
            {
                topPanelText.fontSize = size;
            }
        }
        else
        {
            //if (text == "" || (text != "" && text != topPanelText.text))
            //{
                StopCoroutine(DisplayNewText(text, size, time));
                StartCoroutine(DisplayNewText(text, size, time));
            //}
        }
    }

    private IEnumerator DisplayNewText(string text, int size, float time)
    {
        float alpha = topPanelText.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time * 4)
        {
            Color newColor = new Color(topPanelText.color.r, topPanelText.color.g, topPanelText.color.b, Mathf.Lerp(alpha, 0, t));
            topPanelText.color = newColor;
            yield return null;
        }

        topPanelText.text = text;
        if (size != 0)
        {
            topPanelText.fontSize = size;
        }

        alpha = topPanelText.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time * 1.5f)
        {
            Color newColor = new Color(topPanelText.color.r, topPanelText.color.g, topPanelText.color.b, Mathf.Lerp(alpha, 1, t));
            topPanelText.color = newColor;
            yield return null;
        }
        yield break;
    }

    internal void FadeOutPanels(int f)
    {
        switch (f)
        {
            case 1:
                panelTop.GetComponent<Animator>().SetTrigger("fadeOut");
                SetText("");
                break;
            case -1:
                panelBottom.GetComponent<Animator>().SetTrigger("fadeOut");
                break;
            default:
                panelTop.GetComponent<Animator>().SetTrigger("fadeOut");
                SetText("");
                panelBottom.GetComponent<Animator>().SetTrigger("fadeOut");
                break;
        }
    }

    internal void MoveTopText(float x, float y)
    {
        topPanelText.rectTransform.Translate(new Vector3 (x, y, 0));
    }
}
