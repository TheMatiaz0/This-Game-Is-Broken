using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeReferences;
using UnityEngine;

public class Bug : Collectable
{
    public override bool IsBad => true;
    /*
	[ClassExtends(typeof(GlitchEffect))]
    [SerializeField]
	ClassTypeReference specialGlitchEffect = new ClassTypeReference();
	*/

    protected override void OnCollect()
	{
		int rndNumber = UnityEngine.Random.Range(0, (GlitchEffect.allGlitchEffects.Length));
		PlayerController.Instance.PushBugs(GlitchEffect.allGlitchEffects[rndNumber]);

	}

}
