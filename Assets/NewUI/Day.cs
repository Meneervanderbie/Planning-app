using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Day {

    [XmlElement("Date")]
    public DateTime date;

    [XmlElement("Score")]
    public int score;

    public Day()
    {

    }

    public Day(DateTime day, int sc)
    {
        date = day;
        score = sc; 
    }

}
