using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

namespace HiGames.HEY_TAXI
{
    public class Customer : MonoBehaviour
    {
        private GameManager gameManager => GameManager.Instance;
        private RewindControlManager rewindManager => RewindControlManager.Instance;

        private Taxi currentTaxi;
        
        [SerializeField] private new Texture light;
        [SerializeField] private GameObject hey;

        public ObjectColor customerColor;

        private bool isInTaxi;
        private bool isInDestination;

        private void Start()
        {
            InvokeRepeating("CreateHey", 2, 5);
            Debug.Log("Başlangıç");
            rewindManager.rewindDone += OnRewindDone;
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Taxi")) // Müşteri Toplama
            {
                transform.parent = other.transform;
                
                if (!isInTaxi)
                {
                    transform.DOLocalJump(Vector3.zero, 3f, 1, 0.5f);
                    other.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 1);
                    CancelInvoke("CreateHey");
                }
                currentTaxi = other.gameObject.GetComponent<Taxi>();
                if (customerColor != currentTaxi.carColor)
                    gameManager.GameOver();
                isInTaxi = true;
            }

            if (other.gameObject.CompareTag(("Destination"))) // Müşteri Teslimi
            {
                transform.parent = other.transform;
                isInDestination = true;
                transform.DOLocalJump(Vector3.zero, 3f, 1, 0.5f);
                if (isInDestination)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                
                other.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 1);
                var destinationColor = other.gameObject.GetComponent<Destination>().destinationColor;

                if (customerColor == destinationColor)
                {
                    gameManager.customerList.RemoveAt(0);
                    other.gameObject.GetComponent<Renderer>().material.mainTexture = light;
                    if (gameManager.customerList.Count == 0)
                    {
                        gameManager.LevelComplete();
                    }
                }
                else
                    gameManager.GameOver();
            }
        }
        private void CreateHey()
        {
            transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 1);
            GameObject _hey = Instantiate(hey,
                transform.position + new Vector3(0.25f, 3.25f, 0),
                quaternion.identity);
            _hey.transform.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 1);
            Destroy(_hey, 1f);
        }

        private void OnRewindDone()
        {
            //InvokeRepeating("CreateHey", 2, 5);
            Debug.Log("Rewind sonucu");
            gameObject.transform.SetParent(null);
            if (isInDestination == false)
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }

            if (currentTaxi != null && customerColor != currentTaxi.carColor )
            {
                isInTaxi = false;
            }

        }
    }
}