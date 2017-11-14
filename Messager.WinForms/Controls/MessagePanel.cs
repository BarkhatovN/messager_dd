using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messager.WinForms.Controls
{
    public class MessagePanel : Panel
    {
        public MessagePanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
