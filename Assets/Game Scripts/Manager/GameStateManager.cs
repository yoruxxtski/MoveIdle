
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public static event Action StartGamePlayer;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject MoveJoystick;
    

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public void StartGame() {
        StartGameManager.instance.GoOut();
        GameManager.gameOver = false;
        EnemyManager.instance.SpawnEnemies();
        StartGamePlayer?.Invoke();
        MoveJoystick.SetActive(true);
    }
    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }
}