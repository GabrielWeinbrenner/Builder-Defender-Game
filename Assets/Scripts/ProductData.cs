using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ProductData 
{
    public string nameString;
    public Sprite sprite;
    public int price;
    public float x;
    public float y;
	//public Color color;
	//public Color replacedColor;

	public ProductData () {
        Sprite[] allSprites = Resources.LoadAll("Products", typeof(Sprite)).Cast<Sprite>().ToArray();
        price = UtilsClass.RoundToHundreths(UnityEngine.Random.Range(300,10000));
        sprite = allSprites[UnityEngine.Random.Range(0, allSprites.Length)];
        nameString = sprite.name;
        //color = UnityEngine.Random.ColorHSV();
        //replacedColor = UtilsClass.AverageColorFromTexture(sprite);
    }
    public void setX(float x) {
        this.x = x;
    }
    public void setY(float y) {
        this.y = y;
    }
}
