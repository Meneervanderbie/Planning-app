﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    public GameObject planEvent;
    public DayPlanner dayPlanner;
    public MenuManager mm;

    public GameObject weekPrefab;
    public Button dayPrefab;

    public GameObject monthPanel;

    public Text calendarMonthName;

    public DateTime monthViewing;
    public enum editState {Edit, Work, Free};
    public editState currentEdit = editState.Edit;

    public void Back()
    {
        planEvent.SetActive(true);
        gameObject.SetActive(false);
    }

    // Initialize the Calendar thing
    public void Initialize()
    {
        monthViewing = DateTime.Now;
        GetMonth(monthViewing.Year, monthViewing.Month);
    }

    public void Edit() {
        currentEdit = editState.Edit;
    }

    public void Work() {
        currentEdit = editState.Work;
    }

    public void Free() {
        currentEdit = editState.Free;
    }

    public void GetMonth(int yr, int mnt)
    {
        int month = mnt;
        int year = yr;
        DateTime firstDay = new DateTime(year, month, 1);
        calendarMonthName.text = firstDay.ToString("MMMM yyyy");
        int firstDayNum = (int)firstDay.DayOfWeek;

        int daysInMonth = DateTime.DaysInMonth(year, month);
        DateTime prevMonth = monthViewing.AddMonths(-1);
        int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);

        foreach (Transform child in monthPanel.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newBar = Instantiate(weekPrefab, monthPanel.transform) as GameObject;
        int dayCounter = 1;
        // Waarom + 2? Find out!
        for (int i = daysInPrevMonth - firstDayNum + 2; i <= daysInPrevMonth; i++)
        {
            Button newButton = Instantiate(dayPrefab, newBar.transform) as Button;
            string buttonText = i.ToString();
            newButton.GetComponentInChildren<Text>().text = buttonText.ToString();
            newButton.image.color = Color.grey;
        }
        for (int i = firstDayNum; i <= 7; i++)
        {
            Button newButton = Instantiate(dayPrefab, newBar.transform) as Button;
            string buttonText = dayCounter.ToString();
            newButton.GetComponentInChildren<Text>().text = buttonText.ToString();
            int dayCount = dayCounter;
            DateTime setDate = new DateTime(year, month, dayCount);
            if (setDate < DateTime.Today)
            {
                newButton.image.color = Color.grey;
            }
            else
            {
                newButton.onClick.AddListener(() => ChooseDay(setDate));
                newButton.image.color = GetDateColor(setDate);
            }
            dayCounter++;
            daysInMonth--;
        }
        while (daysInMonth >= 7)
        {
            newBar = Instantiate(weekPrefab, monthPanel.transform) as GameObject;
            for (int i = 0; i < 7; i++)
            {
                Button newButton = Instantiate(dayPrefab, newBar.transform) as Button;
                string buttonText = dayCounter.ToString();
                newButton.GetComponentInChildren<Text>().text = buttonText.ToString();
                int dayCount = dayCounter;
                DateTime setDate = new DateTime(year, month, dayCount);
                newButton.onClick.AddListener(() => ChooseDay(setDate));
                if (setDate < DateTime.Today)
                {
                    newButton.image.color = Color.grey;
                }
                else
                {
                    newButton.onClick.AddListener(() => ChooseDay(setDate));
                    newButton.image.color = GetDateColor(setDate);
                }
                dayCounter++;
                daysInMonth--;
            }
        }
        int lastSeven = 0;
        newBar = Instantiate(weekPrefab, monthPanel.transform) as GameObject;
        for (int i = daysInMonth; i > 0; i--)
        {
            Button newButton = Instantiate(dayPrefab, newBar.transform) as Button;
            string buttonText = dayCounter.ToString();
            newButton.GetComponentInChildren<Text>().text = buttonText.ToString();
            int dayCount = dayCounter;
            DateTime setDate = new DateTime(year, month, dayCount);
            newButton.onClick.AddListener(() => ChooseDay(setDate));
            if (setDate < DateTime.Today)
            {
                newButton.image.color = Color.grey;
            }
            else
            {
                newButton.onClick.AddListener(() => ChooseDay(setDate));
                newButton.image.color = GetDateColor(setDate);
            }
            dayCounter++;
            lastSeven++;
        }
        for (int i = 1; i <= 7 - lastSeven; i++)
        {
            Button newButton = Instantiate(dayPrefab, newBar.transform) as Button;
            string buttonText = i.ToString();
            newButton.GetComponentInChildren<Text>().text = buttonText.ToString();
            newButton.image.color = Color.grey;
        }
    }

    public Color GetDateColor(DateTime date) {
        return mm.taskList.GetDateColor(date);
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

    public void ChooseDay(DateTime date)
    {
        dayPlanner.gameObject.SetActive(true);
        // check if chosen date already exists in schedule
        if (mm.taskList.agenda == null)
        {
            mm.taskList.agenda = new List<WeekDay>();
        }
        bool dayFound = false;
        DateTime highestFound = DateTime.Today;
        if (mm.taskList.agenda.Count == 0)
        {
            WeekDay tempDay = new WeekDay(mm.taskList.weekList[((int)(highestFound.DayOfWeek) + 6) % 7].weekDay, highestFound.ToString("dd MMMM, yyyy"), highestFound);
            mm.taskList.agenda.Add(tempDay);
            mm.SaveTaskList();
        }
        foreach (WeekDay wd in mm.taskList.agenda)
        {
            if(wd.dayDate == date)
            {
                dayFound = true;
                if(currentEdit == editState.Edit) {
                    dayPlanner.Initialize(wd);
                    gameObject.SetActive(false);
                    break;
                }
                else if(currentEdit == editState.Work) {
                    wd.work = true;
                    Initialize();
                    break;
                }
                else {
                    wd.work = false;
                    Initialize();
                    break;
                }
            }
            else
            {
                if(wd.dayDate > highestFound)
                {
                    highestFound = wd.dayDate;
                }
            }
        }
        if (!dayFound)
        {
            // Start creating new days from planning until you reach the requested date
            while (highestFound != date)
            {
                highestFound = highestFound.AddDays(1);
                WeekDay tempDay = new WeekDay(mm.taskList.weekList[((int)(highestFound.DayOfWeek) + 6) % 7].weekDay, highestFound.ToString("dd MMMM, yyyy"), highestFound);
                mm.taskList.agenda.Add(tempDay);
                mm.SaveTaskList();
                if (highestFound == date)
                {
                    dayPlanner.Initialize(mm.taskList.agenda[mm.taskList.agenda.Count-1]);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
