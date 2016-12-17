using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    public DateTime monthViewing;
    public Text currentMonthText;
    public GameObject monthPanel;
    public GameObject monthRow;
    public Button monthDay;

    public TaskList taskList;

    public ButtonList buttonList;

    public void StartUp(TaskList tl)
    {
        taskList = tl;
        monthViewing = DateTime.Now;
        GetMonth(monthViewing.Year, monthViewing.Month);
    }

    public void GetMonth(int yr, int mnth)
    {
        // Dirty! Clean me up please!
        int month = mnth;
        int year = yr;
        DateTime firstDay = new DateTime(year, month, 1);
        currentMonthText.text = firstDay.ToString("MMMM yyyy");
        int firstDayNum = (int)firstDay.DayOfWeek;

        int daysInMonth = DateTime.DaysInMonth(year, month);
        DateTime prevMonth = monthViewing.AddMonths(-1);
        int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);

        foreach (Transform child in monthPanel.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newBar = Instantiate(monthRow, monthPanel.transform) as GameObject;
        int dayCounter = 1;
        // Waarom + 2? Find out!
        for (int i = daysInPrevMonth - firstDayNum + 2; i <= daysInPrevMonth; i++)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            newButton.GetComponent<CalendarDay>().SetDate(prevMonth.Year, prevMonth.Month, i);
            List<Task> tempList = getPlannedTasks(newButton.GetComponent<CalendarDay>());
            newButton.onClick.AddListener(() => buttonList.MakeButtonList(tempList, true));
            newButton.image.color = Color.grey;
        }
        for (int i = firstDayNum; i <= 7; i++)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            newButton.GetComponent<CalendarDay>().SetDate(year, month, dayCounter);
            List<Task> tempList = getPlannedTasks(newButton.GetComponent<CalendarDay>());
            newButton.onClick.AddListener(() => buttonList.MakeButtonList(tempList, true));
            dayCounter++;
            daysInMonth--;
        }
        while (daysInMonth >= 7)
        {
            newBar = Instantiate(monthRow, monthPanel.transform) as GameObject;
            for (int i = 0; i < 7; i++)
            {
                Button newButton = Instantiate(monthDay, newBar.transform) as Button;
                newButton.GetComponent<CalendarDay>().SetDate(year, month, dayCounter);
                List<Task> tempList = getPlannedTasks(newButton.GetComponent<CalendarDay>());
                newButton.onClick.AddListener(() => buttonList.MakeButtonList(tempList, true));
                dayCounter++;
                daysInMonth--;
            }
        }
        int lastSeven = 0;
        newBar = Instantiate(monthRow, monthPanel.transform) as GameObject;
        for (int i = daysInMonth; i > 0; i--)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            newButton.GetComponent<CalendarDay>().SetDate(year, month, dayCounter);
            List<Task> tempList = getPlannedTasks(newButton.GetComponent<CalendarDay>());
            newButton.onClick.AddListener(() => buttonList.MakeButtonList(tempList, true));
            dayCounter++;
            lastSeven++;
        }
        for (int i = 1; i <= 7 - lastSeven; i++)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            //newButton.GetComponentInChildren<Text>().text = i.ToString();
            newButton.GetComponent<CalendarDay>().SetDate(prevMonth.AddMonths(2).Year, prevMonth.AddMonths(2).Month, i);
            List<Task> tempList = getPlannedTasks(newButton.GetComponent<CalendarDay>());
            newButton.onClick.AddListener(() => buttonList.MakeButtonList(tempList, true));
            newButton.image.color = Color.grey;
        }

        // get contents from calenderday scripts after filling with dayPlanned contents
        //getPlannedTasks();
        UpdateCalendar();
    }

    public List<Task> getPlannedTasks(CalendarDay cDay)
    {
        foreach (Task task in taskList.GetList())
        {
            DateTime planned = task.datePlanned;
            if (cDay.day == planned.Day && cDay.month == planned.Month && cDay.year == planned.Year)
            {
                cDay.AddTask(task);
            }
            //if (task.datePlanned != DateTime.MinValue)
            //{
            //    DateTime planned = task.datePlanned;
            //    if (Math.Abs((task.datePlanned - monthViewing).TotalDays) < 32)
            //    {
            //        foreach (CalendarDay cDay in monthPanel.GetComponentsInChildren<CalendarDay>())
            //        {
            //            if (cDay.day == planned.Day && cDay.month == planned.Month && cDay.year == planned.Year)
            //            {
            //                cDay.AddTask(task);
            //                break;
            //            }
            //        }
            //    }
            //}
        }
        return cDay.assignedTasks;
    }

    public void UpdateCalendar()
    {
        foreach (CalendarDay cDay in monthPanel.GetComponentsInChildren<CalendarDay>())
        {
            List<string> textList = cDay.GetTasks();

            string toPrint = "";

            foreach (string str in textList)
            {
                toPrint += str + " ";
            }

            cDay.gameObject.GetComponentInChildren<Text>().text = toPrint;
        }
    }

    public void prevMonth()
    {
        monthViewing = monthViewing.AddMonths(-1);
        GetMonth(monthViewing.Year, monthViewing.Month);
    }

    public void nextMonth()
    {
        monthViewing = monthViewing.AddMonths(1);
        GetMonth(monthViewing.Year, monthViewing.Month);
    }
}
