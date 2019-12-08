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
        private string _eventType;
        public string EventType {
            get
            {
                return _eventType;
            }
            set
            {
                _eventType = value;
                switch (value)
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

        public InputType Type;
        public int KeyCode { get; set; }

        public int DeltaX { get; set; }

        public int DeltaY { get; set; }
    }
}
