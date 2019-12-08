using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;
using WindowsInput;

namespace TocalabsRDP
{
    class InputControl
    {
        private InputSimulator input;

        public InputControl()
        {
            input = new InputSimulator();
        }

        public void RunEvent(InputEvent ie)
        {
            if(ie.Type == InputType.Key)
            {
                input.Keyboard.KeyPress((VirtualKeyCode)ie.KeyCode);
            }
            if(ie.Type == InputType.Mouse)
            {
                input.Mouse.MoveMouseBy(ie.DeltaX, ie.DeltaY);
            }
        }
    }
}
