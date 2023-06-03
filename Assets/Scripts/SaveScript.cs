using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Mono.Data.Sqlite;
using System.Data;

public class SaveScript : MonoBehaviour
{
    public SqliteConnection dbconnection;
    private string pathToDB;

    [SerializeField] private Text days;
    [SerializeField] private Text moneys;
    [SerializeField] List<GameObject> Stanki;
    [SerializeField] private Text ErrorText;
    [SerializeField] private GameObject ErrorGameObject;
    const string fileName = "Save.json";
    string path;
    SavedData save = new SavedData();

    private void Start()
    {

        path = Application.persistentDataPath + "/" + fileName;
    }
    public void Save()
    {
        save.savedMoneys = DataHolder.money;
        save.savedDays = DataHolder.days;
        foreach (GameObject stanokOnScene in Stanki)
        {
            save.stanki.Add(new StankiClass()
            {
                SavedStanokName = stanokOnScene.name,
                SavedLvl = stanokOnScene.GetComponent<EconomyScript>().currentLvl,
                SavedDohod = stanokOnScene.GetComponent<EconomyScript>().dohod,
                SavedRashod = stanokOnScene.GetComponent<EconomyScript>().rashod,
                _isActive = stanokOnScene.activeSelf
            });
        }

        var json = JsonUtility.ToJson(save);
        using (var writer = new StreamWriter(path))
        {
            writer.WriteLine(json);
        }
        pathToDB = Application.dataPath + "/StreamingAssets/mydb.db";
        dbconnection = new SqliteConnection("Data Source = " + pathToDB);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconnection;
            cmd.CommandText = "SELECT COUNT(Login) FROM Users WHERE Login = @Login";
            SqliteParameter log = new SqliteParameter("@Login", DataHolder.CurrentUser);
            cmd.Parameters.Add(log);
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            if (r > 0)
            {
                string saveabelObj = json.ToString();
                SqliteCommand ins = new SqliteCommand();
                ins.Connection = dbconnection;
                ins.CommandText = "UPDATE Users SET SaveFile = @Save, LivedTIme = @Time, COllectedMoney =  @Money WHERE Login = @Login";
                ins.Parameters.Add(log);
                SqliteParameter save = new SqliteParameter("@Save", saveabelObj);
                ins.Parameters.Add(save);
                SqliteParameter time = new SqliteParameter("@Time", DataHolder.days);
                ins.Parameters.Add(time);
                SqliteParameter money = new SqliteParameter("@Money", DataHolder.money);
                ins.Parameters.Add(money);
                ins.ExecuteNonQuery();
            }
        }
    }
    public void Load()
    {
        string json = "";
        //Устанавливаем соединение с БД
        pathToDB = Application.dataPath + "/StreamingAssets/mydb.db";
        dbconnection = new SqliteConnection("Data Source = " + pathToDB);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconnection;
            //Запрос на наличие пользователя
            cmd.CommandText = "SELECT COUNT(Login) FROM Users WHERE Login = @Login";
            SqliteParameter log = new SqliteParameter("@Login", DataHolder.CurrentUser);
            cmd.Parameters.Add(log);
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            if (r > 0)
            {
                SqliteCommand loadFromDB = new SqliteCommand();
                loadFromDB.Connection = dbconnection;
                //Запрос на загрузку сохранения из БД
                loadFromDB.CommandText = "SELECT SaveFile FROM Users WHERE Login = @Login";
                loadFromDB.Parameters.Add(log);
                SqliteDataReader reader = loadFromDB.ExecuteReader();
                if (reader.HasRows)
                {
                    //Запись загруженных данных в файл на диске
                    using (var writer = new StreamWriter(path))
                    {
                        string result = "";
                        while (reader.Read())
                        {
                            result += reader.GetValue(0);
                        }
                        writer.WriteLine(result);
                    }
                    //Загрузка данных из файла в симулятор
                    using (var reader1 = new StreamReader(path))
                    {
                        string line;
                        while ((line = reader1.ReadLine()) != null)
                        {
                            json += line;
                        }
                    }
                    SavedData load = JsonUtility.FromJson<SavedData>(json);
                    DataHolder.money = load.savedMoneys;
                    DataHolder.days = load.savedDays;
                    days.text = "Месяц: " + load.savedDays;
                    moneys.text = "Доступные средства (тыс.руб.): " + load.savedMoneys;
                    foreach (GameObject stanokOnScene in Stanki)
                    {
                        foreach (StankiClass loadedStanok in load.stanki)
                        {
                            if (stanokOnScene.name == loadedStanok.SavedStanokName)
                            {
                                stanokOnScene.SetActive(loadedStanok._isActive);
                                stanokOnScene.GetComponent<EconomyScript>().currentLvl = loadedStanok.SavedLvl;
                                stanokOnScene.GetComponent<EconomyScript>().dohod = loadedStanok.SavedDohod;
                                stanokOnScene.GetComponent<EconomyScript>().rashod = loadedStanok.SavedRashod;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    StartCoroutine(Error(ErrorGameObject, ErrorText, "У пользователя нет сохраненных данных"));
                }
            }
        }
    }
    public void StartGame( string ScineName)
    {
        SceneManager.LoadScene(ScineName);
        DataHolder.days = 0;
        DataHolder.money = 100;
    }
    public void Exit()
    {
        Application.Quit();
    }
    private IEnumerator Error(GameObject O, Text T, string message)
    {
        O.SetActive(true);
        T.text = message;
        yield return new WaitForSeconds(3f);
        O.SetActive(false);
    }
}
