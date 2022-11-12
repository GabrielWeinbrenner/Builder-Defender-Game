using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsClass {
	public static int RoundToHundreths (int num) {
		double temp = Convert.ToDouble(num);
		return Convert.ToInt32(Math.Round(temp / 100d, 0) * 100);
	}
	public static bool GetRandomBool () {
		int randomNumber = UnityEngine.Random.Range(0, 100);
		return (randomNumber % 2 == 0) ? true : false;
	}
	public static Color AverageColorFromTexture (Sprite sprite) {
		var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
		var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
												(int)sprite.textureRect.y,
												(int)sprite.textureRect.width,
												(int)sprite.textureRect.height);
		croppedTexture.SetPixels(pixels);
		croppedTexture.Apply();
		Color[] texColors = croppedTexture.GetPixels();

		int total = texColors.Length;

		float r = 0;
		float g = 0;
		float b = 0;
		int offset = 0;
		for(int i = 0; i < 10000; i++) { 
			Debug.Log(texColors[i]);
		}
		for (int i = 0; i < total; i++) {
				r += texColors[i].r;
				g += texColors[i].g;
				b += texColors[i].b;

		}
		total = total - offset;
		Debug.Log(r + " " + g + " " + b);
		Color average = new Color((byte)(r / total), (byte)(g / total), (byte)(b / total), 0);
		Debug.Log(average);
		return average;

	}

}
