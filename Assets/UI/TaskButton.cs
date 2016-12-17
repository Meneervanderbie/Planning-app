using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskButton: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    string buttonText;

    Task task;

    GameObject dragTo;

    public void SetTask(Task tsk)
    {
        task = tsk;
    }

    public void SetName()
    {
        gameObject.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = task.GetName();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //print("dragging");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.pointerEnter.transform.parent.GetComponent<CalendarDay>() != null)
        {
            dragTo = eventData.pointerEnter.transform.parent.gameObject;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(dragTo != null)
        {
            CalendarDay cd = dragTo.GetComponent<CalendarDay>();
            task.datePlanned = new DateTime(cd.year, cd.month, cd.day);
            cd.AddTask(task);

            // redraw tasks?
            //dragTo.GetComponentInChildren<Text>().text = "test";
        }
    }


}
