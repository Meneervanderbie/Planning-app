using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour {

    public GameObject planEvent;

    public GameObject weekPrefab;
    public Button dayPrefab;

    public GameObject monthPanel;

    public Text calendarMonthName;

    public DateTime monthViewing;

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
            DateTime setDate = new DateTime(year, month, dayCounter);
            if(setDate < DateTime.Today)
            {
                newButton.image.color = Color.grey;
            }
            else
            {
                newButton.onClick.AddListener(() => ChooseDay(setDate));
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
                DateTime setDate = new DateTime(year, month, dayCounter);
                newButton.onClick.AddListener(() => ChooseDay(setDate));
                if (setDate < DateTime.Today)
                {
                    newButton.image.color = Color.grey;
                }
                else
                {
                    newButton.onClick.AddListener(() => ChooseDay(setDate));
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
            DateTime setDate = new DateTime(year, month, dayCounter);
            newButton.onClick.AddListener(() => ChooseDay(setDate));
            if (setDate < DateTime.Today)
            {
                newButton.image.color = Color.grey;
            }
            else
            {
                newButton.onClick.AddListener(() => ChooseDay(setDate));
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

    }
}
