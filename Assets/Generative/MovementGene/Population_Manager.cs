using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Population_Manager : MonoBehaviour {
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
        GUI.Box(new Rect(10, 10, 140, 140), "Stats: " , guiStyle);
        GUI.Label(new Rect(10, 25, 100, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 100, 30), string.Format("Time: {00:0.0}",elapsed), guiStyle);
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
            b.GetComponent<Brain>().Init();
            population.Add(b);
        }

        }		
	


GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 startingPos = new Vector3(this.transform.position.x + Random.Range(-2, 2),
                    this.transform.position.y,
                    this.transform.position.x + Random.Range(-2, 2));

        GameObject offspring_h = Instantiate(botPrefab, startingPos, Quaternion.identity);

        Brain b = offspring_h.GetComponent<Brain>();

        //swap parent data

        if (Random.Range(0, 100) == 1)
        {
            b.Init();
            b.dna1.Mutate();
        }
        else
        {
            b.Init();
            b.dna1.Combine(parent1.GetComponent<Brain>().dna1, parent2.GetComponent<Brain>().dna1);
        }
        return offspring_h;
    }
    void BreedNewPopulation()
    {

        List<GameObject> newPopulation = new List<GameObject>();

        //Get rid of unfit individuals based how long they have been alive
       // List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().timeAlive).ToList();


        //Get rid of unfit individuals based how far they have been traveled
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().distanceTraveled).ToList();

        population.Clear();

            for (int i= (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count -1; i++)
            {
                population.Add(Breed(sortedList[i], sortedList[i+1]));
                population.Add(Breed(sortedList[i+1], sortedList[i]));
            }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);

        }

        generation++;

    }

    // Update is called once per frame
    void Update ()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
	}
}
