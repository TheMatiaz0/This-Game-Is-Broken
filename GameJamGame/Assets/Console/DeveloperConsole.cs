using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#pragma warning disable IDE0067
public class DeveloperConsole : AutoInstanceBehaviour<DeveloperConsole>
{

	private Cint? select = null;
	private readonly List<ProtipObject> tips = new List<ProtipObject>();
	public void Put(string text)
	{
		this.input.text = text;
		this.input.caretPosition = input.text.Length;
	}
	public static bool IsOpen => Instance != null && Instance.gameObject.activeSelf;

	[SerializeField] private Text outputEntity = null;
	[SerializeField] private ProtipObject protipObjectPrefab = null;
	[SerializeField] private Transform protipParent = null;


	private class Writer : TextWriter
	{
		public Writer(DeveloperConsole developerConsole)
		{
			DeveloperConsole = developerConsole ?? throw new ArgumentNullException(nameof(developerConsole));
		}

		public DeveloperConsole DeveloperConsole { get; }
		public override Encoding Encoding => Encoding.UTF8;
		public override void Write(char value)
		{
			DeveloperConsole.outputEntity.text += value;
		}
		public override void WriteLine()
		{
			base.WriteLine();
			DeveloperConsole.outputEntity.text += ">>	";
		}
		public override void WriteLine(char value)
		{
			DeveloperConsole.outputEntity.text += $">>	{value}\n";
		}
		public override void WriteLine(string value)
		{

			DeveloperConsole.outputEntity.text += $">>	{value}\n";
		}


	}

	[SerializeField] private InputField input = null;
	private readonly static Dictionary<string, Command> commands;
	public TextWriter GetOutput()
	{
		return new Writer(this);

	}

	protected override void Awake()
	{
		base.Awake();
		this.gameObject.SetActive(false);
	}

	protected void OnEnable()
	{
		input.Select();
	}

	public void RefuseOrClose()
	{
		outputEntity.text = string.Empty;
		outputEntity.text = "Starting a developer console ... \n";
      
		this.gameObject.SetActive(!this.gameObject.activeSelf);
        input.Select();
        input.ActivateInputField();
    }

	static DeveloperConsole()
	{
		commands = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
					from type in assembly.GetTypes()
					where type.GetCustomAttributes(true).
					OfType<CommandContainerAttribute>().
					Any()
					from method in type.GetMethods()
					let atr = method.GetCustomAttributes(true)
					.OfType<CommandAttribute>().FirstOrDefault()
					where atr != null
					select new Command(atr, method)).ToDictionary
		 (c => c.Atr.Name);

	}
	private void Start()
	{
		input.onValueChanged.AddListener(OnChange);
		input.onEndEdit.AddListener(OnEndEdit);
	}
	private void OnEndEdit(string val)
	{

		if (protipParent.GetComponentsInChildren<ProtipObject>()
			.All(item => item.IsMouseColliding == false))
			protipParent.KillAllChild();

	}
	private void OnChange(string val)
	{
		select = null;
		protipParent.KillAllChild();
		tips.Clear();
		string original = val;
		val = val.ToUpper() ?? string.Empty;
		foreach (KeyValuePair<string, Command> equalElement in
			from element in commands
			where element.Key != original && element.Key.ToUpper().Contains(val)
			select element)
		{
			var obj = Instantiate(protipObjectPrefab, protipParent);
			obj.Init(equalElement.Key);
			tips.Add(obj);
		}


	}

	protected virtual void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return))
		{
			if (select == null)
			{
				CallCommand(input.text);
				input.text = String.Empty;
				input.Select();
				input.ActivateInputField();
			}
			else
			{
				tips[(int)(uint)(Cint)select].Put();
				input.Select();
			}

		}
		if (select != null)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				if (select > 0)
					select--;
				else
					select = null;
			}
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				tips[(int)(uint)(Cint)select].Put();

			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			select = null;


		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (select != null)
			{
				select++;
			}
			else
				select = 0;
		}






		if (tips.Count != 0 && select >= tips.Count)
			select = (uint)tips.Count - 1;
		for (int i = 0; i < tips.Count; i++)
		{
			ProtipObject obj = (ProtipObject)tips[i];
			obj.TextEntity.color = i == select ? Color.yellow : Color.green;
		}



	}
	public void CallCommand(string command)
	{

		string[] splited = command.Split().ToArray();
		string errorCode = null;
		if (commands.ContainsKey(splited[0]) == false)
		{
			errorCode = "ERROR WHILE FINDING COMMAND";
		}
		else
		{
            if (commands[splited[0]].Atr.OnlyIfGameStart)
                errorCode = "Game had to start before you can use it";
            else
            {
                try
                {
                    object result = commands[splited[0]].Method.Invoke(null, new object[] { splited.Skip(1).ToArray() });
                    this.GetOutput().WriteLine($"{command}:");
                    if (result is string text)
                        this.GetOutput().WriteLine(text);
                }
                catch(System.Reflection.TargetInvocationException e)
                {
                    errorCode = e.InnerException.Message;
                }
                catch (Exception e)
                {
                    errorCode = e.Message; 
                }
            }

            

		}
		if (errorCode != null)
			this.GetOutput().WriteLine($"<color=red>{errorCode}</color>");
	}



}

