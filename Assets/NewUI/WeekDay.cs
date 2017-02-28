using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class WeekDay {

    [XmlArray("DayPlanning")]
    [XmlArrayItem("Event")]
    public List<Event> weekDay;

    [XmlAttribute("DayName")]
    public string dayName;

    public WeekDay()
    {

    }

    public WeekDay(string day)
    {
        dayName = day;
        weekDay = new List<Event>();
    }

    public void AddEvent(Event toAdd)
    {
        // Probably sort by startTime order?
        weekDay.Add(toAdd);
    }

    public void DeleteEvent(Event toDelete)
    {
        weekDay.Remove(toDelete);
    }

    public void SortList()
    {
        List<Event> newList = new List<Event>();
        while(weekDay.Count > 0)
        {
            Event toAdd = weekDay[0];
            for (int i = 0; i < weekDay.Count; i++)
            {
                if(weekDay[i].startTime < toAdd.startTime)
                {
                    toAdd = weekDay[i];
                }
            }
            newList.Add(toAdd);
            weekDay.Remove(toAdd);
        }
        weekDay = newList;
    }

}
