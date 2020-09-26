﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class MathQuiz : Form
    {
        /**
         * Create a Random object called randomizer
         * to generate random numbers.
         */
        Random randomizer = new Random();

        /**
         * These integer variables store the numbers
         * for the addition problem.
         */
        int addend1, addend2;

        /**
         * These integer variables store the numbers
         * for the subtraction problem.
         */
        int minuend, subtrahend;

        /**
         * These integer variables store the numbers
         * for the subtraction problem.
         */
        int multiplicand, multiplier;

        /**
         * These integer variables store the numbers
         * for the subtraction problem.
         */
        int dividend, divisor;

        /**
         * This integer variable keeps track of the
         * remaining time
         */
        int timeLeft;

        public MathQuiz()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start the quiz by filling in all pf the problems
        /// and starting the timer
        /// </summary
        public void StartQuiz()
        {
            // Fill in the addition problem.
            /**
             * Generate two random numbers to add.
             * Store the values in the variables 'addend1' and 'addend2'.
             */
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            /**
             * Convert the two randomly generated numbers
             * into strings so that they can be displayed
             * in the label controls.
             */
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            /**
             * 'sum' is the name of the NumericUpDown control.
             * This step makes sure its value is zero before
             * adding any values to it.
             */
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /// <summary>
        /// Check the answer to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void MathQuiz_Load(object sender, EventArgs e)
        {
            /**
             * Defining the current date in the form
             * when its load.
             */
            string currentDateValue = DateTime.Now.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            lblDate.Text = currentDateValue;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                /**
                 * If CheckAnswer() returns true, then the user
                 * got the answer right. Stop the timer
                 * and show a MessageBox
                 */
                timer1.Stop();
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 6)
            {
                /**
                 * Display the new time left
                 * up updating the Time Left label.
                 */
                timeLeft--;
                timeLabel.Text = $"{timeLeft} seconds";
            }
            else if (timeLeft > 0)
            {
                /**
                 * Display the new time left
                 * up updating the Time Left label
                 * and change the back color to red.
                 */
                timeLabel.BackColor = Color.Red;
                timeLeft--;
                timeLabel.Text = $"{timeLeft} seconds";
            }
            else
            {
                /**
                 * If the user ran out of time, to stop the timer, show
                 * a MessageBox, and fill in the answers.
                 */
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartQuiz();
            startButton.Enabled = false;
        }
    }
}
