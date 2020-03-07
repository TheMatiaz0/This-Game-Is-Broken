using GameJolt.API;
using GameJolt.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tables
{
    Highscore = 477684,
    Downscore = 481420
}

public class HighscoreTable : MonoBehaviour
{
    [SerializeField]
    private GameObject GJStuff;

#if GAME_JOLT

    protected void Start()
    {
        GJStuff.SetActive(true);
    }

    public void Show()
    {
        GameJoltUI.Instance.ShowLeaderboards();
    }

    public void AddScore (InputField nickname)
    {
        if (!GameJoltAPI.Instance.HasSignedInUser)
        {
            AddProcess(nickname.text);
        }


        else
        {
            AddProcess(null);
        }
    }

    private void AddProcess (string nickname = null)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            if (GameJoltAPI.Instance.CurrentUser != null)
            {
                GameJolt.API.Scores.Add(GameManager.Instance.Score, GameManager.Instance.Score.ToString(), (int)Tables.Highscore);
                GameJolt.API.Scores.Add(GameManager.Instance.Score, GameManager.Instance.Score.ToString(), (int)Tables.Downscore);
            }
        }

        else
        {
            GameJolt.API.Scores.Add(GameManager.Instance.Score, GameManager.Instance.Score.ToString(), nickname, (int)Tables.Highscore);
            GameJolt.API.Scores.Add(GameManager.Instance.Score, GameManager.Instance.Score.ToString(), nickname, (int)Tables.Downscore);
        }

    }
#endif
}
