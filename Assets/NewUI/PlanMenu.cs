using UnityEngine;
using System.Collections;

public class PlanMenu : MonoBehaviour {

    public GameObject startMenu;
    public GameObject planTask;
    public GameObject planEvent;

    public void Back()
    {
        startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PlanTask()
    {
        planTask.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PlanEvent()
    {
        planEvent.SetActive(true);
        gameObject.SetActive(false);
    }

}
