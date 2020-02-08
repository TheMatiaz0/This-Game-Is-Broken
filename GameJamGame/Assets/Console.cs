using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver.Unity;
using System.IO;
using System.Text;

public class Console : AutoInstanceBehaviour<Console>
{
	[SerializeField] private Text outputText = null;
    public ConsoleWriter GetWriter()=> new ConsoleWriter();
    public class ConsoleWriter:TextWriter
    {
        public string HtmlColor { get; set; } = "red";
        public override Encoding Encoding => Encoding.UTF8;

        private string GetColorText(object text)
        {
            return $"<color={HtmlColor}>{text.ToString()}</color>";
        }

        public override void Write(char value)
        {
            Console.Instance.outputText.text += GetColorText(value);
        }
        public override void WriteLine(char value)
        {
            Console.Instance.outputText.text += $"{ GetColorText(value)}\n";
        }
        public override void Write(string value)
        {
            Console.Instance.outputText.text += GetColorText(value);

        }
        public override void WriteLine(string value)
        {
            Console.Instance.outputText.text += $"{ GetColorText(value)}\n";
        }
    }

}
