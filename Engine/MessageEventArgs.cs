using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class MessageEventArgs : EventArgs
    /*The ": EventArgs" is for this class to inherit from the base EventArgs class, a built-in class for
     event notifications. All custom event argument classes need to inherit from EventArgs.*/
    {
        public string aMessage { get; private set; }
        public bool addAnExtraNewLine { get; private set; }

        public MessageEventArgs(string message, bool addExtraNewLine)
        {
            aMessage = message;
            addAnExtraNewLine = addExtraNewLine;
        }
    }
}
