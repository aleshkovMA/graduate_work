using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AuthScript : MonoBehaviour
{
    public SqliteConnection dbconnection;
    private string path;
    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private GameObject Error;
    [SerializeField] private Text ErrorText;
    public void Authorise()
    {
        if (login.text != "" && password.text != "")
        {
            path = Application.dataPath + "/StreamingAssets/mydb.db";
            dbconnection = new SqliteConnection("Data Source = " + path);
            dbconnection.Open();
            if (dbconnection.State == ConnectionState.Open)
            {
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = dbconnection;
                cmd.CommandText = "SELECT COUNT(Login) FROM Users WHERE Login = @Login AND Password = @Password";
                SqliteParameter log = new SqliteParameter("@Login", login.text);
                cmd.Parameters.Add(log);
                SqliteParameter pas = new SqliteParameter("@Password", password.text);
                cmd.Parameters.Add(pas);
                int r = Convert.ToInt32(cmd.ExecuteScalar());
                Debug.Log(r);
                if (r != 0)
                {
                    SceneManager.LoadScene("SampleScene");
                    DataHolder.CurrentUser = login.text;
                }
                else
                {
                    StartCoroutine(ErrorE(Error, ErrorText, "Введены неверные данные"));
                }
            }
            else
            {
                StartCoroutine(ErrorE(Error, ErrorText, "Ошибка соединения"));
            }
        }
        else
        {
            StartCoroutine(ErrorE(Error, ErrorText, "Заполнены не все поля"));
        }
    }

    private IEnumerator ErrorE(GameObject O, Text T, string message)
    {
        O.SetActive(true);
        T.text = message;
        yield return new WaitForSeconds(3f);
        O.SetActive(false);
    }



}
