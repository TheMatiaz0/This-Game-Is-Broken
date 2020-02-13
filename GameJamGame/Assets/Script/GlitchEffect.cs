using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GlitchEffect
{
    public static GlitchEffect[] allGlitchEffects;


    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {

        allGlitchEffects = Cyberevolver.TheReflection.GetAllType(item => Cyberevolver.TheReflection.Is(item, typeof(GlitchEffect)) && item != typeof(GlitchEffect))
            .Select(item => (GlitchEffect)Activator.CreateInstance(item))
            .ToArray();

    }
    public static GlitchEffect GetRandomGlitchEffect ()
    {
        GlitchEffect[] notEqupied =
             (from item in allGlitchEffects
             where PlayerController.Instance.CurrentGlithes.Any(elemen => elemen.GetType() == item.GetType()) == false
             select item).ToArray();

        if (notEqupied.Length == 0)
            return null;

        return notEqupied[UnityEngine.Random.Range(0, notEqupied.Length)];



    }

  
    public void Cancel()
    {
        OnCancel();
        global::Console.Instance.GetWriter().WriteLine($"<color=#00FFD5><color=#FFCD00>{Description}</color> has been repaired.\nThe game is now a little less broken.</color>");
    }
    public virtual void WhenCollect() { }
    public virtual  void Update() { }

    public virtual void FixedUpdate() { }
    public abstract string Description { get; }
    protected virtual void OnCancel() { }



}