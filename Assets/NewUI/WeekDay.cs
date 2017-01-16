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

}
