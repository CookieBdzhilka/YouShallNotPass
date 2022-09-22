using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameOverMenu GameOverWindow;
    public CharacterPlayer Player;

    private void OnEnable()
    {
        Player.OnDeadEvent += ShowGameOverWindow;
        SceneManager.sceneLoaded += FindPlayer;
    }
    private void OnDisable()
    {
        Player.OnDeadEvent -= ShowGameOverWindow;
        SceneManager.sceneLoaded -= FindPlayer;
    }
    private void ShowGameOverWindow()
    {
        Canvas PlayerCanvas = Player.PlayerHUD;
        GameOverMenu Window = Instantiate(GameOverWindow);
        Window.RestartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        Window.transform.SetParent(PlayerCanvas.transform, false);
    }
    private void FindPlayer(Scene scene, LoadSceneMode sceneMode)
    {
        Player = FindObjectOfType<CharacterPlayer>();
        Player.OnDeadEvent += ShowGameOverWindow;
    }
}
