using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modern : MonoBehaviour
{
    [SerializeField] private Text name;
    [SerializeField] private Text dohod;
    [SerializeField] private Text rashod;
    [SerializeField] private Text modernCost;
    [SerializeField] private Text curLvl;
    public void Upgrade()
    {
        GameObject stanok = GameObject.Find(name.text);
        if (stanok.GetComponent<EconomyScript>().currentLvl < 3 && DataHolder.money >= stanok.GetComponent<EconomyScript>().modernCost)
        {
            DataHolder.money = DataHolder.money - stanok.GetComponent<EconomyScript>().modernCost;
            stanok.GetComponent<EconomyScript>().currentLvl++;
            stanok.GetComponent<EconomyScript>().dohod = stanok.GetComponent<EconomyScript>().dohod * 2;
            stanok.GetComponent<EconomyScript>().rashod = stanok.GetComponent<EconomyScript>().rashod * 2;
            stanok.GetComponent<EconomyScript>().modernCost = stanok.GetComponent<EconomyScript>().modernCost + 100;
            dohod.text = "Прибыль (тыс.руб.): " + stanok.GetComponent<EconomyScript>().dohod;
            rashod.text = "Расход (тыс.руб.): " + stanok.GetComponent<EconomyScript>().rashod;
            curLvl.text = "Текущий уровень: " + stanok.GetComponent<EconomyScript>().currentLvl;
            modernCost.text = "Стоимость модернизации (тыс.руб.): " + stanok.GetComponent<EconomyScript>().modernCost;
        }
        if(stanok.GetComponent<EconomyScript>().currentLvl == 3)
        {
            modernCost.text = "Стоимость модернизации: MAX LVL";
        }
    }
}
