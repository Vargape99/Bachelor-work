using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private bool laserOn = true;
    [SerializeField]
    private string colour;
    [SerializeField]
    LayerMask mask;

    private bool castLaser = true;

    public Vector3 hitpos;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Emmision", Colors.colors[colour]);
        rend.material.SetFloat("_TimeOffset", Random.Range(-10, 10));
    }

    private void Update()
    {
        if (laserOn)
        {
            if (castLaser)
            {
                SetLasers();
                StartCoroutine(Offset());
            }
        }
    }

    private void SetLasers()
    {
        RaycastHit hit_one;
        var cast = Physics.CapsuleCast(transform.position + transform.forward * 0.02f, transform.position - transform.forward * 0.02f, 0.01f, transform.right, out hit_one, 100, mask);
        if (cast)
        {
            hitpos = hit_one.point;
            Vector3 scale_one = transform.localScale;
            Vector3 vec_one = new Vector3(1, 0, 0) * (hit_one.distance + 0.065f) + new Vector3(0, scale_one[1], scale_one[2]);
            transform.localScale = vec_one;
            if (hit_one.transform.GetComponent<Moveable>()) {
                hit_one.transform.GetComponent<Moveable>().StartResetAnimation();
            }
        }
    }

    public void SetLaserOff()
    {
        Vector3 scale_one = transform.localScale;
        transform.localScale = new Vector3(0, scale_one[1], scale_one[2]);
        transform.parent.gameObject.SetActive(false);
        laserOn = false;
    }

    public void SetLaserOn()
    {
        laserOn = true;
        SetLasers();
        transform.parent.gameObject.SetActive(true);
        castLaser = true;
    }

    public void SetColour(string set)
    {
        colour = set;
    }

    public string GetColour()
    {
        return colour;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player") {
            GetComponentInParent<Game>().StartRespawn();
        }
    }

    private IEnumerator Offset()
    {
        castLaser = false;
        yield return new WaitForSeconds(Random.Range(0, 0.05f));
        castLaser = true;
    }


}
