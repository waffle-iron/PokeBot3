using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGo.RocketAPI.Logic
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }
        public void check(int x)
        {
            switch (x)
            {
                case 1:
                    this.checkBox1.CheckState = CheckState.Checked;
                    break;
                case 2:
                    this.checkBox2.CheckState = CheckState.Checked;
                    break;
                case 3:
                    this.checkBox3.CheckState = CheckState.Checked;
                    break;
                case 4:
                    this.checkBox4.CheckState = CheckState.Checked;
                    break;
                case 5:
                    this.checkBox5.CheckState = CheckState.Checked;
                    break;
                case 6:
                    this.checkBox6.CheckState = CheckState.Checked;
                    break;
            }
            
        }
        public void uncheck(int x)
        {
            switch (x)
            {
                case 1:
                    this.checkBox1.CheckState = CheckState.Unchecked;
                    break;
                case 2:
                    this.checkBox2.CheckState = CheckState.Unchecked;
                    break;
                case 3:
                    this.checkBox3.CheckState = CheckState.Unchecked;
                    break;
                case 4:
                    this.checkBox4.CheckState = CheckState.Unchecked;
                    break;
                case 5:
                    this.checkBox5.CheckState = CheckState.Unchecked;
                    break;
                case 6:
                    this.checkBox6.CheckState = CheckState.Unchecked;
                    break;
            }

        }
    }
}
