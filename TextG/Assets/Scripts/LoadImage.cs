using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadImage : MonoBehaviour
{
    [SerializeField] private Image image;
    public Typer typer = null;
    void Start()
    {   string SuperWord = string.Empty;
        SuperWord = typer.SetWordForImage();
        SuperWord=SuperWord.ToUpper();
        var myTexture = Resources.Load<Texture2D>($"Images/{SuperWord}");
        Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5F));

        image.sprite = newSprite;
    }
}
