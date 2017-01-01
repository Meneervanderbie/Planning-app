using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskPopup: MonoBehaviour {

    public UI ui;
    public ButtonList buttonList;

    public InputField taskName;
    public Dropdown textCategory;
    public InputField taskText;

    public Toggle today;
    public Toggle asap;

    public GameObject dayPicker;
    public GameObject dayPickerRow;
    public Button dayPickerDay;
    public Text currentMonthText;
    public DateTime current;

    public Dropdown minutes;
    public Dropdown hours;

    public Dropdown repeat;

    public Task currentTask;

    public void OnEnable()
    {
        current = DateTime.Now;
        int month = current.Month;
        int year = current.Year;
        GetMonth(year, month);
        repeat.value = currentTask.repeat;

        minutes.ClearOptions();
        hours.ClearOptions();
        List<string> minuteList = new List<string>();
        for (int i = 0; i < 60; i++)
        {
            minuteList.Add(i.ToString());
        }
        minutes.AddOptions(minuteList);
        minutes.value = currentTask.minutes;

        today.isOn = currentTask.datePlanned == DateTime.Today;

        List<string> hourList = new List<string>();
        for (int i = 0; i < 11; i++)
        {
            hourList.Add(i.ToString());
        }
        hours.AddOptions(hourList);
        hours.value = currentTask.hours;
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
        DateTime prevMonth = current.AddMonths(-1);
        int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);

        foreach (Transform child in dayPicker.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newBar = Instantiate(dayPickerRow, dayPicker.transform) as GameObject;
        int dayCounter = 1;
        for (int i = daysInPrevMonth - firstDayNum + 2; i <= daysInPrevMonth; i++)   // Waarom + 2? Find out!
        {
            Button newButton = Instantiate(dayPickerDay, newBar.transform) as Button;
            newButton.GetComponentInChildren<Text>().text = i.ToString();
            newButton.image.color = Color.grey;
        }
        for (int i = firstDayNum; i <= 7; i++)
        {
            Button newButton = Instantiate(dayPickerDay, newBar.transform) as Button;
            newButton.GetComponentInChildren<Text>().text = dayCounter.ToString();
            int temp = dayCounter;
            newButton.onClick.AddListener(() => DayClicked(temp));
            if(currentTask.taskDeadline == new DateTime(year, month, dayCounter))
            {
                newButton.image.color = Color.yellow;
            }
            else
            {
                newButton.image.color = Color.white;
            }
            dayCounter++;
            daysInMonth--;
        }
        while (daysInMonth >= 7)
        {
            newBar = Instantiate(dayPickerRow, dayPicker.transform) as GameObject;
            for (int i = 0; i < 7; i++)
            {
                Button newButton = Instantiate(dayPickerDay, newBar.transform) as Button;
                newButton.GetComponentInChildren<Text>().text = dayCounter.ToString();
                int temp = dayCounter;
                newButton.onClick.AddListener(() => DayClicked(temp));
                if (currentTask.taskDeadline == new DateTime(year, month, dayCounter))
                {
                    newButton.image.color = Color.yellow;
                }
                else
                {
                    newButton.image.color = Color.white;
                }
                dayCounter++;
                daysInMonth--;
            }
        }
        int lastSeven = 0;
        newBar = Instantiate(dayPickerRow, dayPicker.transform) as GameObject;
        for (int i = daysInMonth; i > 0; i--)
        {
            Button newButton = Instantiate(dayPickerDay, newBar.transform) as Button;
            newButton.GetComponentInChildren<Text>().text = dayCounter.ToString();
            int temp = dayCounter;
            newButton.onClick.AddListener(() => DayClicked(temp));
            if (currentTask.taskDeadline == new DateTime(year, month, dayCounter))
            {
                newButton.image.color = Color.yellow;
            }
            else
            {
                newButton.image.color = Color.white;
            }
            dayCounter++;
            lastSeven++;
        }
        for (int i = 1; i <= 7 - lastSeven; i++)
        {
            Button newButton = Instantiate(dayPickerDay, newBar.transform) as Button;
            newButton.GetComponentInChildren<Text>().text = i.ToString();
            newButton.image.color = Color.grey;
        }
    }

    public void SetToday()
    {
        if (today.isOn)
        {
            currentTask.datePlanned = DateTime.Today;
            currentTask.taskDeadline = DateTime.Today;
            asap.isOn = false;
        }
        else
        {
            currentTask.datePlanned = DateTime.MinValue;
            currentTask.taskDeadline = DateTime.MinValue;
        }
    }

    public void SetASAP()
    {
        if (asap.isOn)
        {
            currentTask.taskDeadline = DateTime.MinValue;
            currentTask.datePlanned = DateTime.MinValue;
            today.isOn = false;
        }
    }

    public void prevMonth()
    {
        current = current.AddMonths(-1);
        GetMonth(current.Year, current.Month);
    }

    public void nextMonth()
    {
        current = current.AddMonths(1);
        GetMonth(current.Year, current.Month);
    }

    public void DayClicked(int day)
    {
        currentTask.taskDeadline = new DateTime(current.Year, current.Month, day);
        GetMonth(current.Year, current.Month);
    }

    public void SetTask(Task task)
    {
        currentTask = task;
        taskName.text = currentTask.GetName();
        textCategory.value = currentTask.taskCategory;
        taskText.text = currentTask.taskText;
    }

    public void SaveTask()
    {
        currentTask.SetName(taskName.text);
        currentTask.taskCategory = textCategory.value;
        currentTask.taskText = taskText.text;
        currentTask.minutes = minutes.value;
        currentTask.hours = hours.value;
        currentTask.repeat = repeat.value;
        buttonList.UpdateButtonList();
        gameObject.SetActive(false);
    }

    public void DeleteTask()
    {
        if(repeat.value == 0)
        {
            ui.DeleteTask(currentTask);
            gameObject.SetActive(false);
        }
        else if(repeat.value == 1) // daily
        {
            currentTask.taskDeadline = currentTask.taskDeadline.AddDays(1);
            currentTask.datePlanned = currentTask.taskDeadline.AddDays(1);
            gameObject.SetActive(false);
        }
        else if (repeat.value == 2) // weekly
        {
            currentTask.taskDeadline = currentTask.taskDeadline.AddDays(7);
            currentTask.datePlanned = currentTask.taskDeadline.AddDays(7);
            gameObject.SetActive(false);
        }
        else if (repeat.value == 3) // daily
        {
            currentTask.taskDeadline = currentTask.taskDeadline.AddMonths(1);
            currentTask.datePlanned = currentTask.datePlanned.AddMonths(1);
            gameObject.SetActive(false);
        }

    }
}
