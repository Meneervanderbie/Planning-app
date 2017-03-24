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

    [XmlAttribute("DayDate")]
    public DateTime dayDate;

    public WeekDay()
    {

    }

    public WeekDay(string day)
    {
        dayName = day;
        weekDay = new List<Event>();
    }

    public WeekDay(List<Event> wd, string dName, DateTime date)
    {
        weekDay = wd;
        dayName = dName;
        dayDate = date;
    }

    public void ChangeDate()
    {
        foreach(Event ev in weekDay)
        {
            DateTime newStart = new DateTime(dayDate.Year, dayDate.Month, dayDate.Day, ev.startTime.Hour, ev.startTime.Minute, ev.startTime.Second);
            ev.startTime = newStart;
            DateTime newEnd = new DateTime(dayDate.Year, dayDate.Month, dayDate.Day, ev.endTime.Hour, ev.endTime.Minute, ev.endTime.Second);
            ev.endTime = newEnd;
        }
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
