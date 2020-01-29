using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MethodDelay
{  
    public bool HasStopped { get; private set; }
    public bool IsRepeating { get; }
    public bool LimitedRepating { get; } 
    public int RepatingValue { get; set; }   
    public void Stop()
    {
        HasStopped = true;
    }
    public MethodDelay(bool isRepeating=false)
    {  
        IsRepeating = isRepeating;
    }
 
    public MethodDelay(bool limitedRepating,int repatingValue)
    {
        IsRepeating = true;
        LimitedRepating = limitedRepating;
        RepatingValue = repatingValue;
    }

   
   

}