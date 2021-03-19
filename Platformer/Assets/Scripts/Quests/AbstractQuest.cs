using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AbstractQuest : MonoBehaviour
{
    protected int Coins;
    public string EnglishDescription;
    public string RussianDescription;
    public TextMeshProUGUI QuestDescriptionTMP;
    public TextMeshProUGUI QuestCoinsTMP;
    public GameObject Done;
    public GameObject NotDone;
}
