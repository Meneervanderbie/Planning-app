using UnityEngine;
using System.Collections;

public class PlanEvent : MonoBehaviour {

    public GameObject planMenu;
    public GameObject planOnce;
    public GameObject schedule;
    public Calendar calendar;

	// Back button clicked
    public void Back()
    {
        planMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    // Once button clicked
    public void OnceClicked()
    {
        planOnce.SetActive(true);
        planOnce.GetComponent<PlanOnce>().Initialize();
        gameObject.SetActive(false);
    }

    // Schedule button clicked
    public void ScheduleClicked()
    {
        schedule.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CalendarClicked()
    {
        calendar.gameObject.SetActive(true);
        calendar.Initialize();
        gameObject.SetActive(false);
    }

}
