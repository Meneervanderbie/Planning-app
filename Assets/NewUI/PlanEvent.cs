using UnityEngine;
using System.Collections;

public class PlanEvent : MonoBehaviour {

    public GameObject planMenu;
    public GameObject planOnce;
    public GameObject schedule;

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
        gameObject.SetActive(false);
    }

    // Schedule button clicked
    public void ScheduleClicked()
    {
        schedule.SetActive(true);
        gameObject.SetActive(false);
    }

}
