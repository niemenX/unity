using System.Linq;
using System.Collections.Generic;
using UnityEngine;




public class WordBank : MonoBehaviour
{

    private List<string> originalWords = new List<string>()
    {
        "ca-sa", "ma-sa", "car-ca-sa", "ba-lon", "ac", "va-ca", "nu-ca", "mar", "ca-na", "ro-bot", "pa-na", "oa-ie"
    };

    private List<string> workingWords = new List<string>();

    private void Awake()
    {
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConverToUpper(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConverToUpper(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
            list[i] = list[i].ToUpper();
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if(workingWords.Count != 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }

        if(workingWords.Count == 0)
        {
            Awake();
        }

        return newWord;
    }
}
