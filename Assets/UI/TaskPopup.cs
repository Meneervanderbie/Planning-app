using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class TaskPopup: MonoBehaviour {

    public UI ui;

    public InputField taskName;
    public Dropdown textCategory;
    public InputField taskText;

    public GameObject dayPicker;
    public GameObject dayPickerRow;
    public Button dayPickerDay;
    public Text currentMonthText;
    public DateTime current;

    public Task currentTask;

    public void OnEnable()
    {
        current = DateTime.Now;
        int month = current.Month;
        int year = current.Year;
        GetMonth(year, month);
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
        // Waarom + 2? Find out!
        for (int i = daysInPrevMonth - firstDayNum + 2; i <= daysInPrevMonth; i++)
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
        ClearAllFields();
        ui.UpdateButtonList();
        gameObject.SetActive(false);
    }

    public void DeleteTask()
    {
        ui.DeleteTask(currentTask);
        ClearAllFields();
        gameObject.SetActive(false);
    }

    public void ClearAllFields()
    {
        taskName.text = "";
    }
}
