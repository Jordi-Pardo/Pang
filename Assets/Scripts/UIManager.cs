using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timerText;
    [SerializeField] private TMPro.TextMeshProUGUI countDownText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private GameObject loseCanvas;
    [SerializeField] private UIWinMenu winMenu;

    [Header("Weapon Info UI")]
    [SerializeField] private TMPro.TextMeshProUGUI weaponNameText;
    [SerializeField] private TMPro.TextMeshProUGUI weaponAmmoText;

    [Header("Pause")]
    [SerializeField] private GameObject pause;

    private GameManager gameManager;


    private void OnEnable()
    {
        gameManager = GameManager.instance;

        if (gameManager == null)
            return;

        gameManager.OnUpdateTime += UpdateTime;
        gameManager.OnUpdateCountDown += UpdateCountdown;
        gameManager.OnCountDownEnds += OnCountdownEnds;
        gameManager.OnUpdateScore += UpdateScore;
        gameManager.OnPlayerLose += ShowLoseMenu;
        gameManager.OnPlayerWin += ShowWinMenu;

        PlayerShoot.OnPlayerShootRifle += PlayerShoot_OnPlayerShootRifle;
        PlayerShoot.OnPlayerChangeWeapon += PlayerShoot_OnPlayerChangeWeapon;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool paused = !pause.activeInHierarchy;
            PauseGame(paused);
        }
    }


    private void PlayerShoot_OnPlayerChangeWeapon(PlayerShoot.Weapon weapon, int ammo)
    {
        weaponNameText.text = weapon.ToString();
        weaponAmmoText.text = weapon == PlayerShoot.Weapon.Hook ? "Inf." : ammo.ToString();
    }

    private void PlayerShoot_OnPlayerShootRifle(int ammo)
    {
        weaponAmmoText.text = ammo.ToString();
    }

    private void OnDisable()
    {
        if (gameManager == null)
            return;
        

        gameManager.OnUpdateTime -= UpdateTime;
        gameManager.OnUpdateCountDown -= UpdateCountdown;
        gameManager.OnCountDownEnds -= OnCountdownEnds;
        gameManager.OnUpdateScore -= UpdateScore;
        gameManager.OnPlayerLose -= ShowLoseMenu;
        gameManager.OnPlayerWin -= ShowWinMenu;

        PlayerShoot.OnPlayerShootRifle -= PlayerShoot_OnPlayerShootRifle;
        PlayerShoot.OnPlayerChangeWeapon -= PlayerShoot_OnPlayerChangeWeapon;


    }

    private void OnCountdownEnds()
    {
        countDownText.gameObject.SetActive(false);
    }
    private void UpdateTime(string text)
    {
        timerText.text = text;
    }
    private void UpdateCountdown(string text)
    {
        countDownText.text = text;
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

    private void PauseGame(bool toggle)
    {
        Time.timeScale = toggle ? 0 : 1;
        pause.SetActive(toggle);
    }
    

}
