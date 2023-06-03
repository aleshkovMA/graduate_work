using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewStanok : MonoBehaviour
{
    [SerializeField] List<GameObject> Stanki;
    [SerializeField] Text moneyField;
    public void CreateNew()
    {
        foreach(GameObject stanok in Stanki)
        {
            if(stanok.activeSelf==false && DataHolder.money>=767)
            {
                DataHolder.money = DataHolder.money - 767;
                moneyField.text = "Доступные средства (тыс.руб.): " + DataHolder.money;
                stanok.SetActive(true);
                break;
            }
        }
    }
}
