using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class TemplateTextVisibility : GlitchEffect
{
	private Text thisText = null;

	private Task fontSizeChange;
	private Task replaceCharacters;

	public override string Description => "Boolean \"isVisible\" is missing.";

	protected override void OnCancel()
	{
		fontSizeChange.Dispose();
		replaceCharacters.Dispose();

		thisText.enabled = false;	
	}

	public override void WhenCollect()
	{
		thisText = TemplateTextSingleton.Instance.TemplateText;

		thisText.enabled = true;

		fontSizeChange = FontSizeChange(thisText);
		replaceCharacters = ReplaceCharacters(thisText);
	}

	private async Task FontSizeChange (Text text)
	{
		text.fontSize = 1;

		while (text.fontSize < 255)
		{
			text.fontSize += 1;
			await Async.Wait(TimeSpan.FromSeconds(0.08));
		}
	}

	private async Task ReplaceCharacters (Text text)
	{
		text.text = "Template Text";
		char[] alphabet = "abcdefghijklmnopqstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_+-=[]{}'.,|".ToCharArray();

		while (true)
		{
			string previousText = text.text;
			char choosedChar = text.text[UnityEngine.Random.Range(0, text.text.Length)];
			char changedChar = alphabet[UnityEngine.Random.Range(0, alphabet.Length)];
			text.text = text.text.Replace(choosedChar, changedChar);

			await Async.Wait(TimeSpan.FromSeconds(0.1d));

			text.text = previousText;
		}
	}
}
