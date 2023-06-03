using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class ScoreTable : MonoBehaviour
{
    public SqliteConnection dbconnection;
    private string path;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<ScoreEntries> ScoreEntriesList;
    private List<Transform> ScoreEntriesTransformList;
    private void Awake()
    {
        entryContainer = transform.Find("TemplatesContainer");
        entryTemplate = entryContainer.Find("Template");

        entryTemplate.gameObject.SetActive(false);
        ScoreEntriesList = new List<ScoreEntries>();

        path = Application.dataPath + "/StreamingAssets/mydb.db";
        dbconnection = new SqliteConnection("Data Source = " + path);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconnection;
            cmd.CommandText = "SELECT Login, LivedTIme, COllectedMoney FROM Users";
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    ScoreEntriesList.Add(new ScoreEntries { name = reader.GetValue(0).ToString(), days = Convert.ToInt32(reader.GetValue(1).ToString()), moneys = Convert.ToInt32(reader.GetValue(2).ToString()) });
                }
                catch
                {
                    ScoreEntriesList.Add(new ScoreEntries { name = reader.GetValue(0).ToString(), days = 1, moneys = 1000 });
                }
            }
        }

        for (int i = 0; i < ScoreEntriesList.Count; i++)
        {
            for (int j = i + 1; j < ScoreEntriesList.Count; j++)
            {
                if(ScoreEntriesList[j].moneys > ScoreEntriesList[i].moneys)
                {
                    ScoreEntries tmp = ScoreEntriesList[i];
                    ScoreEntriesList[i] = ScoreEntriesList[j];
                    ScoreEntriesList[j] = tmp;
                }
            }
        }
        int userPlace = 0;
        foreach(ScoreEntries entry in ScoreEntriesList)
        {
            entry.uPlace = userPlace + 1;
            userPlace++;
        }

        ScoreEntriesTransformList = new List<Transform>();
        int count = 0;
        foreach(ScoreEntries entry in ScoreEntriesList)
        {
            if (count < 10)
            {
                CreateEntryTransform(entry, entryContainer, ScoreEntriesTransformList);
                count++;
            }
        }
        foreach (ScoreEntries entry in ScoreEntriesList)
        {
            if(entry.name == DataHolder.CurrentUser)
            {
                Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, -30f * 13f);
                entryTransform.Find("Place").GetComponent<Text>().text = entry.uPlace.ToString();
                entryTransform.Find("UserName").GetComponent<Text>().text = entry.name;
                entryTransform.Find("DaysS").GetComponent<Text>().text = entry.days.ToString();
                entryTransform.Find("MoneysS").GetComponent<Text>().text = entry.moneys.ToString();
                entryTransform.gameObject.SetActive(true);
            }
        }
    }
    private void CreateEntryTransform(ScoreEntries scoreEntries, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);


        int rank = scoreEntries.uPlace;
        int moneys = scoreEntries.moneys;
        int days = scoreEntries.days;
        string name = scoreEntries.name;
        entryTransform.Find("Place").GetComponent<Text>().text = rank.ToString();
        entryTransform.Find("UserName").GetComponent<Text>().text = name;
        entryTransform.Find("DaysS").GetComponent<Text>().text = days.ToString();
        entryTransform.Find("MoneysS").GetComponent<Text>().text = moneys.ToString();
        entryTransform.gameObject.SetActive(true);
        transformList.Add(entryTransform);
    }
    private class ScoreEntries
    {
        public int uPlace;
        public int days;
        public int moneys;
        public string name;
    }
}
