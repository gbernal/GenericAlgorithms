using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_bot : MonoBehaviour {

    List<int> genes_bot = new List<int>();
    int dnaLength = 0;
    int maxValues = 0;

    public DNA_bot(int l, int v)
    {

        dnaLength = l;
        maxValues = v;
        SetRandom();
    }

    public void SetRandom()
    {
        genes_bot.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes_bot.Add(Random.Range(0, maxValues));
        }
    }

    public void Combine(DNA_bot d1, DNA_bot d2)
    {
        for (int i = 0; i < dnaLength; i++)
        {
            if (i < dnaLength / 2.0)
            {
                int c = d1.genes_bot[i];
                genes_bot[i] = c;
            }
            else
            {
                int c = d2.genes_bot[i];
                genes_bot[i] = c;

            }
        }
    }

    public void Mutate()
    {
        genes_bot[Random.Range(0, dnaLength)] = Random.Range(0, maxValues);
    }


    public int GetGene(int pos)
    {
        return genes_bot[pos];
    }
}
