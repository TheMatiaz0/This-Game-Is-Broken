using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GlitchEffect
{
    public static GlitchEffect[] allGlitchEffects;

    public abstract string Description { get; }
    public abstract void Cancel();
    public virtual void WhenCollect() { }
    public virtual  void Update() { }

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        
        allGlitchEffects = Cyberevolver.TheReflection.GetAllType(item => Cyberevolver.TheReflection.Is(item,typeof(GlitchEffect))&& item != typeof(GlitchEffect))
            .Select(item => (GlitchEffect)Activator.CreateInstance(item))
            .ToArray();
    }

   
}