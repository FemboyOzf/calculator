using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Calculator_with_gui
{
    public partial class MainWindow : Window
    {
        private bool isNewCalculation = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // You can implement additional logic here if needed
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            textBox.Text += button.Content.ToString();
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (isNewCalculation)
            {
                textBox.Text += (sender as Button).Content.ToString();
                isNewCalculation = false;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (isNewCalculation)
                return;

            string expression = textBox.Text;

            // Split the expression based on operators
            string[] symbols = { "+", "-", "*", "/" };
            string[] operands = expression.Split(symbols, StringSplitOptions.RemoveEmptyEntries);

            // Validate the expression
            if (operands.Length < 2)
                return;

            double result = double.Parse(operands[0]);
            int currentOperandIndex = 1;

            // Iterate through the expression and perform calculations
            foreach (char c in expression)
            {
                if (Array.Exists(symbols, s => s[0] == c))
                {
                    string currentOperator = c.ToString();
                    double currentOperand = double.Parse(operands[currentOperandIndex]);

                    switch (currentOperator)
                    {
                        case "+":
                            result += currentOperand;
                            break;
                        case "-":
                            result -= currentOperand;
                            break;
                        case "*":
                            result *= currentOperand;
                            break;
                        case "/":
                            if (currentOperand == 0)
                            {
                                MessageBox.Show("Error: Division by zero.");
                                return;
                            }
                            result /= currentOperand;
                            break;
                    }

                    currentOperandIndex++;
                }
            }

            textBox.Text = result.ToString();
            isNewCalculation = true;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Clear();
            isNewCalculation = true;
        }
    }
}
