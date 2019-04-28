using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlantDrop_Script : MonoBehaviour
{
    private bool _allowPickp;
    public int BloodValue;
    private SpriteRenderer _spriteRenderer;

    void Start ()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        float rx = transform.localPosition.x;
        float ry = transform.localPosition.y;
        rx += Random.Range(-0.3f, 0.3f);
        ry += Random.Range(-0.3f, 0.3f);
        
        Vector3 target = new Vector3(rx,ry,0);
        transform.DOLocalMove(target,1f);
        StartCoroutine(RemoveItem());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerInv_Script pi = col.GetComponent<PlayerInv_Script>();
            if (pi.BloodAmount < pi.MaxBloodAmount)
            {
                pi.GainBlood(BloodValue);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator RemoveItem()
    {
        yield return new WaitForSeconds(10f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.enabled = false;
        Destroy(this.gameObject);

    }

}
