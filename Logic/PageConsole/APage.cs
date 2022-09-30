using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyConsMenu
{
    public abstract class APage : IPage
    {
        public virtual string title { get; protected set; }
        public virtual string content { get; protected set; }
        public virtual string footer { get; protected set; }
        public virtual void Display(string footer)
        {
			MyConsole.DisplayPage(this);
        }
    }
}
