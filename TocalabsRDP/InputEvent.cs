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
        public string Type { get; set; }

        private InputType inputType;
        public int KeyCode { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public void Run()
        {
            switch (Type)
            {
                case "mouse":
                    inputType = InputType.Mouse;
                    break;
                case "key":
                    inputType = InputType.Key;
                    break;
                default:
                    inputType = InputType.Key;
                    break;
            }

            if(inputType == InputType.Key)
            {

            }
        }
    }
}
