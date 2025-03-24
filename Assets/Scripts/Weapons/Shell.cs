using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour
{
    private float lifeTime = 5;
    private Material mat;
    private Color originaCol;
    private float fadePercent;
    private float deathTime;
    private bool fading;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originaCol = mat.color;
        deathTime = Time.time + lifeTime;

        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        while(true)
        {
            yield return new WaitForSeconds(.2f);
            if(fading)
            {
                fadePercent += Time.deltaTime;
                mat.color = Color.Lerp(originaCol,Color.clear,fadePercent);
                if(fadePercent >= 1)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if(Time.time > deathTime)
                {
                    fading = true;
                }
            }
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Ground")
        {
            GetComponent<Rigidbody>().Sleep();
        }
    }
}
