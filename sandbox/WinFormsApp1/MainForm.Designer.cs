namespace WinFormsApp1;

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
        maskedTextBox1 = new MaskedTextBox();
        textBox1 = new TextBox();
        button1 = new Button();
        comboBox1 = new ComboBox();
        checkBox1 = new CheckBox();
        SuspendLayout();
        // 
        // maskedTextBox1
        // 
        maskedTextBox1.AllowDrop = true;
        maskedTextBox1.Location = new Point(12, 12);
        maskedTextBox1.Name = "maskedTextBox1";
        maskedTextBox1.Size = new Size(100, 32);
        maskedTextBox1.TabIndex = 0;
        // 
        // textBox1
        // 
        textBox1.AllowDrop = true;
        textBox1.Location = new Point(12, 60);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(100, 32);
        textBox1.TabIndex = 1;
        // 
        // button1
        // 
        button1.Location = new Point(146, 7);
        button1.Name = "button1";
        button1.Size = new Size(107, 40);
        button1.TabIndex = 2;
        button1.Text = "button1";
        button1.UseVisualStyleBackColor = true;
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[] { "Item1", "Item2", "Item3" });
        comboBox1.Location = new Point(146, 60);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new Size(121, 33);
        comboBox1.TabIndex = 3;
        // 
        // checkBox1
        // 
        checkBox1.AutoSize = true;
        checkBox1.Location = new Point(289, 12);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new Size(120, 29);
        checkBox1.TabIndex = 4;
        checkBox1.Text = "checkBox1";
        checkBox1.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        AllowDrop = true;
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(584, 361);
        Controls.Add(checkBox1);
        Controls.Add(comboBox1);
        Controls.Add(button1);
        Controls.Add(textBox1);
        Controls.Add(maskedTextBox1);
        Margin = new Padding(2);
        Name = "MainForm";
        Text = "Form1";
        DragDrop += MainForm_DragDrop;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MaskedTextBox maskedTextBox1;
    private TextBox textBox1;
    private Button button1;
    private ComboBox comboBox1;
    private CheckBox checkBox1;
}
