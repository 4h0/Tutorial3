using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text[] uiText;
    public GameObject enemy;

    public bool gameEnd;
    public int score;

    private void Awake()
    {
        for (int counter = 0; counter < uiText.Length; counter++)
        {
            uiText[counter].enabled = false;
        }

        gameEnd = false;
        score = 0;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());

        uiText[0].text = "Player Score: " + score;
        uiText[1].text = "Press R to Restart";
        uiText[2].text = "Game Over";

        uiText[0].enabled = true;
    }

    private void Update()
    {
        uiText[0].text = "Player Score: " + score;

        if (gameEnd)
        {
            StartCoroutine(SlowGame());

            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;

                SceneManager.LoadScene("SampleScene");
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SlowGame()
    {
        uiText[1].enabled = true;
        uiText[2].enabled = true;

        yield return new WaitForSeconds(7f);

        Time.timeScale = .01f;
    }

    IEnumerator SpawnEnemy()
    {
        while(!gameEnd)
        {
            for(int counter = 0; counter < Random.Range(6, 13); counter++)
            {
                Instantiate(enemy, new Vector3 (Random.Range(-5.5f, 5.5f), 0f, Random.Range(6f, 13f)), Quaternion.identity);

                yield return new WaitForSeconds(.9f);
            }

            yield return new WaitForSeconds(6f);
        }
    }
}
