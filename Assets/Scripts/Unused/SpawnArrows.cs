using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrows : MonoBehaviour
{
    [SerializeField]
    private float firstSpawn, spawnTime;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private float spawndistance, spawnheight;
    [SerializeField]
    private Light thelight;
    [SerializeField]
    private button but;
    private Game game;
    [SerializeField]
    GameObject dangerZone;
    [SerializeField]
    LayerMask levelMask;


    [SerializeField]
    Material danger;
    [SerializeField]
    Material safety;
    [SerializeField]
    Material fire;


    float distance;

    private Material mat;
    private void Awake()
    {
        game = GetComponentInParent<Game>();
        mat = GetComponent<Material>();
    }
    private void Update()
    {
        SetDangerZone();
    }


    private void SetDangerZone() {
        if (dangerZone != null)
        {
            RaycastHit hit;
            RaycastHit hit2;
            Physics.Raycast(transform.position - transform.forward * 0.39f + new Vector3(0, spawnheight, 0), transform.right, out hit, 100, levelMask);
            Physics.Raycast(transform.position - transform.forward * 0.61f + new Vector3(0, spawnheight, 0), transform.right, out hit2, 100, levelMask);
            if (hit.distance != distance || hit2.distance != distance)
            {
                if (hit.distance < hit2.distance)
                {
                    distance = hit.distance;
                }
                else
                {
                    distance = hit2.distance;
                }

                Vector3 vec = new Vector3(-1, 0, 0) * distance;
                for (int i = 0; i < 3; i++)
                {
                    if (Mathf.Abs(vec[i]) < 0.2)
                    {
                        vec[i] = 1;
                    }
                }
                dangerZone.transform.localScale = vec;
            }
        }

    }


    private void OnEnable()
    {
        StartCoroutine(wait());
    }

    public IEnumerator wait(){
        yield return new WaitForSeconds(firstSpawn);
        StartCoroutine(Spawn());
    }
    public IEnumerator Spawn() {
        bool stoped = false;
        while (true)
        {
            if ((but != null && !but.getIsPressed()) || but == null)
            {
                if (stoped) {
                    yield return new WaitForSeconds(firstSpawn);
                    stoped = false;
                }
                dangerZone.GetComponent<MeshRenderer>().material = danger;
                if (spawnTime > 1)
                {
                    yield return new WaitForSeconds(1);
                }
                else {
                    yield return new WaitForSeconds(spawnTime);
                }
                dangerZone.GetComponent<MeshRenderer>().material = fire;
                RaycastHit hit;
                Physics.CapsuleCast(transform.position - transform.forward * 0.49f + new Vector3(0, spawnheight, 0) - transform.right * 0.2f, transform.position - transform.forward * 0.51f + new Vector3(0, spawnheight, 0) - transform.right * 0.2f, 0.1f, transform.right, out hit, 100);
                if (hit.collider.gameObject.name == "player")
                {
                    game.StartRespawn();
                }
                yield return new WaitForSeconds(0.1f);
                dangerZone.GetComponent<MeshRenderer>().material = safety;
                if (spawnTime > 1)
                {
                    yield return new WaitForSeconds(spawnTime - 1);
                }
            }
            else {
                stoped = true;
                yield return new WaitForSeconds(0.1f);
            }  
        }
    }

}
