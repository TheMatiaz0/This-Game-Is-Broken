using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public  sealed class CommandAttribute:Attribute
{
    public CommandAttribute(string name,bool onlyIfGameStart=true)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        OnlyIfGameStart = onlyIfGameStart;
    }
    public bool OnlyIfGameStart { get; }

    public string Name { get; }
    
}