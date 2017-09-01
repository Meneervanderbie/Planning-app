using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPlanner : MonoBehaviour {

    public MenuManager mm;
    public Canvas UI;

    public Text header;
    public GameObject dayPlanner;

    public GameObject taskPrefab;
    public GameObject timeSlotPrefab;

    public int numberOfQuarters;

    public void Start() {
        header.text = DateTime.Today.ToString("dddd dd MMMM yyyy");
        List<Task> todayTasks = mm.taskList.GetDay(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        TimeSpan totalTime = new TimeSpan(0, numberOfQuarters * 15, 0);
        float heightLeft = dayPlanner.GetComponent<RectTransform>().rect.height * UI.scaleFactor;
        int listIndex = 0;
        while(todayTasks.Count < 0) {
            //todayTasks[listIndex].
        }
        //TimeSpan restOfDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0).AddDays(1) - DateTime.Now;
       // int quarters = (int) restOfDay.TotalMinutes / 4;
        //for(int i = 0; i < 18; i++) {
        //    GameObject spawnTimeSlot = Instantiate(timeSlotPrefab, dayPlanner.transform);
        //}
    }

}
