using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject ObjectToPickUp;
    public GameObject ObjectInHand;

    void Start()
    {
        ObjectToPickUp.SetActive(false);
        PickUpText.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                ObjectToPickUp.SetActive(true);
                PickUpText.SetActive(false);
                ObjectInHand.SetActive(false);
            }
            
        }
    }
}
