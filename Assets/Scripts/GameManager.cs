using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int sceneIndex = 1;
    [SerializeField]
    public int menu_level = 0;
    public int lv1_en = 1;
    public int lv2_en = 2;
    public int lv3_en = 3;
    public int cur_en = 0; //текущее число врагов
    public int final_scene = 3; //последний уровень
    public int win_window = 4;
    public int lose_window = 5;

    private void Awake()
    {
        cur_en = getEnemiesCount(SceneManager.GetActiveScene().buildIndex);
        SceneManager.sceneLoaded += Initialize;
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded GM");
        var playerInput = FindObjectOfType<Player_Input>();
        if (playerInput != null)
            player = playerInput.gameObject;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return;
    }

    public void closeGame()
    {
        Application.Quit();
    }

    public void Lv_win()
    {
        cur_en -= 1;
        if (cur_en <= 0 )
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                G_win();
            }
            cur_en = 0;
            LoadNextLevel();
        }
    }

    private int getEnemiesCount(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                return lv1_en;
            case 2:
                return lv2_en;
            case 3:
                return lv3_en;
            default:
                return 0;
        }
    }

    public void G_win()
    {
        SceneManager.LoadScene(win_window);
    }

    public void G_lose()
    {
        SceneManager.LoadScene(lose_window);
    }

    public void back_to_menu()
    {
        SceneManager.LoadScene(menu_level);
    }

}