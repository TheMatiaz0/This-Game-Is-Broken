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
 
    private List<Action> onEnd = new List<Action>();
    public void SetOnEnd(Action action)
    {
        onEnd.Add(action);
    }
    public void Stop()
    {
        HasStopped = true;
    }
    public MethodDelay(bool isRepeating=false)
    {  
        IsRepeating = isRepeating;
    }
    public void InvokeOnEnd()
    {
        foreach(var item in onEnd)
        {
            item.Invoke();
        }
    }
 
    public MethodDelay(bool limitedRepating,int repatingValue)
    {
        IsRepeating = true;
        LimitedRepating = limitedRepating;
        RepatingValue = repatingValue;
    }

   
   

}