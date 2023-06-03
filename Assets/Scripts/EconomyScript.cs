using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;


public class EconomyScript : MonoBehaviour
{
    public int producedDetails;
    public int brakO;
    public System.Random details;
    public System.Random brak;
    public int dohod;
    public int rashod = 150;
    public int detailCost = 6;
    public int maxBrak = 10;
    public int minDet = 50;
    public int maxDet = 60;
    public int modernCost = 153;
    public int currentLvl = 0;
    [SerializeField]private Text moneyField;
    private void OnEnable()
    {
        switch (currentLvl)
        {
            case 0:
                rashod = 150;
                minDet = 50;
                maxDet = 60;
                modernCost = 153;
                break;
            case 1:
                rashod = 165;
                minDet = 55;
                maxDet = 65;
                modernCost = 230;
                break;
            case 2:
                rashod = 180;
                minDet = 60;
                maxDet = 70;
                modernCost = 307;
                break;
            case 3:
                rashod = 195;
                minDet = 65;
                maxDet = 75;
                break;
        }
        StartCoroutine(Wait());
    }
    private void Count()
    {
        //if (DataHolder.money >= rashod)
        //{
        details = new System.Random();
        brak = new System.Random();
        producedDetails = details.Next(minDet, maxDet);
        brakO = brak.Next(maxBrak);
        producedDetails = producedDetails - brakO;
        dohod = producedDetails * detailCost;
            DataHolder.money = DataHolder.money - rashod + dohod;
            moneyField.text = "Доступные средства (тыс.руб.): " + DataHolder.money;
        //}
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
        Count();
    }
}
