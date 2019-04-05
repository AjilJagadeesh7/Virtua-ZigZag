using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    

    public GameObject[] tilePrefabs;

    public GameObject CurrentTile;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    public Stack<GameObject> LeftTiles 
    {
        get { return leftTiles; }
        set { leftTiles = value; }
    }

    private Stack<GameObject> topTiles = new Stack<GameObject>();
    public Stack<GameObject> TopTiles
    {
        get { return topTiles; }
        set { topTiles = value; }
    }

    private static TileManager instance;

    public static TileManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<TileManager>();
            }

            return TileManager.instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTiles(100);

        for (int i = 0; i < 50; i++)
        {
            
            spawnTile();

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateTiles(int amount)
    {
        for (int i=0; i <amount;i++) 
        {
            leftTiles.Push(Instantiate(tilePrefabs[0]));
            topTiles.Push(Instantiate(tilePrefabs[1]));
            topTiles.Peek().name = "TopTile";
            leftTiles.Peek().name = "LeftTile";
            topTiles.Peek().SetActive(false);
            leftTiles.Peek().SetActive(false);
        }
        

    }
    
    public void spawnTile()
    {
        if(leftTiles.Count==0||topTiles.Count==0)
        {
            CreateTiles(20);
        }

       //Generating a random integer between 1 and 0 because there are 2 prefabs
       int randomIndex = Random.Range(0, 2);

        if(randomIndex==0)
        {
            GameObject tmp = leftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = CurrentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
            CurrentTile = tmp;
        }
        else if(randomIndex==1)
        {
            GameObject tmp = topTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = CurrentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
            CurrentTile = tmp;
        }
        int spawnPickUp = Random.Range(0, 10);
        if(spawnPickUp==0)
        {
            CurrentTile.transform.GetChild(1).gameObject.SetActive(true);
        }
       //CurrentTile = (GameObject)Instantiate(tilePrefabs[randomIndex], CurrentTile.transform.GetChild(0).transform.GetChild(randomIndex).position,Quaternion.identity);
    }
    public void RestGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
