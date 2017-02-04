using UnityEngine;
using UnityEngine.UI;
using System;

public class StartMenu : MonoBehaviour {

    public MenuManager mm;

    public GameObject startTask;
    public GameObject planTask;

    // Maybe display current day as well? 
    public Text currentPoints;

    // Display next task, with time to go to that task
    public Text nextTask;

    // Display current daily points (and date?)
    public void Initialize()
    {
        currentPoints.text = "Points today: " + mm.taskList.highscores[0].score;
        mm.SaveTaskList();
    }

    // Start task clicked
    public void StartTask()
    {
        startTask.SetActive(true);
        gameObject.SetActive(false);
    }

    // Plan task clicked
    public void PlanTask()
    {
        planTask.SetActive(true);
        gameObject.SetActive(false);
    }

    // Display next planned task
    public void NextTask()
    {
        // Can only do this once we have a fixed schedule?
    }
}
