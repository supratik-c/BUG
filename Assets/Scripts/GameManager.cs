using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TotalEnemy;
    public int EnemyRemain;

    public Text Score;
    public Text Round;

    public RoomLists Gen;

    public GameObject Win;
    public GameObject Lose;

	private void Awake()
	{
        Win.SetActive(false);
        Lose.SetActive(false);
    }

	public void Init() 
    {
        //PlayerPrefs.DeleteKey("RoomMod");
        EnemyRemain = TotalEnemy;
        Score.text = $"{EnemyRemain}/{TotalEnemy}";
        Round.text = $"Round {PlayerPrefs.GetInt("Round")+1}";
    }

    public void MinusEnemy() 
    {
        EnemyRemain -= 1;
        Score.text = $"{EnemyRemain}/{TotalEnemy}";
        if (EnemyRemain == 0) 
        {
            Debug.Log("Winner ");
            WinLose(true);
        }
    }

    public void WinLose(bool win) 
    {
        if (win)
        {
            PlayerPrefs.SetInt("RoomMod",Gen.MaxRoomsToSpawn + 5);
            PlayerPrefs.SetInt("Round",PlayerPrefs.GetInt("Round")+1);
            Win.SetActive(true);
        }
        else 
        {
            Lose.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Reload() 
    {
        SceneManager.LoadScene(0);
    }
}
