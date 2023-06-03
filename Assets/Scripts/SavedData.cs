using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SavedData
{
    public List<StankiClass> stanki;
    public int savedMoneys;
    public int savedDays;
    public SavedData ()
    {
        stanki = new List<StankiClass>();
    }
}
