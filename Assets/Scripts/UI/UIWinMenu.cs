using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIWinMenu : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI timerText;

    public void Setup(string scoreText, string timerText)
    {
        this.scoreText.text = scoreText;
        this.timerText.text = timerText;
    }
}
