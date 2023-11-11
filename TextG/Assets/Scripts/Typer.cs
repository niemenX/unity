using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TMP_Text wordOutput = null;
    public TMP_Text input = null;
    public TMP_Text lastKey = null;
    public Image image = null;

    private string remainingWord = string.Empty;
    public string inputWord = string.Empty;
    private string currentWord = string.Empty;

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        inputWord = currentWord;
        input.text = wordBank.GetFullWord(currentWord);
        SetRemainingWord(currentWord);
        SetImage(currentWord);
    }

    private void SetImage(string word)
    {
        if (!string.IsNullOrEmpty(word))
        {
            
            byte[] bytes = File.ReadAllBytes(wordBank.GetImagePath(word));
            Texture2D loadTexture = new Texture2D(1, 1); //mock size 1x1
            loadTexture.LoadImage(bytes);
            image.sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), new Vector2(0.5f, 0.5F)); 

        }
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
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            keysPressed = keysPressed.ToUpper();

            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);

                string res = remainingWord.Substring(0, 1); //sare peste -

                if (res == "-")
                {
                    RemoveLetter();

                    if (IsWordComplete())
                        SetCurrentWord();
                }
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (typedLetter != "-")
        {
            lastKey.text = typedLetter.ToUpper();
        }
        if (IsCorrectLetter(typedLetter))
        {
            lastKey.color = Color.green;
            RemoveLetter();

            if (IsWordComplete())
            {
                SetCurrentWord();
                lastKey.text = "";
            }
        }
        else
        {
            lastKey.color = Color.red;
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


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void ResetGame()
    {
        wordBank.Init();
        SetCurrentWord();
    }
}
