﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnCardDragged : MonoBehaviour
{
    Vector3 position;
    bool isDragging, isFinish;
    public bool isFocusing;
    public SceneManager sceneManager;
    public GameObject leftText, rightText;

    void Start(){
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
        entry_Drag.eventID = EventTriggerType.PointerDown;
        entry_Drag.callback.AddListener((data) => { OnDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_Drag);
        EventTrigger.Entry entry_Up = new EventTrigger.Entry();
        entry_Up.eventID = EventTriggerType.PointerUp;
        entry_Up.callback.AddListener((data) => { OnUp((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_Up);
        
        position = this.GetComponent<RectTransform>().localPosition;
        isFinish = false;
    }

    void Update(){
        if(isDragging){
            RectTransform rect = this.GetComponent<RectTransform>();

            float x = transform.position.x - Input.mousePosition.x;
            float y = transform.position.y - Input.mousePosition.y;

            if(-rect.offsetMax.y < 430 && y < 0)y = 0;
            if(-rect.offsetMax.y > 600 && y > 0)y = 0;

            transform.position += new Vector3(x * -0.05f, y * -0.01f, 0);
            //Debug.Log(transform.position.x);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (float)((550 - transform.position.x)*0.022)));

            if(transform.position.x > 700) rightText.SetActive(true);
            else rightText.SetActive(false);

            if(transform.position.x < 400) leftText.SetActive(true);
            else leftText.SetActive(false);
        }
    }

    void OnDown(PointerEventData data){
        if(!isFinish && isFocusing)isDragging = true;
    }

    void OnUp(PointerEventData data){
        rightText.SetActive(false);
        leftText.SetActive(false);
        if(transform.position.x > 800)
        {
            isFinish = true;
            isDragging = false;
            StartCoroutine(Finish(true));
        }
        if(transform.position.x < 300)
        {
            isFinish = true;
            isDragging = false;
            StartCoroutine(Finish(false));
        }
        if(!isFinish){
            isDragging = false;
            transform.localPosition = position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    IEnumerator Finish(bool isGoRight){
        if(isGoRight)
        {
            while(transform.position.x < 2000){
                transform.position += new Vector3(2000 * Time.deltaTime , -1000 * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, (float)((550 - transform.position.x)*0.022)));
                yield return new WaitForEndOfFrame();
            }
            sceneManager.SetUI(false); 
            transform.localPosition = position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isFinish = false;
        }
        else
        {
            while(transform.position.x > -1000){
                transform.position -= new Vector3(2000 * Time.deltaTime , 1000 * Time.deltaTime, 0);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, (float)((550 - transform.position.x)*0.022)));
                yield return new WaitForEndOfFrame();
            }
            sceneManager.SetUI(true); 
            transform.localPosition = position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            isFinish = false;
        }
    }

    public void SetFocus(bool focus){
        isFocusing = focus;
    }
    public bool GetFocus(){
        return isFocusing;
    }
}
