using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationManager : MonoBehaviour {

	public GameObject chameleonPrefab;
	public int populationSize = 10;
	List<GameObject>population = new List<GameObject>();
	public static float elapased = 0;

	int trialTime = 10;
	int generation = 1;


	GUIStyle guiStyle = new GUIStyle();
	void OnGUI(){

		guiStyle.fontSize = 50;
		guiStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (10, 10, 100, 20), "Generations: " + generation, guiStyle);
		GUI.Label (new Rect (10, 65, 100, 20), "Trial time : " + (int)elapased, guiStyle);

	
	
	}


	// Use this for initialization
	void Start () {

		for(int i=0; i< populationSize ; i++)
		{
			Vector3 pos = new Vector3 (Random.Range (-9, 9), Random.Range (-4.0f, 4.5f), 0);
			GameObject go = Instantiate (chameleonPrefab, pos, Quaternion.identity);
			go.GetComponent<DNA>().r = Random.Range (0.0f, 1.0f);
			go.GetComponent<DNA>().g = Random.Range (0.0f, 1.0f);
			go.GetComponent<DNA>().b = Random.Range (0.0f, 1.0f);

			population.Add (go);
		}
	}
	
	// Update is called once per frame
	void Update () {

		elapased += Time.deltaTime;
		if (elapased > trialTime) 
		
		{
			BreedNewPopulation ();
			elapased = 0;
		}
	}
	GameObject Breed(GameObject parent1, GameObject parent2)
	{
		Vector3 pos = new Vector3 (Random.Range (-9, 9), Random.Range (-4.5f, 4.5f), 0);
		GameObject offspring = Instantiate (chameleonPrefab, pos, Quaternion.identity);
		DNA dna1 = parent1.GetComponent<DNA>();
		DNA dna2 = parent2.GetComponent<DNA>();

		//swap parent data

		if (Random.Range (0, 10) < 5) {
			offspring.GetComponent<DNA> ().r = Random.Range (0, 10) < 5 ? dna1.r : dna2.r;
			offspring.GetComponent<DNA> ().g = Random.Range (0, 10) < 5 ? dna1.g : dna2.g;
			offspring.GetComponent<DNA> ().b = Random.Range (0, 10) < 5 ? dna1.b : dna2.b;
		} else 
		{
			offspring.GetComponent<DNA>().r = Random.Range (0.0f, 1.0f);
			offspring.GetComponent<DNA>().g = Random.Range (0.0f, 1.0f);
			offspring.GetComponent<DNA>().b = Random.Range (0.0f, 1.0f);


		}

		return offspring;

	}

	void BreedNewPopulation()
	{

		List <GameObject> newPopulation = new List<GameObject>();

		//Get rid of unfit individuals 
		List <GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

		population.Clear();
		for ( int i = (int) (sortedList.Count /2.0f) -1; i< sortedList.Count - 1; i++ )
		{
			population.Add (Breed(sortedList [i], sortedList [i + 1]));
			population.Add (Breed (sortedList [i + 1], sortedList [i]));

		}
		for (int i = 0; i < sortedList.Count; i++) {
			Destroy (sortedList [i]);
		
		}

		generation++;
	
	}
		
}
