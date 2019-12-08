using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TocalabsRDP
{
    enum InputType { Mouse, Key };
    class InputEvent
    {
        public string EventType { get; set; }

        public InputType Type;
        public int KeyCode { get; set; }

        public int DeltaX { get; set; }

        public int DeltaY { get; set; }

        public InputEvent()
        {
            switch (EventType)
            {
                case "mouse":
                    Type = InputType.Mouse;
                    break;
                case "key":
                    Type = InputType.Key;
                    break;
                default:
                    Type = InputType.Key;
                    break;
            }
        }
    }
}
