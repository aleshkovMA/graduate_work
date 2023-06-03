using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DayCount : MonoBehaviour
{
    private System.Random rndE;
    [SerializeField] private GameObject brokenStanok;
    [SerializeField] private GameObject emblem;
    [SerializeField] private Text dayField;
    [SerializeField] private GameObject event13;
    [SerializeField] private GameObject choiseEvent;
    [SerializeField] private Text event13Text;
    [SerializeField] private Text choiseEventText;
    [SerializeField] private Text moneyField;
    [SerializeField] private GameObject speedButton;
    private GameObject[] stanki;
    private List<int> activeStanki;
    private int stanokNum;
    private int pay;
    private void Start()
    {
        Count();
    }
    private void Count()
    {
        DataHolder.days ++;
        dayField.text = "�����: " + DataHolder.days;
        StartCoroutine(Wait());
    }

    private void EventRandomizer()
    {
        SpeedController speed;
        rndE = new System.Random();
        int events = rndE.Next(1, 10);
        switch (events)
        {
            case 3://��������� ������� 1
                //��������� ������� ��������� ��������� ���-�� �����
                event13.SetActive(true);
                speed = speedButton.GetComponent<SpeedController>();
                speed.PauseGame();
                int cost = rndE.Next(15, 30);
                event13Text.text = "���� �� ������� �������, �� ������� ������������ ��� ���������� � ������� " + cost + " ����� ������";
                DataHolder.money = DataHolder.money - cost;
                moneyField.text = "��������� �������� (���.���.): " + DataHolder.money;
                break;
            case 6://��������� ������� 2
                //������ �������� 3 �������� ��������: ������, ������ ������ ������� ��������� �� ������������ ������
                choiseEvent.SetActive(true);
                speed = speedButton.GetComponent<SpeedController>();
                speed.PauseGame();
                stanki = GameObject.FindGameObjectsWithTag("Stanok");
                stanokNum = rndE.Next(stanki.Length);
                pay = rndE.Next(767 / 2, (stanki[stanokNum].GetComponent<EconomyScript>().modernCost + 767) / 2);
                choiseEventText.text = "��������� �������, �� �������� ������� ������������ ��� ������ � �������������, ������ ���� �� ����� �������. �� ������ ��������������� ������ ������ ������������ ������ ���.������������ � � ������� 3�� �������, �� ���� ����� ���� ����������� ������� ����������� ������������� ������� ��� ��������� � " + pay + " ����� ������� ��� �� ���� ����������� ���������� �� ���������� ������������.";
                Debug.Log("����������, �� ������� ��������� �����");
                break;
            case 9://��������� ������� 3
                   //����������� �������� ����� �� �������� + ������
                event13.SetActive(true);
                speed = speedButton.GetComponent<SpeedController>();
                speed.PauseGame();
                int prib = rndE.Next(100, 500);
                event13Text.text = "� ���� ��������������� ��������� �� ��������� � �������� ������ ������������ �����������, ������ ������ �������� ����� � ������� " + prib + " ����� ������";
                DataHolder.money = DataHolder.money + prib;
                moneyField.text = "��������� �������� (���.���.): " + DataHolder.money;
                break;
        }
    }

    public void destroyStanok()
    {
        stanki[stanokNum].SetActive(false);
        stanki[stanokNum].GetComponent<EconomyScript>().currentLvl = 0;
        stanki[stanokNum].GetComponent<EconomyScript>().rashod = 150;
        stanki[stanokNum].GetComponent<EconomyScript>().modernCost = 153;
    }
    public void payForRepair()
    {
        if(DataHolder.money >= pay)
        {
            DataHolder.money = DataHolder.money - pay;
            moneyField.text = "��������� �������� (���.���.): " + DataHolder.money;
        }
    }
    public void selfRepair()
    {
        brokenStanok.transform.position = stanki[stanokNum].GetComponent<Transform>().position;
        emblem.transform.position = stanki[stanokNum].GetComponent<Transform>().position;
        brokenStanok.SetActive(true);
        emblem.SetActive(true);
        stanki[stanokNum].SetActive(false);
        StartCoroutine(broken());
    }
    private IEnumerator broken()
    {
        yield return new WaitForSeconds(18f);
        stanki[stanokNum].SetActive(true);
        brokenStanok.SetActive(false);
        emblem.SetActive(false);
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
        Count();
        EventRandomizer();
    }
}
