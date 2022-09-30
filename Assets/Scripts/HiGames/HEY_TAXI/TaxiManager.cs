using UnityEngine;

namespace HiGames.HEY_TAXI
{
    public class TaxiManager : MonoBehaviour
    {
        [SerializeField] Camera cam;
        private Taxi currentTaxi;
        [SerializeField] private GameObject arrow;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Choose();
            }
        }

        private void Choose()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                switch (hit.transform.tag)
                {
                    case "Taxi":
                        if (currentTaxi != null)
                        {
                            currentTaxi.UnChooseTaxi();
                        }

                        currentTaxi = hit.transform.GetComponent<Taxi>();
                        currentTaxi.ChooseTaxi();
                        break;

                    case "Road":
                        if (currentTaxi == null)
                        {
                            return;
                        }
                        GameObject _point = Instantiate(arrow, hit.point + new Vector3(0, 1, 0),
                            arrow.transform.rotation);
                        Destroy(_point, 1.5f);
                        currentTaxi.ChooseRoad(hit.point);
                        break;
                }
            }
        }
    }
}