using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DMController : MonoBehaviour
{
    public int DLIndex = 0;
    public KeyCode TestDM;
    public GameObject DL1;
    public GameObject DL2;
    public GameObject DL3;
    //private bool Activation;

    void Update()
    {
        DLIndex = Mathf.Clamp(DLIndex, 0, 4);
        if (Input.GetKeyDown(TestDM))
        {
            DLIndex ++;
            if(DLIndex >= 1)
            {
                DL1.SetActive(true);
                if(DLIndex >= 2)
                {
                    DL2.SetActive(true);
                    if(DLIndex >= 3)
                    {
                        DL3.SetActive(true);
                        if(DLIndex >= 4)
                        {
                            StartCoroutine(SoberUp());
                            if (DLIndex <= 0)
                            {
                                StopCoroutine(SoberUp());
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator SoberUp()
    {
            DLIndex --;
            yield return new WaitForSeconds(10f);
            DLIndex --;
            DL3.SetActive(false);
            yield return new WaitForSeconds(10f);
            DLIndex --;
            DL2.SetActive(false);
            yield return new WaitForSeconds(10f);
            DLIndex --;
            DL1.SetActive(false);
    }
}
