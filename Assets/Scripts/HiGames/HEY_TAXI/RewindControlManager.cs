using System;
using System.Collections;
using Chronos;
using UnityEngine;

namespace HiGames.HEY_TAXI
{
    public class RewindControlManager : Framework.Singleton<RewindControlManager>
    {
        private GameManager gameManager => GameManager.Instance;
        [SerializeField] private GlobalClock time;
        public Action rewindDone;
        public Action rewindStart;
        
        public void Rewind()
        {
            gameManager.isLevelComplete = false;
            time.localTimeScale= -1f;
            StartCoroutine(DelayRewind());
            gameManager.GameOverCanvas.gameObject.SetActive(false);
        }
        IEnumerator DelayRewind()
        {
            rewindStart?.Invoke();
            yield return new WaitForSeconds(3f);
            time.GetComponent<GlobalClock>().localTimeScale= 1f;
            rewindDone?.Invoke();
        }
        
    }
}
