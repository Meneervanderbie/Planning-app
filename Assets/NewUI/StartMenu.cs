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
    public Text currentTaskText;
    public Text nextTaskText;
    public Text nextTaskTime;

    public WeekDay currentDay;
    public Event currentEvent;
    public Event upcomingEvent;

    // Display current daily points (and date?)
    public void Initialize()
    {
        currentPoints.text = "Points today: " + mm.taskList.highscores[0].score;
        if(mm.taskList.agenda.Count > 0){
            // step 0: find the correct day
            foreach (WeekDay wd in mm.taskList.agenda)
            {
                if (wd.dayDate == DateTime.Today)
                {
                    currentDay = wd;
                    break;
                }
            }
            // step 1: find the current task/event and display
            foreach (Event ev in currentDay.weekDay)
            {
                if (ev.startTime <= DateTime.Now && ev.endTime > DateTime.Now)
                {
                    currentTaskText.text = "Current: " + ev.eventName;
                    currentEvent = ev;
                    break;
                }
                else
                {
                    currentTaskText.text = "No current event.";
                    currentEvent = null;
                }
            }
            // step 2: find the next task/event by starttime and display
            DateTime nextTime = DateTime.Today.AddDays(1);
            foreach (Event ev in currentDay.weekDay)
            {
                if (ev.startTime > DateTime.Now && ev.startTime > nextTime)
                {
                    nextTaskText.text = "Next: " + ev.eventName;
                    upcomingEvent = ev;
                    break;
                }
                else
                {
                    nextTaskText.text = "No upcoming events today.";
                    upcomingEvent = null;
                }
            }
        }

        mm.SaveTaskList();
    }

    void Update()
    {
        if (upcomingEvent != null)
        {
            nextTaskTime.text = "Next event starts in: " + (int)(upcomingEvent.startTime - DateTime.Now).TotalHours + " Hours and " + (int)(upcomingEvent.startTime - DateTime.Now).Minutes + "Minutes.";
        }
        else
        {
            nextTaskTime.text = "No events today.";
        }
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
    //public void NextTask()
    //{
    //    // Can only do this once we have a fixed schedule?
    //}
}
