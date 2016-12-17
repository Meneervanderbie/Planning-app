using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ButtonList : MonoBehaviour {

    public Button taskPrefab;
    public Dropdown sortTasks;
    public GameObject buttonList;

    public enum sortBy { Deadline, DateAdded };

    // This should only be accessible from UI. ClickButton needs it now. 
    public TaskPopup taskPopup;

    public void MakeButtonList(List<Task> addList, bool planned)
    {
        Button[] oldList = buttonList.GetComponentsInChildren<Button>();
        RectTransform rt = buttonList.GetComponentInChildren<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 45 * addList.Count);
        for (int i = 0; i < oldList.Length; i++)
        {
            Destroy(oldList[i].gameObject);
        }
        foreach (Task task in Sort(sortTasks.value, addList))
        {
            if (planned)
            {
                SpawnButton(task);
            }
            else if (task.datePlanned == default(DateTime))
            {
                SpawnButton(task);
            }
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
        foreach (Button button in buttonList.GetComponentsInChildren<Button>())
        {
            button.GetComponent<TaskButton>().SetName();
        }
    }

    //// Calendar picks a day to view with this method
    //public void DisplayDay()
    //{
    //    Button[] oldList = buttonList.GetComponentsInChildren<Button>();
    //    RectTransform rt = buttonList.GetComponentInChildren<RectTransform>();
    //    rt.sizeDelta = new Vector2(rt.sizeDelta.x, 45 * taskList.GetDay(year, month, day).Count);
    //    for (int i = 0; i < oldList.Length; i++)
    //    {
    //        Destroy(oldList[i].gameObject);
    //    }

    //    foreach (Task task in taskList.GetDay(year, month, day))
    //    {
    //        SpawnButton(task);
    //    }
    //}

    public List<Task> Sort(int value, List<Task> taskList)
    {
        List<Task> sortedList = taskList;
        bool sorted = false;

        sortBy typeSort = (sortBy)value;

        while (!sorted)
        {
            bool switched = false;
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                // TODO: make this switch pretty ;)
                switch (typeSort)
                {
                    case sortBy.Deadline:
                        if (sortedList[i].taskDeadline > sortedList[i + 1].taskDeadline)
                        {
                            Task tempTask = sortedList[i];
                            sortedList[i] = sortedList[i + 1];
                            sortedList[i + 1] = tempTask;
                            switched = true;
                        }
                        break;
                    case sortBy.DateAdded:
                        if (sortedList[i].dateAdded > sortedList[i + 1].dateAdded)
                        {
                            Task tempTask = sortedList[i];
                            sortedList[i] = sortedList[i + 1];
                            sortedList[i + 1] = tempTask;
                            switched = true;
                        }
                        break;
                }
            }
            if (!switched)
            {
                sorted = true;
            }
        }


        return sortedList;
    }

}
