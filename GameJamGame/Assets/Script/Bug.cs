using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeReferences;
using UnityEngine;

public class Bug : Collectable
{
	[ClassExtends(typeof(GlitchEffect))]
    [SerializeField]
	ClassTypeReference specialGlitchEffect = new ClassTypeReference();

	protected override void OnCollect()
	{
		PlayerController.Instance.PushBugs((GlitchEffect)Activator.CreateInstance(specialGlitchEffect));
	}

}
