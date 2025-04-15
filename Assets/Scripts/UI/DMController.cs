using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class DMController : MonoBehaviour
{
    public int DLIndex = 0;
    public Volume DizzyEffect;
    public GameObject DL1;
    public GameObject DL2;
    public GameObject DL3;
    public PlayerMovement playerMovement;

    void Start()
    {
        DizzyEffect = GameObject.FindGameObjectWithTag("Effects").GetComponent<Volume>();
        DizzyEffect.weight = 0;
        UpdateDrunkUI();
        ApplyDizzyEffect();
    }
    public void IncreaseDrunkLevel()
    {
        DLIndex = Mathf.Clamp(DLIndex + 1, 0, 4);
        UpdateDrunkUI();
        ApplyDizzyEffect();
    }
    void UpdateDrunkUI()
    {
        DL1.SetActive(DLIndex >= 1);
        if(DLIndex >= 1)
        {
            DizzyEffect.weight = 0.5f;
        }
        DL2.SetActive(DLIndex >= 2);
        if (DLIndex >= 2)
        {
            DizzyEffect.weight = 0.75f;
        }
        DL3.SetActive(DLIndex >= 3);
        if (DLIndex >= 3)
        {
            DizzyEffect.weight = 1f;
        }
        if (DLIndex >= 4)
        {
           StartCoroutine(Cooldown()); 
        }
    }
    void ApplyDizzyEffect()
    {
        if (playerMovement == null) return; 
        switch (DLIndex)
        {
            case 1:
                playerMovement.walkSpeed *= 1.2f; 
                playerMovement.runSpeed *= 1.2f;
                break;
            case 2:
                playerMovement.walkSpeed *= 0.8f;
                playerMovement.runSpeed *= 0.8f;
                break;
            case 3:
                playerMovement.walkSpeed *= 0.6f;
                playerMovement.runSpeed *= 0.6f;
                playerMovement.transform.Rotate(0, Random.Range(-5f, 5f), 0);
                break;
        }
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds (7f);
        DLIndex --;
        yield return new WaitForSeconds (7f);
        DLIndex --;
    }

}
