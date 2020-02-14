using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Threading;

public class TemplateTextVisibility : GlitchEffect
{
	private Text thisText = null;
	private CancellationTokenSource tokenSource;

	public override string Description => "Boolean \"isVisible\" is missing.";

	protected override void OnCancel()
	{
		tokenSource.Cancel();

		thisText.enabled = false;	
	}

	public override void WhenCollect()
	{
		tokenSource = new CancellationTokenSource();
		thisText = TemplateTextSingleton.Instance.TemplateText;

		thisText.enabled = true;

		_ = FontSizeChange(tokenSource.Token, thisText);
		_ = ReplaceCharacters(tokenSource.Token, thisText);
	}

	private async Task FontSizeChange (CancellationToken token, Text text)
	{
		text.fontSize = 1;

		while (text.fontSize < 255 && token.IsCancellationRequested == false)
		{
			text.fontSize += 1;
			await Async.Wait(TimeSpan.FromSeconds(0.08));
		}
	}

	private async Task ReplaceCharacters (CancellationToken token, Text text)
	{
		text.text = "Template Text";
		char[] alphabet = "abcdefghijklmnopqstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_+-=[]{}'.,|".ToCharArray();

		while (token.IsCancellationRequested == false)
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
