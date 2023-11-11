using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class WordBank : MonoBehaviour
{
    public class MyWord
    {
        public MyWord(string value, string pieces)
        {
            this.Value = value;
            this.Pieces = pieces;
        }
        public string Value { get; set; }
        public string Pieces { get; set; }
    }

    private List<string> workingWords = new List<string>();


    private List<MyWord> wordList = new List<MyWord>();

    public void Init()
    {

        var data = System.IO.File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\game\words.json");
        wordList = JsonConvert.DeserializeObject<List<MyWord>>(data);

        workingWords = wordList.Select(a => a.Pieces.ToUpper())
            .Randomize()
            .ToList();
    }
    private void Awake()
    {
        Init();
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if (workingWords.Count != 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }

        if (workingWords.Count == 0)
        {
            Init();
        }

        return newWord;
    }

    public string GetImagePath(string word)
    {
        var myWord = wordList.FirstOrDefault(a => a.Pieces.ToLower() == word.ToLower());
        var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\game\images\{myWord.Value.ToLower()}.png";
        if (System.IO.File.Exists(path))
        {
            return path;
        }
        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\game\images\noimage.jpg"; ;
    }

    public string GetFullWord(string pieces)
    {
        return wordList.FirstOrDefault(a => a.Pieces.ToLower() == pieces.ToLower()).Value.ToUpper();
    }
}

public static class Extensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        System.Random rnd = new System.Random();
        return source.OrderBy((item) => rnd.Next());
    }
}
