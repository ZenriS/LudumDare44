using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destroy_Script : MonoBehaviour
{
    public int LifeTime;
    public int FadeTime;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (FadeTime == 0)
        {
            Destroy(this.gameObject,LifeTime);
        }
        else
        {
            float t = LifeTime - FadeTime;
            StartCoroutine(FadeTo(t,0, FadeTime));
        }
    }
    
    IEnumerator FadeTo(float delay,float aValue, float aTime)
    {
        yield return new WaitForSeconds(delay);
        float alpha = _spriteRenderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            _spriteRenderer.material.color = newColor;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
