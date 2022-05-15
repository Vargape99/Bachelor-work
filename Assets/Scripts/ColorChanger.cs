using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : BeamInteractive
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    Dictionary<GameObject,GameObject> allLasers;
    public List<GameObject> createdLasers;
    [SerializeField]
    List<GameObject> recievingLasers;
    string colour;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    bool isAdditive = false;
    List<GameObject> toremove;

    private void Awake() {
        toremove = new List<GameObject>();
        allLasers = new Dictionary<GameObject, GameObject>();
        createdLasers = new List<GameObject>();
        recievingLasers = new List<GameObject>();
        colour = GetComponentInParent<SetColor>().GetMyColor();
        GetComponent<Renderer>().material.SetColor("_Emmision", Colors.colors[colour]);
    }

    public override void BeamEnter(string beamcolor, GameObject beam)
    {
            //check if Im not interacting with it, or I have created it
            if (!createdLasers.Contains(beam) &&  !recievingLasers.Contains(beam))
            {
                string newCol;
                if (isAdditive) {
                    newCol = Colors.AddColors(beamcolor, colour);
                } else {
                    newCol = Colors.SubstractColors(beamcolor, colour);
                }
                //create new beam
                if (newCol != "Black" && newCol != null)
                {         
                    GameObject newlaser = Instantiate(prefab);
                    newlaser.transform.rotation = beam.transform.rotation;
                    newlaser.transform.parent = this.transform;
                    allLasers[beam] = newlaser;
                    recievingLasers.Add(beam);
                    createdLasers.Add(newlaser.GetComponentInChildren<LightBeam>().gameObject);
                    LightBeam las = newlaser.GetComponentInChildren<LightBeam>();
                Debug.Log(newCol);
                    las.SetColour(newCol);
                    las.OnEnable();
                    las.SetLightBeamOn();
                }
            }
        
    }
    //change new beams position if necesary
    public override void BeamStay(GameObject beam) {
            if (recievingLasers.Contains(beam))
            {
               Vector3 right = beam.transform.right;
                for (int i = 0; i < 3; i++) {
                    if (right[i] < 0) {
                        right[i] *= -1;
                    }
                }
                Vector3 vec = Vector3.Scale((new Vector3(1, 1, 1) - right), beam.transform.position);

                vec = vec + Vector3.Scale(transform.position , right) + beam.transform.right * 0.001f;

                allLasers[beam].transform.position = vec;
            }


    }

    public override void BeamExit(GameObject beam)
    {
        if ( recievingLasers.Contains(beam)) {
            Remove(beam);
        }
    }

    //remove from my lists
    private void Remove(GameObject other) {
            recievingLasers.Remove(other.gameObject);
            createdLasers.Remove(allLasers[other.gameObject]);
            allLasers[other.gameObject].GetComponentInChildren<LightBeam>().SetLightBeamOff();
            Destroy(allLasers[other.gameObject],0.1f);
            allLasers.Remove(other.gameObject);
        
    }
}
