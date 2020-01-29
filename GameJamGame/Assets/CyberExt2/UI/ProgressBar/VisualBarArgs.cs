using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver.Unity
{
    public class VisualBarArgs:EventArgs
    {
        public VisualBar VisualBar { get; }
        public Percent Preferred { get; }
        public Percent Showing { get; }
        public VisualBarArgs(VisualBar visualBar, Percent preffered, Percent showing)
        {
            VisualBar = visualBar != null ? visualBar : throw new ArgumentNullException(nameof(visualBar));
            Preferred = preffered;
            Showing = showing;
        }

       
      

        
    }
}
