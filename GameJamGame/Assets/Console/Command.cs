using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Command
{
    public Command(CommandAttribute atr, MethodInfo method)
    {
        this.Atr = atr ?? throw new ArgumentNullException(nameof(atr));
        Method = method ?? throw new ArgumentNullException(nameof(method));
    }

    public CommandAttribute Atr { get;}
    public MethodInfo Method { get; }
}