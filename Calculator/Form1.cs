using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Double resultValue = 0;
        String operationDone = "";
        bool isOperationPerformed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if((textBox_Result.Text == "0") || (isOperationPerformed))
            /*This clears the zero that shows by default.*/
            {
                textBox_Result.Clear();
            }

            isOperationPerformed = false;

            Button aButton = (Button)sender;
            /*This performs multiple operations with a single event handler.﻿*/

            if(aButton.Text == ".")
            {
                if (!textBox_Result.Text.Contains("."))
                {
                    textBox_Result.Text = textBox_Result.Text + aButton.Text;
                }
            }
            else
            {
                textBox_Result.Text = textBox_Result.Text + aButton.Text;
            }
        }

        private void arithmetic_click(object sender, EventArgs e)
        {
            Button aButton = (Button)sender;

            if (resultValue != 0)
            {
                button19.PerformClick();
                /*This will do the switch statements*/

                operationDone = aButton.Text;
                resultValue = Double.Parse(textBox_Result.Text);
                labelCurrentOperation.Text = resultValue + " " + operationDone;
                isOperationPerformed = true;
            }
            else
            {
                operationDone = aButton.Text;
                /*This passes aButton to operationDone*/

                resultValue = Double.Parse(textBox_Result.Text);
                /*Converts the textBox_Result.txt to the Double value*/

                labelCurrentOperation.Text = resultValue + " " + operationDone;

                isOperationPerformed = true;
            }
        }

        private void button_ClickDot(object sender, EventArgs e)
        {
            textBox_Result.Text = textBox_Result.Text + ".";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            resultValue = 0;
        }

        private void equals_Click(object sender, EventArgs e)
        {
            switch(operationDone)
            /*This switch statement is used to perform the calculations*/
            {
                case "+":
                    textBox_Result.Text = (resultValue + Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "-":
                    textBox_Result.Text = (resultValue - Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "*":
                    textBox_Result.Text = (resultValue * Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "/":
                    textBox_Result.Text = (resultValue / Double.Parse(textBox_Result.Text)).ToString();
                    break;
                default:
                    break;
            }
            resultValue = Double.Parse(textBox_Result.Text);
            labelCurrentOperation.Text = "";
        }
    }
}
