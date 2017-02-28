using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DayPlanner : MonoBehaviour {

    public MenuManager mm;
    public GameObject schedule;

    // Should not be a button? 
    public Button dayButton;
    public GameObject dayObject;
    public GameObject eventPrefab;

    // GameObject list? 

    public InputField eventName;
    public InputField eventDescription;

    // location?

    public InputField objective1;
    public InputField objective2;
    public InputField objective3;
    public InputField objective4;

    public Dropdown startHours;
    public Dropdown startMinutes;
    public Dropdown endHours;
    public Dropdown endMinutes;

    public WeekDay weekDay;
    public string dayName;

    public Event currentlyEditing;

    void OnEnable()
    {
        startHours.ClearOptions();
        startMinutes.ClearOptions();
        endHours.ClearOptions();
        endMinutes.ClearOptions();

        List<string> hourlist = new List<string>();
        for(int i = 7; i < 25; i++)
        {
            hourlist.Add(i.ToString());
        }
        startHours.AddOptions(hourlist);
        endHours.AddOptions(hourlist);

        List<string> minuteList = new List<string>();
        for(int i = 0; i < 60; i++)
        {
            if(i < 10)
            {
                minuteList.Add("0" + i.ToString());
            }
            else
            {
                minuteList.Add(i.ToString());
            }
        }
        startMinutes.AddOptions(minuteList);
        endMinutes.AddOptions(minuteList);
    }

    public void Initialize(WeekDay day)
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("EventItem");
        foreach(GameObject obj in allObjects)
        {
            if(obj != dayButton)
            {
                Destroy(obj);
            }
        }

        weekDay = day;
        dayButton.GetComponentInChildren<Text>().text = weekDay.dayName;
        // Instantiate text elements in the right place
        DateTime currentTime = new DateTime(1, 1, 1, 7, 0, 0);
        List<Event> eventList = day.weekDay;

        foreach (Event ev in eventList)
        {
            if (currentTime < ev.startTime)
            {
                GameObject tempText = Instantiate(eventPrefab, dayObject.transform) as GameObject;
                Text[] tempElements = tempText.GetComponentsInChildren<Text>();
                tempElements[0].text = String.Format("{0:HH:mm}", currentTime);
                tempElements[1].text = "Not planned";
                tempElements[2].text = String.Format("{0:HH:mm}", ev.startTime); 
            }
            GameObject eventText = Instantiate(eventPrefab, dayObject.transform) as GameObject;
            Event tempEvent = ev;
            eventText.GetComponent<Button>().onClick.AddListener(() => EditEvent(tempEvent));
            Text[] textElements = eventText.GetComponentsInChildren<Text>();
            textElements[0].text = String.Format("{0:HH:mm}", ev.startTime);
            textElements[1].text = ev.eventName;
            textElements[2].text = String.Format("{0:HH:mm}", ev.endTime);
            currentTime = ev.endTime;
        }
        if (currentTime < new DateTime(1, 1, 2, 0, 0, 0))
        {
            GameObject tempText = Instantiate(eventPrefab, dayObject.transform) as GameObject;
            Text[] tempElements = tempText.GetComponentsInChildren<Text>();
            tempElements[0].text = String.Format("{0:HH:mm}", currentTime);
            tempElements[1].text = "Not planned";
            tempElements[2].text = String.Format("{0:HH:mm}", new DateTime(1, 1, 2, 0, 0, 0));
        }
        ClearFields();
    }

    public void Back()
    {
        schedule.SetActive(true);
        gameObject.SetActive(false);
    }

    public void DeleteEvent()
    {
        if(currentlyEditing != null)
        {
            weekDay.DeleteEvent(currentlyEditing);
            mm.SaveTaskList();
            Initialize(weekDay);
        }
    }

    public void ClearFields()
    {
        currentlyEditing = null;
        eventName.text = "";
        eventDescription.text = "";
        objective1.text = "";
        objective2.text = "";
        objective3.text = "";
        objective4.text = "";
        startHours.value = 0;
        startMinutes.value = 0;
        endHours.value = 0;
        endMinutes.value = 0;
    }

    public void EditEvent(Event ev)
    {
        currentlyEditing = ev;
        eventName.text = ev.eventName;
        eventDescription.text = ev.eventDescription;
        objective1.text = ev.objective1;
        objective2.text = ev.objective2;
        objective3.text = ev.objective3;
        objective4.text = ev.objective4;
        startHours.value = ev.startTime.Hour + 7;
        startMinutes.value = ev.startTime.Minute;
        endHours.value = ev.endTime.Hour + 7;
        endMinutes.value = ev.endTime.Minute;
    }

    // Submit button makes new event and saves to the tasklist.
    public void Submit()
    {
        string newName = eventName.text;
        string newDescription = eventDescription.text;

        // Add location!
        string newLocation = "check?";

        int hourValue = 0;
        int.TryParse(startHours.options[startHours.value].text, out hourValue);
        int minuteValue = 0;
        int.TryParse(startMinutes.options[startMinutes.value].text, out minuteValue);
        DateTime startTime = new DateTime(1, 1, 1, hourValue, minuteValue, 0);

        hourValue = 0;
        int.TryParse(startHours.options[endHours.value].text, out hourValue);
        minuteValue = 0;
        int.TryParse(startMinutes.options[endMinutes.value].text, out minuteValue);
        DateTime endTime = new DateTime(1, 1, 1, hourValue, minuteValue, 0);

        // Check first if time is still available? 
        // Also check if timespan is positive!

        // Make new Event
        Event newEvent = new Event(newName, newDescription, objective1.text, objective2.text, objective3.text, objective4.text, newLocation, startTime, endTime);
        // Add event to eventlist
        weekDay.AddEvent(newEvent);
        weekDay.SortList();

        // Save Tasklist
        mm.SaveTaskList();

        // Update the screen after that to replace it. 
        Initialize(weekDay);
    }
}
