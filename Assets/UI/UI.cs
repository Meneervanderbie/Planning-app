using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public TaskList taskList;
    public IO io;
    public string dataPath = "/Data/Tasklist.xml";

    public Button taskPrefab;
    public Dropdown sortTasks;
    public GameObject buttonList;

    public TaskPopup taskPopup;
    public Dropdown popupCategories;
    public bool popupActive;

    public DateTime monthViewing;
    public Text currentMonthText;
    public GameObject monthPanel;
    public GameObject monthRow;
    public Button monthDay;

    void Start()
    {
        io = new IO();
        taskPopup.ui = this;
        taskList = io.Load(Application.dataPath + dataPath);
        if (taskList.categoryList.Count == 0)
        {
            TempCategoryMaker();
        }

        monthViewing = DateTime.Now;
        GetMonth(monthViewing.Year, monthViewing.Month);

        popupCategories.ClearOptions(); 
        popupCategories.AddOptions(taskList.GetCategoryStrings());

        MakeButtonList();
    }

    public void AddTask()
    {
        // make new constructor with all values?
        Task newTask = new Task("New task");
        taskList.AddTask(newTask);
        SpawnButton(newTask);
        taskPopup.SetTask(newTask);
        taskPopup.gameObject.SetActive(true);
    }

    public void DeleteTask(Task task)
    {
        taskList.DeleteTask(task);
        MakeButtonList();
    }

    public void SaveTasks()
    {
        io.Save(Application.dataPath + "/Data/Tasklist.xml", taskList);
        MakeButtonList();
        UpdateCalendar();
    }

    public void MakeButtonList()
    {
        Button[] oldList = buttonList.GetComponentsInChildren<Button>();
        RectTransform rt = buttonList.GetComponentInChildren<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 45 * taskList.taskList.Count);
        for(int i = 0; i < oldList.Length; i++)
        {
            Destroy(oldList[i].gameObject);
        }
        foreach (Task task in taskList.Sort(sortTasks.value))
        {
            if (task.datePlanned == default(DateTime))
            {
                SpawnButton(task);
            }
        }
    }

    public void DisplayDay(int year, int month, int day)
    {
        Button[] oldList = buttonList.GetComponentsInChildren<Button>();
        RectTransform rt = buttonList.GetComponentInChildren<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 45 * taskList.GetDay(year,month,day).Count);
        for (int i = 0; i < oldList.Length; i++)
        {
            Destroy(oldList[i].gameObject);
        }
        
        foreach (Task task in taskList.GetDay(year, month, day))
        {
            SpawnButton(task);
        }
    }

    public void SpawnButton(Task task)
    {
        Button toAdd = Instantiate(taskPrefab);
        toAdd.GetComponent<TaskButton>().SetTask(task);
        toAdd.transform.SetParent(buttonList.transform, false);
        toAdd.GetComponentInChildren<Text>().text = task.GetName();
        toAdd.onClick.AddListener(() => ClickButton(task));
    }

    public void ClickButton(Task task)
    {
        taskPopup.SetTask(task);
        taskPopup.gameObject.SetActive(true);
    }

    public void UpdateButtonList()
    {
        foreach(Button button in buttonList.GetComponentsInChildren<Button>())
        {
            button.GetComponent<TaskButton>().SetName();
        }
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
        DateTime prevMonth = monthViewing.AddMonths(-1);
        int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);

        foreach (Transform child in monthPanel.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newBar = Instantiate(monthRow, monthPanel.transform) as GameObject;
        int dayCounter = 1;
        // Waarom + 2? Find out!
        for (int i = daysInPrevMonth - firstDayNum + 2; i <= daysInPrevMonth; i++)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            newButton.GetComponent<CalendarDay>().SetDate(prevMonth.Year,prevMonth.Month,i);
            int tempyear = prevMonth.Year;
            int tempMonth = prevMonth.Month;
            int tempDay = i;
            newButton.onClick.AddListener(() => DisplayDay(tempyear, tempMonth, tempDay));
            newButton.image.color = Color.grey;
        }
        for (int i = firstDayNum; i <= 7; i++)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            newButton.GetComponent<CalendarDay>().SetDate(year,month,dayCounter);
            int tempyear = year;
            int tempMonth = month;
            int tempDay = dayCounter;
            newButton.onClick.AddListener(() => DisplayDay(tempyear, tempMonth, tempDay));
            dayCounter++;
            daysInMonth--;
        }
        while (daysInMonth >= 7)
        {
            newBar = Instantiate(monthRow, monthPanel.transform) as GameObject;
            for (int i = 0; i < 7; i++)
            {
                Button newButton = Instantiate(monthDay, newBar.transform) as Button;
                newButton.GetComponent<CalendarDay>().SetDate(year,month,dayCounter);
                int tempyear = year;
                int tempMonth = month;
                int tempDay = dayCounter;
                newButton.onClick.AddListener(() => DisplayDay(tempyear, tempMonth, tempDay));
                dayCounter++;
                daysInMonth--;
            }
        }
        int lastSeven = 0;
        newBar = Instantiate(monthRow, monthPanel.transform) as GameObject;
        for (int i = daysInMonth; i > 0; i--)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            newButton.GetComponent<CalendarDay>().SetDate(year,month,dayCounter);
            int tempyear = year;
            int tempMonth = month;
            int tempDay = dayCounter;
            newButton.onClick.AddListener(() => DisplayDay(tempyear, tempMonth, tempDay));
            dayCounter++;
            lastSeven++;
        }
        for (int i = 1; i <= 7 - lastSeven; i++)
        {
            Button newButton = Instantiate(monthDay, newBar.transform) as Button;
            //newButton.GetComponentInChildren<Text>().text = i.ToString();
            newButton.GetComponent<CalendarDay>().SetDate(prevMonth.AddMonths(2).Year, prevMonth.AddMonths(2).Month,i);
            int tempyear = prevMonth.AddMonths(2).Year;
            int tempMonth = prevMonth.AddMonths(2).Month;
            int tempDay = i;
            newButton.onClick.AddListener(() => DisplayDay(tempyear, tempMonth, tempDay));
            newButton.image.color = Color.grey;
        }

        // get contents from calenderday scripts after filling with dayPlanned contents
        getPlannedTasks();
        UpdateCalendar();
    }

    public void getPlannedTasks()
    {
        foreach(Task task in taskList.GetList())
        {
            if(task.datePlanned != DateTime.MinValue)
            {
                DateTime planned = task.datePlanned;
                if(Math.Abs((task.datePlanned - monthViewing).TotalDays) < 32)
                {
                    foreach(CalendarDay cDay in monthPanel.GetComponentsInChildren<CalendarDay>())
                    {
                        if(cDay.day == planned.Day && cDay.month == planned.Month && cDay.year == planned.Year)
                        {
                            cDay.AddTask(task);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void UpdateCalendar()
    {
        foreach (CalendarDay cDay in monthPanel.GetComponentsInChildren<CalendarDay>())
        {
            List<string> textList = cDay.GetTasks();

            string toPrint = "";

            foreach(string str in textList)
            {
                toPrint += str + " ";
            }

            cDay.gameObject.GetComponentInChildren<Text>().text = toPrint;
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

    public void TempCategoryMaker()
    {
        taskList.categoryList = new List<Category>();
        taskList.categoryList.Add(new Category("Werk", 0));
        taskList.categoryList.Add(new Category("Studie", 1));
        taskList.categoryList.Add(new Category("Huishouden", 2));
        taskList.categoryList.Add(new Category("Persoonlijk", 3));
        taskList.categoryList.Add(new Category("Planner", 4));
    }

}
