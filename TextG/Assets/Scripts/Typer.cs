using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public Text wordOutput = null;

    private string remainingWord = string.Empty;
    public string SuperWord = string.Empty;
    private string currentWord = string.Empty;

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SuperWord = currentWord;
        SetRemainingWord(currentWord);
        SetWordForImage();
      //  SetRemoveLiniuta();
    }

    public string SetWordForImage()
    {
        SuperWord = currentWord;
        return SuperWord;
    }

    public string SetRemoveLiniuta()
    {
        string SuperWord = string.Empty;
        SuperWord = currentWord.Replace("-", string.Empty);
        return SuperWord;
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            keysPressed = keysPressed.ToUpper();

            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);

            string res = remainingWord.Substring(0, 1); //sare peste -
        
            if(res == "-")
            {
                RemoveLetter();  

                if (IsWordComplete())
                SetCurrentWord();          
            }   

            
        }
    }

    private void EnterLetter(string typedLetter)
    {
        
      
        
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
                SetCurrentWord();
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {       
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
