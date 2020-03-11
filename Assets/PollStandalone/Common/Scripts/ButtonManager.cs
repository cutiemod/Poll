using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    protected WebGM GM;

    protected virtual void Awake()
    {
        GM = FindObjectOfType<WebGM>();
    }

    protected void Update()
    {
        if (Input.anyKeyDown/* && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended*/)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit && hit.collider != null) //something is hit
            {
                CustomButton button = hit.collider.GetComponent<CustomButton>();
                if (button != null && button.isPressable) //if it's a button - act
                {
                    Act(button.value, GM);
                }
            }
        }
    }

    protected virtual void Act(string value, WebGM GM)
    {

    }

}
