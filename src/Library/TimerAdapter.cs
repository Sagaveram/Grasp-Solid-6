using System;
using System.Threading;

namespace Full_GRASP_And_SOLID
{
    

    public class TimerAdapter:TimerClient
    {
        
        public Recipe recipe{get;set;}
    
        
        public void TimeOut()
        {
            recipe.CookedTrue();    
            
        }
    

    }

}