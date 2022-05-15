using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeArrows : MonoBehaviour
{
    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        float alpha = (1.0f/255 * 35f);
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alpha);
        StartCoroutine(addAlpha());
    }
    //with more time in maze increase alpha of arrows
    IEnumerator addAlpha() {
        yield return new WaitForSeconds(60);
        while (rend.material.color.a < 1) {           
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, (float)rend.material.color.a + 1.0f/255 * 10);
            yield return new WaitForSeconds(20);
        }
    }
}
