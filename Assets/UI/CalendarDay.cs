using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CalendarDay : MonoBehaviour {

    public int year;
    public int month;
    public int day;
    public List<Task> assignedTasks;

    public void SetDate(int yr, int mnt, int dy)
    {
        year = yr;
        month = mnt;
        day = dy;

        assignedTasks = new List<Task>();
    }

    public void AddTask(Task toAdd)
    {
        assignedTasks.Add(toAdd);
    }

    public List<string> GetTasks()
    {
        List<string> toReturn = new List<string>();

        toReturn.Add(day.ToString());

        foreach(Task task in assignedTasks)
        {
            toReturn.Add(task.GetName());
        }

        return toReturn;
    }

}
