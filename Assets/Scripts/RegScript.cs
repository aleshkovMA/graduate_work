using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;

public class RegScript : MonoBehaviour
{
    public SqliteConnection dbconnection;
    private string path;
    [SerializeField] private InputField login;
    [SerializeField] private InputField email;
    [SerializeField] private InputField password;
    [SerializeField] private GameObject SuccessField;
    [SerializeField] private GameObject AuthPanel;
    [SerializeField] private GameObject RegPanel;
    [SerializeField] private Text Error;
    [SerializeField] private GameObject ErrorO;

    public void Registration()
    {
        if (login.text !="" && password.text != "" && email.text != "")
        {
            path = Application.dataPath + "/StreamingAssets/mydb.db";
            dbconnection = new SqliteConnection("Data Source = " + path);
            dbconnection.Open();
            if (dbconnection.State == ConnectionState.Open)
            {
                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = dbconnection;
                cmd.CommandText = "SELECT COUNT(Login) FROM Users WHERE Login = @Login";
                SqliteParameter log1 = new SqliteParameter("@Login", login.text);
                cmd.Parameters.Add(log1);
                int r = Convert.ToInt32(cmd.ExecuteScalar());
                if (r>0)
                {
                    StartCoroutine(ErrorE(ErrorO, Error, "Пользователь уже существует"));
                }
                else
                {
                    SqliteCommand addU = new SqliteCommand();
                    addU.Connection = dbconnection;
                    addU.CommandText = "INSERT INTO Users (Login, Password, Email) VALUES (@Login, @Password, @Email)";
                    SqliteParameter log = new SqliteParameter("@Login", login.text);
                    addU.Parameters.Add(log);
                    SqliteParameter pas = new SqliteParameter("@Password", password.text);
                    addU.Parameters.Add(pas);
                    SqliteParameter em = new SqliteParameter("@Email", email.text);
                    addU.Parameters.Add(em);
                    addU.ExecuteNonQuery();

                    StartCoroutine(Success());
                }
            }
            else
            {
                StartCoroutine(ErrorE(ErrorO, Error, "Ошибка подключения"));
            }
        }
        else
        {
            StartCoroutine(ErrorE(ErrorO, Error, "Заполнены не все поля"));
        }
    }
    private IEnumerator Success()
    {
        SuccessField.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SuccessField.SetActive(false);
        RegPanel.SetActive(false);
        AuthPanel.SetActive(true);
    }
    private IEnumerator ErrorE(GameObject O, Text T, string message)
    {
        O.SetActive(true);
        T.text = message;
        yield return new WaitForSeconds(3f);
        O.SetActive(false);
    }
    public void ToReg()
    {
        AuthPanel.SetActive(false);
        RegPanel.SetActive(true);
    }
    public void ToAuth()
    {
        AuthPanel.SetActive(true);
        RegPanel.SetActive(false);
        login.text = "";
        password.text = "";
        email.text = "";
    }
}
