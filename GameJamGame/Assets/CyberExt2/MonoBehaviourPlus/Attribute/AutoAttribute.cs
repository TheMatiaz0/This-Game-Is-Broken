using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cyberevolver.Unity
{
	/// <summary>
	/// Get Component from <c>this</c> GameObject. If Component doesn't exist then the attribute adds it automatically and uses it.
	/// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoAttribute : Attribute
    {
    }

}

