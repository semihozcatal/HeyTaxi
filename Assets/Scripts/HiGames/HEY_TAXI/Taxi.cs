using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace HiGames.HEY_TAXI
{
    public class Taxi : MonoBehaviour
    {
        public ObjectColor carColor;

        private GameManager gameManager => GameManager.Instance;
        
        private RewindControlManager rewindManager => RewindControlManager.Instance;

        private NavMeshAgent agent;
        

        [SerializeField] private GameObject highLight;
        private List<GameObject> highLightList = new List<GameObject>();
        [SerializeField] private Canvas tutorial;
        

        [SerializeField] private NavMeshAgent taxiAgent;

        private void Start()
        {
            rewindManager.rewindStart += OnRewindStart;
            rewindManager.rewindDone += OnRewindDone;
        }

        public void ChooseTaxi()
        {
            tutorial.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            tutorial.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;

            GameObject _highLight = Instantiate(highLight, transform.position, transform.rotation, transform);
            highLightList.Add(_highLight);

            agent = taxiAgent;
        }

        public void UnChooseTaxi()
        {
            foreach (var t in highLightList)
            {
                Destroy(t.gameObject);
            }
        }

        public void ChooseRoad(Vector3 destination)
        {
            if (gameManager.isLevelComplete == false)
            {
                taxiAgent.isStopped = false;
                tutorial.transform.GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
                agent.SetDestination(destination);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Taxi"))
            {
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                if (gameObject.GetComponentInChildren<Customer>() != null)
                {
                    gameObject.GetComponentInChildren<Customer>().GetComponent<BoxCollider>().enabled = false;
                }

                other.transform.DOPunchRotation(new Vector3(30, 0, 0), 1f, 1);
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameManager.GameOver();
                gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
        }

        private void OnRewindStart()
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }

        private void OnRewindDone()
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            if (gameObject.GetComponentInChildren<Customer>() != null)
            {
                gameObject.GetComponentInChildren<Customer>().GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}