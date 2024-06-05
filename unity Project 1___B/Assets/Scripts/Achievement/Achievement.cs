using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Achievement
{

    public string name;
    public string description;
    public bool isUnlocked;
    public int currenProgress;
    public int goal;

    public Achievement(string name, string description, int goal)
    {
        this.name = name;
        this.description = description;
        this.isUnlocked = false;
        this.currenProgress = 0;
        this.goal = goal;
    }

    public void AddProgress(int amount)
    {
        if(isUnlocked)
        {
            currenProgress += amount;
            if(currenProgress >= goal)
            {
                isUnlocked = true;
                OnAchievementUnlocked();
            }
        }
    }

    protected virtual void OnAchievementUnlocked()
    {
        Debug.Log($"업적 달성 : {name}");
    }

}
