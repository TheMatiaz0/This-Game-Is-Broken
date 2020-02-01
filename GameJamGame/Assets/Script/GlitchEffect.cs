using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class GlitchEffect
{
    protected abstract string Description { get; }
    public virtual void WhenCollect() { }
    public virtual  void Update() { }

   
}