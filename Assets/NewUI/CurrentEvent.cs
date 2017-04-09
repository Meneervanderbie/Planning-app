using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentEvent : MonoBehaviour {

    public GameObject startMenu;
    public MenuManager mm;
    public DailyTask dt;
    public CurrentTask ct;

    public WeekDay currentDay;
    public Event currentEvent;
    public Event upcomingEvent;

    public Text currentTaskText;
    public Text nextTaskTime;

    public Button[] daily;
    public Task[] dailyTasks;

    public Button[] points;
    public Task[] pointTasks;

    public Button[] random;
    public Task[] randomTasks;

    public void Back()
    {
        startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        // Fill Texts with current event and time until end of event.
        if (mm.taskList.agenda.Count > 0)
        {
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
                //print(ev.startTime + " " + ev.endTime);
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
                if (ev.startTime > DateTime.Now && ev.startTime < nextTime)
                {
                    //nextTaskText.text = "Next: " + ev.eventName;
                    upcomingEvent = ev;
                    break;
                }
                else
                {
                    //nextTaskText.text = "No upcoming events today.";
                    upcomingEvent = null;
                }
            }
        }

        // Fill Daily tasks
        int dailyFilled = 0;
        dailyTasks = new Task[3];
        for (int i = 0; i < mm.taskList.dailyList.Count; i++)
        {
            if (dailyFilled == 3)
            {
                break;
            }
            if (mm.taskList.dailyList[i].taskDeadline <= DateTime.Today)
            {
                dailyTasks[dailyFilled] = mm.taskList.dailyList[i];
                daily[dailyFilled].GetComponentInChildren<Text>().text = dailyTasks[dailyFilled].GetName();
                daily[dailyFilled].image.color = GetColorFromString(mm.taskList.categoryList[dailyTasks[dailyFilled].taskCategory].categoryColor);
                dailyFilled++;
            }
        }

        // Fill highest points
        pointTasks = new Task[3];
        for (int i = 0; i < mm.taskList.taskList.Count; i++)
        {
            pointTasks[i] = mm.taskList.taskList[i];
            points[i].GetComponentInChildren<Text>().text = pointTasks[i].GetName();
            points[i].image.color = GetColorFromString(mm.taskList.categoryList[pointTasks[i].taskCategory].categoryColor);
            if(i == 2)
            {
                break;
            }
        }

        // Fill random
        randomTasks = new Task[3];
        for (int i = 0; i < 3; i++)
        {
            int rand = UnityEngine.Random.Range(0,mm.taskList.taskList.Count);
            randomTasks[i] = mm.taskList.taskList[rand];
            random[i].GetComponentInChildren<Text>().text = randomTasks[i].GetName();
            random[i].image.color = GetColorFromString(mm.taskList.categoryList[randomTasks[i].taskCategory].categoryColor);
        }

        mm.SaveTaskList();
    }

    public void Update()
    {
        if(currentEvent != null)
        {
            nextTaskTime.text = (currentEvent.endTime - DateTime.Now).ToString();
        }
        else if (upcomingEvent != null)
        {
            nextTaskTime.text = "Next event starts in: " + (int)(upcomingEvent.startTime - DateTime.Now).TotalHours + " Hours and " + (int)(upcomingEvent.startTime - DateTime.Now).Minutes + "Minutes.";
        }
        else
        {
            nextTaskTime.text = "No events today.";
        }
    }

    public void DailyClicked(int num)
    {
        dt.gameObject.SetActive(true);
        dt.Initialize(dailyTasks[num]);
        gameObject.SetActive(false);
    }

    public void PointsClicked(int num)
    {
        ct.gameObject.SetActive(true);
        ct.Initialize(pointTasks[num]);
        gameObject.SetActive(false);
    }

    public void RandomClicked(int num)
    {
        ct.gameObject.SetActive(true);
        ct.Initialize(randomTasks[num]);
        gameObject.SetActive(false);
    }

    public Color GetColorFromString(string colStr)
    {
        int count = 0;
        // get r
        string rStr = "";
        rStr += colStr[count];
        count++;
        while (colStr[count] != ',')
        {
            rStr += colStr[count];
            count++;
        }
        float r;
        float.TryParse(rStr, out r);
        count++;
        // get g
        string gStr = "";
        gStr += colStr[count];
        count++;
        while (colStr[count] != ',')
        {
            gStr += colStr[count];
            count++;
        }
        float g;
        float.TryParse(gStr, out g);
        count++;
        // get b
        string bStr = "";
        bStr += colStr[count];
        count++;
        for (int i = count; i < colStr.Length; i++)
        {
            bStr += colStr[i];
        }
        float b;
        float.TryParse(bStr, out b);

        //print(r + " " + g + " " + b);

        return new Color(r, g, b);
    }
}
