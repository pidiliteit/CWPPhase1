using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeckraftBuilds.WFActivities
{
   public class WFContext
    {
     //  private List items;
       //public KeyValuePair<Argument<T>,T>
       public T GetValue<T>(Argument<T> argument) {
           if (argument == null) return default(T);
           return argument.Get();
       }

       public void SetValue<T>(Argument<T> argument, T value){
           argument.Set(value);
       }
    }
}
