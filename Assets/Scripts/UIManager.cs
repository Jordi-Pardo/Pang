using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timerText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private UIWinMenu winMenu;

    
    private GameManager gameManager;


    private void OnEnable()
    {
        gameManager = GameManager.instance;

        if (gameManager == null)
            return;

        gameManager.OnUpdateTime += UpdateTime;
        gameManager.OnUpdateScore += UpdateScore;
        gameManager.OnPlayerLose += ShowLoseMenu;
        gameManager.OnPlayerWin += ShowWinMenu;
    }

    private void OnDisable()
    {
        if (gameManager == null)
            return;
        

        gameManager.OnUpdateTime -= UpdateTime;
        gameManager.OnUpdateScore -= UpdateScore;
        gameManager.OnPlayerLose -= ShowLoseMenu;
        gameManager.OnPlayerWin -= ShowWinMenu;


    }


    private void UpdateTime(string text)
    {
        timerText.text = text;
    }
    private void UpdateScore(string score)
    {
        scoreText.text = score;
    }

    private void ShowLoseMenu()
    {
        loseCanvas.SetActive(true);
    }

    private void ShowWinMenu(string score, string remainingTime)
    {
        winMenu.gameObject.SetActive(true);
        winMenu.Setup(score, remainingTime);
    }
    

}
