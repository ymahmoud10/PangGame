using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum Screen
    {
        Main, Game, Win, Lose
    }
    
    public static UIManager Instance;

    [Header("References:")]
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Animator mainScreenAnimator;
    [SerializeField] private Animator gameScreenAnimator;
    [SerializeField] private Animator winScreenAnimator;
    [SerializeField] private Animator loseScreenAnimator;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelLabel(int level)
    {
        levelText.text = $"LEVEL {level}";
    }

    public void ShowScreen(Screen screen)
    {
        GetScreen(screen).SetActive(true);
    }
    
    public IEnumerator HideScreen(Screen screen)
    {
        GetScreenAnimator(screen).Play("Outro");
        yield return new WaitForSeconds(GetScreenAnimator(screen).GetCurrentAnimatorStateInfo(0).length);
        GetScreen(screen).SetActive(false);
    }

    #region Buttons Handlers
    
    public void OnPlayButtonClicked()
    {
        GameManager.Instance.StartGame();
        if (mainScreen.activeInHierarchy) StartCoroutine(HideScreen(Screen.Main));
        if (winScreen.activeInHierarchy) StartCoroutine(HideScreen(Screen.Win));
        if (loseScreen.activeInHierarchy) StartCoroutine(HideScreen(Screen.Lose));
        ShowScreen(Screen.Game);
    }

    public void OnBackToMenuButtonClicked()
    {
        GameManager.Instance.ResetGame();
        StartCoroutine(HideScreen(Screen.Game));
        if (winScreen.activeInHierarchy) StartCoroutine(HideScreen(Screen.Win));
        if (loseScreen.activeInHierarchy) StartCoroutine(HideScreen(Screen.Lose));
        ShowScreen(Screen.Main);
    }
    
    public void OnShootButtonClicked()
    {
        GameController.Instance.Shoot();
    }
    
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    #endregion

    #region Helpers
    
    private GameObject GetScreen(Screen screen)
    {
        switch (screen)
        {
            case Screen.Main:
                return mainScreen;
            case Screen.Game:
                return gameScreen;
            case Screen.Win:
                return winScreen;
            case Screen.Lose:
                return loseScreen;
            default:
                return null;
        }
    }
    
    private Animator GetScreenAnimator(Screen screen)
    {
        switch (screen)
        {
            case Screen.Main:
                return mainScreenAnimator;
            case Screen.Game:
                return gameScreenAnimator;
            case Screen.Win:
                return winScreenAnimator;
            case Screen.Lose:
                return loseScreenAnimator;
            default:
                return null;
        }
    }

    #endregion
    
}
