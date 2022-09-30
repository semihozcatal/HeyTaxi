using System;
using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HiGames.HEY_TAXI
{
    public class GameManager : Framework.Singleton<GameManager>
    {
        [SerializeField] private Canvas gameOver;
        public Canvas GameOverCanvas  => gameOver;
        [SerializeField] private Canvas levelComplete;
        public List<GameObject> customerList = new List<GameObject>();

        [SerializeField] private GameObject time;

        
        public bool isLevelComplete = false;

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        IEnumerator DelayGameOver()
        {
            yield return new WaitForSeconds(1.3f);
            gameOver.gameObject.SetActive(true);
            time.GetComponent<GlobalClock>().localTimeScale= 0;
            isLevelComplete = true;
        }
        public void GameOver()
        {
            StartCoroutine(DelayGameOver());
        }
        IEnumerator DelayLevelComplete()
        {
            yield return new WaitForSeconds(0.7f);
            levelComplete.gameObject.SetActive(true);
            Time.timeScale = 0;
            isLevelComplete = true;
        }
        public void LevelComplete()
        {
            StartCoroutine(DelayLevelComplete());
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
        }

        public void LoopLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
            Time.timeScale = 1;
        }
        
    }
}