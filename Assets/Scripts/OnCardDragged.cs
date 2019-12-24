using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnCardDragged : MonoBehaviour
{
    Vector3 position;
    bool isDragging;

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
    }

    void Update(){
        if(isDragging){
            RectTransform rect = this.GetComponent<RectTransform>();

            float x = transform.position.x - Input.mousePosition.x;
            float y = transform.position.y - Input.mousePosition.y;

            if(-rect.offsetMax.y < 430 && y < 0)y = 0;
            if(-rect.offsetMax.y > 600 && y > 0)y = 0;

            transform.position += new Vector3(x * -0.05f, y * -0.01f, 0);
            Debug.Log(transform.position.x);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (float)((440 - transform.position.x)*0.022) + 2.2f));
        }
    }

    void OnDown(PointerEventData data){
        isDragging = true;
    }

    void OnUp(PointerEventData data){
        isDragging = false;
        transform.localPosition = position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
