using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pexeso : MonoBehaviour
{
    private int numberofPeices;
    [SerializeField]
    private Color[] colors;
    // Start is called before the first frame update
    [SerializeField]
    private pexeso_tile[] pexesoTiles;
    [SerializeField]
    private pexeso_tile[] turned;
    private int turn = 0;
    private bool gameCompleted = false;
    void Start()
    {
        numberofPeices = pexesoTiles.Length;
        int[] touse = new int[numberofPeices];
        turned = new pexeso_tile[2];

        int pos = 0;
        for (int i = 0; i < numberofPeices; i++) {
            touse[i] = pos;
            if (i % 2 == 1) {
                pos++;
            }
        }
        shuffel(touse);

        pexesoTiles = new pexeso_tile[numberofPeices];
        for (int i = 0; i < numberofPeices; i++) {
            Debug.Log("was here" + i + " of " + numberofPeices);
            Debug.Log(transform.GetChild(i).gameObject.name);
            pexesoTiles[i] = transform.GetChild(i).GetComponent<pexeso_tile>();
            pexesoTiles[i].setNumber(touse[i]);
            pexesoTiles[i].setColor(colors[touse[i]]);
        }
    }
    //Knuth shuffel
    void shuffel(int[] numbers) {
        for (int i = 0; i < numbers.Length; i++) {
            int temp = numbers[i];
            int rand = Random.Range(i, numbers.Length);
            numbers[i] = numbers[rand];
            numbers[rand] = temp;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameCompleted &&  turn < 2)
        {
            bool done = true;
            foreach (pexeso_tile tile in pexesoTiles)
            {
                if (tile.getTurned() && !tile.getFoundDouble() && turned[0]!= tile)
                {
                    turned[turn] = tile;
                    turn++;
                }
                if (!tile.getTurned() && turned[0] == tile) {
                    turned[0] = null;
                    turn--;
                }
                else if (!tile.getTurned())
                {
                    done = false;
                }
            }

            if (turn == 2) {
                StartCoroutine(Wait(1));
            }
            if (done)
            {
                gameCompleted = true;
                Debug.Log("GameCompleted");
            }
        }
    }

    IEnumerator Wait(float duration) {
        Debug.Log("START WAIT " + Time.deltaTime);
        yield return new WaitForSeconds(duration);
        if (turned[0].getNumber() == turned[1].getNumber())
        {
            turned[0].setFoundDouble(true);
            turned[1].setFoundDouble(true);
            Debug.Log("MATCH FOUND");
        }
        else
        {
            turned[0].setTurned(false);
            turned[1].setTurned(false);
            Debug.Log("NotAMatch");
        }
        turned[0] = null;
        turned[1] = null;
        turn = 0;
        Debug.Log("END WAIT " + Time.deltaTime);
    }
}
