using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : BeamInteractive
{
    [SerializeField]
    private bool lightBeamOn = true;
    [SerializeField]
    private string colour;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    GameObject red,yellow,blue;
    public Vector3 hitpos;

    private GameObject lasthit;

    private GameObject haveGlowWith;
    private GameObject otherObject;
    [SerializeField]
    private GameObject instantietedGlow;
    private bool isGlowing;


    private bool castRay = false;

    private Renderer rend;

    private void Awake()
    {
        isGlowing = false;
        rend = GetComponent<Renderer>();
        
        rend.material.SetFloat("_TimeOffset", Random.Range(-10, 10));
        if (GetComponentInParent<SetColor>())
        {
            colour = GetComponentInParent<SetColor>().GetMyColor();
        }
        rend.material.SetColor("_Emmision", Colors.colors[colour]);
        instantietedGlow.transform.localRotation = Quaternion.Euler(0, 90, 0);
        instantietedGlow.GetComponent<Renderer>().material.SetColor("_Emmision", Colors.colors[colour]);
    }


    private void LateUpdate()
    {
        if (lightBeamOn)
        {
            SetLasers();
        }
    }
    private void OnDisable()
    {
        if (!this.transform.parent.gameObject.activeSelf)
        {
            if (otherObject != null && otherObject.GetComponent<BeamInteractive>())
            {
                otherObject.GetComponent<BeamInteractive>().BeamExit(this.gameObject);
            }
        }
        otherObject = null;
        lasthit = null;
    }

    private void SetLasers()
    {
        if (castRay)
        {
            RaycastHit hit_one;
            var cast = Physics.CapsuleCast(transform.position + transform.forward * 0.05f, transform.position - transform.forward * 0.05f, 0.01f, transform.right, out hit_one, 100, mask);
            if (cast && hit_one.collider.gameObject == lasthit && hit_one.collider.gameObject != instantietedGlow)
            {
                hitpos = hit_one.point;
                Vector3 vec_one = new Vector3(1, 0, 0) * (hit_one.distance + 0.05f) + new Vector3(0, transform.localScale.y, transform.localScale.z);
                transform.localScale = vec_one;
                SetParticleSystem(red, vec_one);
                SetParticleSystem(yellow, vec_one);
                SetParticleSystem(blue, vec_one);
                //first time hitting an object
                if (hit_one.collider.GetComponent<BeamInteractive>() && hit_one.collider.gameObject != otherObject)
                {
                    hit_one.collider.GetComponent<BeamInteractive>().BeamEnter(colour, this.gameObject);
                }
                //other then first time hitting a same object
                else if (hit_one.collider.GetComponent<BeamInteractive>())
                {
                    hit_one.collider.GetComponent<BeamInteractive>().BeamStay(this.gameObject);
                }
                if (isGlowing)
                {
                    float dot = Vector3.Dot(transform.right, hit_one.transform.right);
                    if (dot < 0.01f && dot > -0.01f)
                    {
                        instantietedGlow.transform.localPosition = new Vector3(1, 0, 0) * (hit_one.distance + 0.01f + 0.05f);
                    }
                    else
                    {
                        instantietedGlow.transform.localPosition = new Vector3(1, 0, 0) * (hit_one.distance + 0.01f + 0.025f);
                    }
                    
                }
                otherObject = hit_one.collider.gameObject;
            }
            else if (cast)
            {

                if (otherObject != null)
                {
                    if (otherObject.GetComponent<BeamInteractive>())
                    {
                        otherObject.GetComponent<BeamInteractive>().BeamExit(this.gameObject);
                        otherObject = null;
                    }
                }
                StopGlow();
                lasthit = hit_one.collider.gameObject;
            }
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(Offset());
            }
        }
    }

    public void SetLightBeamOff() {
        Vector3 scale_one = transform.localScale;
        transform.localScale = new Vector3(0, scale_one[1], scale_one[2]);
        transform.parent.gameObject.SetActive(false);
        lightBeamOn = false;
    }

    public void SetLightBeamOn() {
        lightBeamOn = true;
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Emmision", Colors.colors[colour]);
        rend.material.SetFloat("_TimeOffset", Random.Range(-10, 10));
        instantietedGlow.transform.localRotation = Quaternion.Euler(0, 90, 0);
        instantietedGlow.GetComponent<Renderer>().material.SetColor("_Emmision", Colors.colors[colour]);
        SetLasers();
        transform.parent.gameObject.SetActive(true);
    }

    public void SetColour(string set) {
        colour = set;
    }

    public string GetColour() {
        return colour;
    }

    //check which particles should be enabled
    public void OnEnable() {
        StartCoroutine(Offset());
        if (Colors.ContainsRed(colour))
        {
            red.SetActive(true);
        }
        else {
            red.SetActive(false);
        }
        if (Colors.ContainsYellow(colour))
        {
            yellow.SetActive(true);
        }
        else {
            yellow.SetActive(false);
        }
        if (Colors.ContainsBlue(colour))
        {
            blue.SetActive(true);
        }
        else {
            blue.SetActive(false);
        }
    
    }
    //set particle systems
    private void SetParticleSystem(GameObject parti, Vector3 vec_one) {
        ParticleSystem.ShapeModule shape = parti.GetComponent<ParticleSystem>().shape;
        shape.scale = new Vector3(shape.scale.x, shape.scale.y, vec_one.x*5 - 2);
        ParticleSystem.EmissionModule emis = parti.GetComponent<ParticleSystem>().emission;
        emis.rateOverTime = vec_one.x*2;
    }


    private void StopGlow() {
        instantietedGlow.SetActive(false);
        isGlowing = false;
    }

    private void StartGlow() {
        instantietedGlow.SetActive(true);
        isGlowing = true;
    }

    public override void BeamEnter(string color, GameObject beam)
    {
        StartGlow();
    }

    public override void BeamStay(GameObject beam)
    {
        if (!isGlowing) {
            StartGlow();
        }
    }

    public override void BeamExit(GameObject beam)
    {
        StopGlow();
    }

    private void OnDestroy()
    {
        if (otherObject != null && otherObject.GetComponent<BeamInteractive>())
        {
            otherObject.GetComponent<BeamInteractive>().BeamExit(this.gameObject);
        }
    }
    //to desynchronize raycasts
    private IEnumerator Offset() {
        castRay = false;
        yield return new WaitForSeconds(Random.Range(0, 0.05f));
        castRay = true;
    }


}
