using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowStanokInfo : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text name;
    [SerializeField] private Text detali;
    [SerializeField] private Text brak;
    [SerializeField] private Text dohod;
    [SerializeField] private Text rashod;
    [SerializeField] private Text modernCost;
    [SerializeField] private Text curLvl;
    [SerializeField] private GameObject stanok;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (stanok.GetComponent<EconomyScript>().currentLvl < 3)
            {
                panel.SetActive(true);
                name.text = stanok.name;
                dohod.text = "Прибыль (тыс.руб.): " + stanok.GetComponent<EconomyScript>().dohod;
                rashod.text = "Расход (тыс.руб.): " + stanok.GetComponent<EconomyScript>().rashod;
                curLvl.text = "Текущий уровень: " + stanok.GetComponent<EconomyScript>().currentLvl;
                detali.text = "Деталей в месяц: " + (stanok.GetComponent<EconomyScript>().producedDetails + stanok.GetComponent<EconomyScript>().brakO);
                brak.text = "Бракованных деталей: " + stanok.GetComponent<EconomyScript>().brakO;
                modernCost.text = "Стоимость модернизации (тыс.руб.): " + stanok.GetComponent<EconomyScript>().modernCost;
            }
            else
            {
                panel.SetActive(true);
                name.text = stanok.name;
                dohod.text = "Прибыль (тыс.руб.): " + stanok.GetComponent<EconomyScript>().dohod;
                rashod.text = "Расход (тыс.руб.): " + stanok.GetComponent<EconomyScript>().rashod;
                curLvl.text = "Текущий уровень: " + stanok.GetComponent<EconomyScript>().currentLvl;
                detali.text = "Деталей в месяц: " + (stanok.GetComponent<EconomyScript>().producedDetails + stanok.GetComponent<EconomyScript>().brakO);
                brak.text = "Бракованных деталей: " + stanok.GetComponent<EconomyScript>().brakO;
                modernCost.text = "Стоимость модернизации: MAX LVL";

            }
        }
    }
}
