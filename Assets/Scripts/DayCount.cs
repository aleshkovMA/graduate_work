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
        dayField.text = "Месяц: " + DataHolder.days;
        StartCoroutine(Wait());
    }

    private void EventRandomizer()
    {
        SpeedController speed;
        rndE = new System.Random();
        int events = rndE.Next(1, 10);
        switch (events)
        {
            case 3://Случайное событие 1
                //Сотрудник заболел выплатить рандомное кол-во денег
                event13.SetActive(true);
                speed = speedButton.GetComponent<SpeedController>();
                speed.PauseGame();
                int cost = rndE.Next(15, 30);
                event13Text.text = "Один из рабочих заболел, вы любезно выплачиваете ему больничные в размере " + cost + " тысяч рублей";
                DataHolder.money = DataHolder.money - cost;
                moneyField.text = "Доступные средства (тыс.руб.): " + DataHolder.money;
                break;
            case 6://Случайное событие 2
                //Станок сломался 3 варианта действий: снести, ремонт своими силамиб заплатить за моментальный ремонт
                choiseEvent.SetActive(true);
                speed = speedButton.GetComponent<SpeedController>();
                speed.PauseGame();
                stanki = GameObject.FindGameObjectsWithTag("Stanok");
                stanokNum = rndE.Next(stanki.Length);
                pay = rndE.Next(767 / 2, (stanki[stanokNum].GetComponent<EconomyScript>().modernCost + 767) / 2);
                choiseEventText.text = "Неопытный рабочий, не соблюдая технику безопасности при работе с оборудованием, сломал один из ваших станков. Вы можете отремантировать станок силами собственного отдела тех.обслуживания и в течении 3ех месяцев, по мимо этого есть возможность вызвать сотрудников регионального сервиса что обойдется в " + pay + " тысяч рублейю Так же есть возможность избавиться от сломанного оборудования.";
                Debug.Log("Поздравляю, ты наролял нерабочий ивент");
                break;
            case 9://Случайное событие 3
                   //Государство выделило грант на развитие + деньги
                event13.SetActive(true);
                speed = speedButton.GetComponent<SpeedController>();
                speed.PauseGame();
                int prib = rndE.Next(100, 500);
                event13Text.text = "В ходе государственной программы по поддержке и развитию тяжело промышленных предприятий, вашему заводу выделили грант в размере " + prib + " тысяч рублей";
                DataHolder.money = DataHolder.money + prib;
                moneyField.text = "Доступные средства (тыс.руб.): " + DataHolder.money;
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
            moneyField.text = "Доступные средства (тыс.руб.): " + DataHolder.money;
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
