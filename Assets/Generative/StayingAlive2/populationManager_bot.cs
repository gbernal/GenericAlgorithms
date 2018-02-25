using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class populationManager_bot : MonoBehaviour {
    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float trialTime = 5;
    int generation = 1;


    GUIStyle guiStyle = new GUIStyle();
    void OnGUI()
    {

        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(10, 10, 140, 140), "Stats: ", guiStyle);
        GUI.Label(new Rect(10, 25, 100, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 100, 30), string.Format("Time: {00:0.0}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 100, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();

    }
    // Use this for initialization
    void Start () {

        for (int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
                this.transform.position.y,
                this.transform.position.z + Random.Range(-2, 2));

            GameObject b = Instantiate(botPrefab, startingPos, this.transform.rotation);
            b.GetComponent<Brain_bot>().Init();
            population.Add(b);
        }
    }
    GameObject Breed_bot(GameObject parent1, GameObject parent2)
    {
        Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
               this.transform.position.y,
               this.transform.position.z + Random.Range(-2, 2));
        GameObject offspring = Instantiate(botPrefab, startingPos, this.transform.rotation);
        Brain_bot b = offspring.GetComponent<Brain_bot>();

        if (Random.Range(0, 100) == 1)//mutate at 1%
        {
            b.Init();
            b.dna.Mutate();

        }
        else
        {
            b.Init();
            b.dna.Combine(parent1.GetComponent<Brain_bot>().dna, parent2.GetComponent<Brain_bot>().dna);
        }
        return offspring;
    }

    void BreedNewPopulation()
    {


        List<GameObject> newPopulation = new List<GameObject>();

        //Get rid of unfit individuals based how long they have been alive
        //List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain_bot>().timeAlive).ToList();

        //Get rid of unfit individuals based how far they have been traveled
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain_bot>().timeWalking).ToList();

        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed_bot(sortedList[i], sortedList[i + 1]));
            population.Add(Breed_bot(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);

        }

        generation++;


    }

    // Update is called once per frame
    void Update () {

        elapsed += Time.deltaTime;
        if (elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }

    }
}
