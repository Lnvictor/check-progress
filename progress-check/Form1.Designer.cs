namespace progress_check
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            monthCalendar = new MonthCalendar();
            exerciseCheckBox = new CheckBox();
            studyCheckBox = new CheckBox();
            workWithFocusCheckBox = new CheckBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(27, 66);
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 0;
            monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.DateSelectedEvent);
            // 
            // exerciseCheckBox
            // 
            exerciseCheckBox.AutoSize = true;
            exerciseCheckBox.Location = new Point(469, 94);
            exerciseCheckBox.Name = "exerciseCheckBox";
            exerciseCheckBox.Size = new Size(99, 29);
            exerciseCheckBox.TabIndex = 1;
            exerciseCheckBox.Text = "Exercise";
            exerciseCheckBox.UseVisualStyleBackColor = true;
            // 
            // studyCheckBox
            // 
            studyCheckBox.AutoSize = true;
            studyCheckBox.Location = new Point(469, 164);
            studyCheckBox.Name = "studyCheckBox";
            studyCheckBox.Size = new Size(83, 29);
            studyCheckBox.TabIndex = 2;
            studyCheckBox.Text = "Study";
            studyCheckBox.UseVisualStyleBackColor = true;
            // 
            // workWithFocusCheckBox
            // 
            workWithFocusCheckBox.AutoSize = true;
            workWithFocusCheckBox.Location = new Point(469, 129);
            workWithFocusCheckBox.Name = "workWithFocusCheckBox";
            workWithFocusCheckBox.Size = new Size(151, 29);
            workWithFocusCheckBox.TabIndex = 3;
            workWithFocusCheckBox.Text = "Work Focused";
            workWithFocusCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(463, 288);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 4;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(workWithFocusCheckBox);
            Controls.Add(studyCheckBox);
            Controls.Add(exerciseCheckBox);
            Controls.Add(monthCalendar);
            Name = "Check Progress";
            Text = "Check Progress";
            ResumeLayout(false);
            PerformLayout();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime dt = monthCalendar.SelectionStart.Date;
            string year = dt.Year.ToString();
            string filename = AnswersPath + String.Format("\\{0}{1}.txt", dt.Month.ToString(), dt.Day.ToString());
            using (StreamWriter outputFile = new StreamWriter(filename)) {
                outputFile.WriteLine(exerciseCheckBox.Checked);
                outputFile.WriteLine(studyCheckBox.Checked);
                outputFile.WriteLine(workWithFocusCheckBox.Checked);
            }
        }

        private void DateSelectedEvent(object sender, DateRangeEventArgs e)
        {
            // Here we retrieve answers for the day
            var selectedDate = e.End.Date;
            var answers = this.GetAnswersByDate(selectedDate);

            this.exerciseCheckBox.Checked = answers.GetValueOrDefault("Exercise", false);
            this.studyCheckBox.Checked = answers.GetValueOrDefault("Study", false);
            this.workWithFocusCheckBox.Checked = answers.GetValueOrDefault("Focus", false);
        }

        private Dictionary<string, bool> GetAnswersByDate(DateTime selectedDate)
        {
            // Quando as respostas estiverem sendo armazenadas, recuperar aqui
            Dictionary<string, bool> answers = new Dictionary<string, bool>();
            answers.Add("Exercise", true);
            answers.Add("Study", true);
            answers.Add("Focus", true);

            return answers;
        }

        #endregion

        private const string AnswersPath = "C:\\Check_Progress";
        private MonthCalendar monthCalendar;
        private CheckBox exerciseCheckBox;
        private CheckBox studyCheckBox;
        private CheckBox workWithFocusCheckBox;
        private Button button1;
    }
}