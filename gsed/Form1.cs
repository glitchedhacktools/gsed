using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gsed
{
    public partial class Form1 : Form
    {
        int CalcBase = 16;
        int CalcCurrent = 0;
        int CalcMemo = 0;
        int CalcNum1 = 0;
        int CalcNum2 = 0;
        char op = (char)0;
        bool CalcClearFlag = false;
        public Form1()
        {
            InitializeComponent();
            baseCombo.SelectedIndex = 0;
        }

        private void changeBaseLayout(object sender, EventArgs e)
        {
            switch (baseCombo.SelectedIndex)
            {
                case 0: //hex
                    calcBox.MaxLength = 9;
                    CalcButton2.Enabled = true;
                    CalcButton3.Enabled = true;
                    CalcButton4.Enabled = true;
                    CalcButton5.Enabled = true;
                    CalcButton6.Enabled = true;
                    CalcButton7.Enabled = true;
                    CalcButton8.Enabled = true;
                    CalcButton9.Enabled = true;
                    CalcButtonA.Enabled = true;
                    CalcButtonB.Enabled = true;
                    CalcButtonC.Enabled = true;
                    CalcButtonD.Enabled = true;
                    CalcButtonE.Enabled = true;
                    CalcButtonF.Enabled = true;
                    CalcButtonPTR.Enabled = true;
                    CalcButtonOFF.Enabled = true;
                    CalcButtonSIGN.Enabled = false;
                    calcBox.Text = CalcCurrent.ToString("X");
                    CalcBase = 16;
                    break;
                case 1: //dec
                    calcBox.MaxLength = 9;
                    CalcButton2.Enabled = true;
                    CalcButton3.Enabled = true;
                    CalcButton4.Enabled = true;
                    CalcButton5.Enabled = true;
                    CalcButton6.Enabled = true;
                    CalcButton7.Enabled = true;
                    CalcButton8.Enabled = true;
                    CalcButton9.Enabled = true;
                    CalcButtonA.Enabled = false;
                    CalcButtonB.Enabled = false;
                    CalcButtonC.Enabled = false;
                    CalcButtonD.Enabled = false;
                    CalcButtonE.Enabled = false;
                    CalcButtonF.Enabled = false;
                    CalcButtonPTR.Enabled = false;
                    CalcButtonOFF.Enabled = false;
                    CalcButtonSIGN.Enabled = true;
                    calcBox.Text = CalcCurrent.ToString();
                    CalcBase = 10;
                    break;
                case 2: //bin
                    calcBox.MaxLength = 8;
                    CalcButton2.Enabled = false;
                    CalcButton3.Enabled = false;
                    CalcButton4.Enabled = false;
                    CalcButton5.Enabled = false;
                    CalcButton6.Enabled = false;
                    CalcButton7.Enabled = false;
                    CalcButton8.Enabled = false;
                    CalcButton9.Enabled = false;
                    CalcButtonA.Enabled = false;
                    CalcButtonB.Enabled = false;
                    CalcButtonC.Enabled = false;
                    CalcButtonD.Enabled = false;
                    CalcButtonE.Enabled = false;
                    CalcButtonF.Enabled = false;
                    CalcButtonPTR.Enabled = false;
                    CalcButtonOFF.Enabled = false;
                    CalcButtonSIGN.Enabled = false;
                    calcBox.Text = ConvertToBin(CalcCurrent).ToString();
                    CalcBase = 2;
                    break;
            }
            CalcClearFlag = true;

        }

        private int ConvertToBin(int current)
        {
            int result = current;
            int exp = 10;
            int[] array = { 0, 0, 0, 0, 0, 0, 0, 0 };
            int i;
            for (i=0; i < 8; i++)
            {
                array[i] = result % 2;
                result = result / 2;
            }
            result = 0;
            for (i=0; i<8; i++)
            {
                result = result + array[i] * exp / 10;
                exp = exp * 10;
            }
            return result;
        }

        private void CalcTypeNew(object sender, KeyPressEventArgs e)
        {
            int diez = 10;
            if (calcBox.Text.Length <= calcBox.MaxLength)
            {
                switch (baseCombo.SelectedIndex)
                {
                    case 0:
                        CalcCurrent = int.Parse(calcBox.Text, System.Globalization.NumberStyles.HexNumber);
                        diez = 0x10;
                        break;
                    case 1:
                        CalcCurrent = int.Parse(calcBox.Text);
                        break;
                    case 2:
                        CalcCurrent = int.Parse(calcBox.Text);
                        //CalcCurrent = Convert.ToInt32(CalcCurrent.ToString(), 2);
                        break;
                    default: break;
                }
                if (CalcClearFlag)
                {
                    CalcCurrent = 0;
                    CalcClearFlag = false;
                }

                if (e.KeyChar >= 'A' && e.KeyChar <= 'F' && baseCombo.SelectedIndex == 0)
                {
                    CalcCurrent = CalcCurrent * diez + e.KeyChar - 55;
                    e.Handled = true;
                }
                if (e.KeyChar >= '0' && e.KeyChar <= '1')
                {
                    CalcCurrent = CalcCurrent * diez + e.KeyChar - 48;
                    e.Handled = true;
                }
                if (e.KeyChar >= '2' && e.KeyChar <= '9' && baseCombo.SelectedIndex <= 1)
                {
                    CalcCurrent = CalcCurrent * diez + e.KeyChar - 48;
                    e.Handled = true;
                }
                if (e.KeyChar >= 'a' && e.KeyChar <= 'f' && baseCombo.SelectedIndex == 0)
                {
                    CalcCurrent = CalcCurrent * diez + e.KeyChar - 87;
                    e.Handled = true;
                }
                switch (baseCombo.SelectedIndex)
                {
                    case 0: calcBox.Text = CalcCurrent.ToString("X"); break;
                    case 1: calcBox.Text = CalcCurrent.ToString(); break;
                    case 2: calcBox.Text = CalcCurrent.ToString(); break;
                    default: break;
                }
                if (e.KeyChar == '+')
                {
                    if (CalcNum1 > 0)
                    {
                        CalcNum2 = CalcCurrent;
                        //CalcCurrent = sum(CalcNum1, CalcNum2);
                    }
                    if (CalcNum1 == 0) CalcNum1 = CalcCurrent;
                    op = '+';
                    CalcClearFlag = true;
                    e.Handled = true;
                }
                if (e.KeyChar == '-')
                {
                    if (CalcNum1 > 0)
                    {
                        CalcNum2 = CalcCurrent;
                        //CalcCurrent = sus(CalcNum1, CalcNum2);
                    }
                    if (CalcNum1 == 0) CalcNum1 = CalcCurrent;
                    op = '-';
                    CalcClearFlag = true;
                    e.Handled = true;
                }
                if (e.KeyChar == '*')
                {
                    if (CalcNum1 > 0)
                    {
                        CalcNum2 = CalcCurrent;
                        //CalcCurrent = mult(CalcNum1, CalcNum2);
                    }
                    if (CalcNum1 == 0) CalcNum1 = CalcCurrent;
                    op = '*';
                    CalcClearFlag = true;
                    e.Handled = true;
                }
                if (e.KeyChar == '/')
                {
                    if (CalcNum1 > 0)
                    {
                        CalcNum2 = CalcCurrent;
                        //CalcCurrent = div(CalcNum1, CalcNum2);
                    }
                    if (CalcNum1 == 0) CalcNum1 = CalcCurrent;
                    op = '/';
                    CalcClearFlag = true;
                    e.Handled = true;
                }
            }
        }

        private void CalcActivateBin(object sender, EventArgs e)
        {
            if (CalcCurrent > 0xFF)
            {
                if (baseCombo.Items.Count > 2)
                {
                    baseCombo.Items.RemoveAt(2);
                }
            }
            else
            {
                if (baseCombo.Items.Count == 2)
                {
                    baseCombo.Items.Add("BIN");
                }
            }
        }

        private void CalcType2(object sender, KeyEventArgs e)
        {
            int diez = 10;
            switch (baseCombo.SelectedIndex)
            {
                case 0:
                    CalcCurrent = int.Parse(calcBox.Text, System.Globalization.NumberStyles.HexNumber);
                    diez = 0x10;
                    break;
                case 1:
                    CalcCurrent = int.Parse(calcBox.Text);
                    break;
                case 2:
                    CalcCurrent = int.Parse(calcBox.Text);
                    break;
                default: break;
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (CalcCurrent == 0) CalcNum1 = 0;
                CalcCurrent = 0;
            }
            if (e.KeyCode == Keys.Back)
            {
                CalcCurrent/= diez;
            }
            if (e.KeyCode == Keys.Enter)
            {
                CalcNum2 = CalcCurrent;
                switch (op)
                {
                    case '+': CalcCurrent = sum(CalcNum1, CalcNum2); break;
                    case '-': CalcCurrent = sus(CalcNum1, CalcNum2); break;
                    case '*': CalcCurrent = mult(CalcNum1, CalcNum2); break;
                    case '/': CalcCurrent = div(CalcNum1, CalcNum2); break;
                }
                CalcNum1 = CalcCurrent;
                CalcClearFlag = true;
            }
            switch (baseCombo.SelectedIndex)
            {
                case 0: calcBox.Text = CalcCurrent.ToString("X"); break;
                case 1: calcBox.Text = CalcCurrent.ToString(); break;
                case 2: calcBox.Text = CalcCurrent.ToString(); break;
                default: break;
            }
        }

        int sum (int num1, int num2)
        {
            return num1 + num2;
        }
        int sus(int num1, int num2)
        {
            return num1 - num2;
        }
        int mult(int num1, int num2)
        {
            return num1 * num2;
        }
        int div(int num1, int num2)
        {
            try
            {
                return num1 / num2;
            }catch
            {
                return 0;
            }
        }
    }
}
