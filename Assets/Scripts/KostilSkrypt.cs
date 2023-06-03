using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KostilSkrypt : MonoBehaviour
{
    [SerializeField] private Text userField;
    [SerializeField] private Text dohodVSrednem;
    [SerializeField] private Text detaliVSrednem;
    [SerializeField] private Text brakVSrednem;
    private GameObject[] stanki;
    public void kostil()
    {
        int money = 0;
        int details = 0;
        int brak = 0;
        stanki = GameObject.FindGameObjectsWithTag("Stanok");
        for(int i = 0; i<stanki.Length; i++)
        {
            if(stanki[i].activeSelf)
            {
                money = money + stanki[i].GetComponent<EconomyScript>().dohod;
                details = details + stanki[i].GetComponent<EconomyScript>().producedDetails;
                brak = brak + stanki[i].GetComponent<EconomyScript>().brakO;
            }
        }
        userField.text = DataHolder.CurrentUser;
        dohodVSrednem.text = string.Format("—редн€€ прибыль в мес€ц(тыс.руб.): {0}", money);
        detaliVSrednem.text = string.Format("¬ среднем произведено деталей в мес€ц: {0}", details);
        brakVSrednem.text = string.Format("¬ среднем бракованных деталей в мес€ц: {0}", brak);
    }
}
