using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Schedule : MonoBehaviour {

    public MenuManager mm;

    public DayPlanner dayPlanner;
    public GameObject eventPrefab;

    public List<GameObject> days;

    public List<WeekDay> weekList;

	void OnEnable()
    {
        // Load week schedule from IO/tasklist and instantiate all objects to fill the days
        weekList = mm.taskList.GetWeek();

        for(int i = 0; i < weekList.Count; i++)
        {
            DateTime currentTime = new DateTime(1, 1, 1, 7, 0, 0);
            List<Event> eventList = weekList[i].weekDay;
            foreach (Event ev in eventList)
            {
                if (currentTime < ev.startTime)
                {
                    GameObject tempText = Instantiate(eventPrefab, days[i].transform) as GameObject;
                    Text[] tempElements = tempText.GetComponentsInChildren<Text>();
                    tempElements[0].text = String.Format("{0:HH:mm}", currentTime);
                    tempElements[1].text = "Not planned";
                    tempElements[2].text = String.Format("{0:HH:mm}", ev.startTime);
                }
                GameObject eventText = Instantiate(eventPrefab, days[i].transform) as GameObject;
                Text[] textElements = eventText.GetComponentsInChildren<Text>();
                textElements[0].text = String.Format("{0:HH:mm}", ev.startTime);
                textElements[1].text = ev.eventName;
                textElements[2].text = String.Format("{0:HH:mm}", ev.endTime);
                currentTime = ev.endTime;
            }
            if (currentTime < new DateTime(1, 1, 2, 0, 0, 0))
            {
                GameObject tempText = Instantiate(eventPrefab, days[i].transform) as GameObject;
                Text[] tempElements = tempText.GetComponentsInChildren<Text>();
                tempElements[0].text = String.Format("{0:HH:mm}", currentTime);
                tempElements[1].text = "Not planned";
                tempElements[2].text = String.Format("{0:HH:mm}", new DateTime(1, 1, 2, 0, 0, 0));
            }
        }
    }

    public void EditDay(int weekDay)
    {
        dayPlanner.gameObject.SetActive(true);
        dayPlanner.Initialize(weekList[weekDay]);
        gameObject.SetActive(false);
    }

}
