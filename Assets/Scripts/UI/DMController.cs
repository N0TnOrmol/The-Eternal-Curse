using System.Collections;
using Unity.VisualScripting;
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
        DLIndex = Mathf.Clamp(DLIndex + 1, 0, 3);
        UpdateDrunkUI();
        ApplyDizzyEffect();
    }
    void UpdateDrunkUI()
    {
        if(DLIndex == 0)
        {
            DizzyEffect.weight = 0f;
            DL1.SetActive(false);
            DL2.SetActive(false);
            DL3.SetActive(false);
        }
        if(DLIndex >= 1)
        {
            DizzyEffect.weight = 0.5f;
            DL1.SetActive(true);
            DL2.SetActive(false);
            DL3.SetActive(false);
            StartCoroutine(Cooldown());
        }
        if (DLIndex >= 2)
        {
            DizzyEffect.weight = 0.75f;
            DL2.SetActive(true);
            DL1.SetActive(true);
            DL3.SetActive(false);
            StartCoroutine(Cooldown());
        }
        if (DLIndex >= 3)
        {
            DizzyEffect.weight = 1f;
            DL3.SetActive(true);
            DL1.SetActive(true);
            DL2.SetActive(true);
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
        yield return new WaitForSeconds (5f);
        DLIndex --;
        UpdateDrunkUI();
    }
}
