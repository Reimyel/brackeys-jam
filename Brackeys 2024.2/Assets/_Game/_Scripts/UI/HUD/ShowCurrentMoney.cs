using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowCurrentMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCurrentMoney;

    private void Update() => Show();

    private void Show() => txtCurrentMoney.text = BalloonStats.CurrentMoney.ToString();
}
