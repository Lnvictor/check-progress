namespace progress_check
{
    partial class MainForm
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
            saveButton = new Button();
            SuspendLayout();

            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(27, 66);
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 0;
            monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.DateSelectedEvent);
            UpdateBoldedDates();

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
            saveButton.Location = new Point(463, 288);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(112, 34);
            saveButton.TabIndex = 4;
            saveButton.Text = "Salvar";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += Save_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(saveButton);
            Controls.Add(workWithFocusCheckBox);
            Controls.Add(studyCheckBox);
            Controls.Add(exerciseCheckBox);
            Controls.Add(monthCalendar);
            Name = "Check Progress";
            Text = "Check Progress";
            ResumeLayout(false);
            PerformLayout();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            DateTime dt = monthCalendar.SelectionStart.Date;
            string year = dt.Year.ToString();
            string filename = $"{AnswersPath}\\{dt.Month.ToString()}{dt.Day.ToString()}.txt";
            using (StreamWriter outputFile = new StreamWriter(filename)) {
                outputFile.WriteLine(exerciseCheckBox.Checked);
                outputFile.WriteLine(workWithFocusCheckBox.Checked);
                outputFile.WriteLine(studyCheckBox.Checked);
            }

            if (exerciseCheckBox.Checked && workWithFocusCheckBox.Checked && studyCheckBox.Checked)
            {
                monthCalendar.AddBoldedDate(dt);
            }
            else
            {
                monthCalendar.RemoveBoldedDate(dt);
            }
            monthCalendar.UpdateBoldedDates();
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

            string answersFullPath = $"{AnswersPath}\\{selectedDate.Month.ToString()}{selectedDate.Day.ToString()}.txt";
            Dictionary<string, bool> answers = new Dictionary<string, bool>();

            if (File.Exists(answersFullPath))
            {
                using (StreamReader inputFile = new StreamReader(answersFullPath))
                {
                    string text = inputFile.ReadToEnd();
                    string[] lines = text.Split('\n');

                    answers.Add("Exercise", Boolean.Parse(lines[0]));
                    answers.Add("Study", Boolean.Parse(lines[1]));
                    answers.Add("Focus", Boolean.Parse(lines[2]));
                }
            }

            return answers;
        }

        private void UpdateBoldedDates()
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            int currentDay = DateTime.Now.Day;


            for (int i = 1; i <= currentDay; i++) 
            {
                string answersFullPath = $"{AnswersPath}\\{currentMonth}{i}.txt";

                if (File.Exists(answersFullPath))
                {
                    using (StreamReader inputFile = new StreamReader(answersFullPath))
                    {
                        string[] lines = inputFile.ReadToEnd().Split('\n');
                        bool Exercise = Boolean.Parse(lines[0]);
                        bool Study = Boolean.Parse(lines[1]);
                        bool Focus = Boolean.Parse(lines[2]);

                        if (Exercise && Study && Focus) 
                        {
                            monthCalendar.AddBoldedDate(new DateTime(currentYear, currentMonth, i));
                        }
                    }
                }
            }

            monthCalendar.UpdateBoldedDates();
        }

        #endregion

        private const string AnswersPath = "C:\\Check_Progress";
        private MonthCalendar monthCalendar;
        private CheckBox exerciseCheckBox;
        private CheckBox studyCheckBox;
        private CheckBox workWithFocusCheckBox;
        private Button saveButton;
    }
}