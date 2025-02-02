using R3;
using R3Utility.WinForms;
using System.Diagnostics;

namespace WinFormsApp1;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        textBox1.TextChangedAsObservable()
                .Debounce(TimeSpan.FromMilliseconds(500))
                .Subscribe(_ => Debug.WriteLine($"textBox1 Changed:{textBox1.Text}"));

        textBox1.DragEnterAsObservable()
                .Subscribe(e => e.Effect = DragDropEffects.Copy);

        textBox1.DragDropAsObservable()
        .Subscribe(e =>
                {
                    textBox1.Text = e.Data?.GetData(DataFormats.FileDrop) is string[] files
                        ? string.Join(Environment.NewLine, files)
                        : e.Data?.GetData(DataFormats.Text)?.ToString();
                });

        maskedTextBox1.TextChangedAsObservable()
                .Debounce(TimeSpan.FromMilliseconds(500))
                .Subscribe(_ => Debug.WriteLine($"maskedTextBox1 Changed:{maskedTextBox1.Text}"));

        button1.ClickAsObservable()
               .Subscribe(_ => ButtonClick());

        comboBox1.SelectedIndexChangedAsObservable()
                 .Subscribe(x => Debug.WriteLine($"comboBox1 SelectedIndexChanged:{comboBox1.SelectedIndex}"));

        comboBox1.SelectionChangeCommittedAsObservable()
                 .Subscribe(x => Debug.WriteLine($"comboBox1 SelectionChangeCommitted:{comboBox1.SelectedIndex}"));

        checkBox1.CheckedChangedAsObservable()
                 .Subscribe(x => Debug.WriteLine($"checkBox1 CheckedChanged:{checkBox1.Checked}"));
        this.DragEnterAsObservable()
            .Subscribe(x =>
            {
                Debug.WriteLine($"MainForm DragEnter:Data {x.Data?.GetData(DataFormats.Text)}");
                x.Effect = DragDropEffects.All;
            });

        this.DragDropAsObservable()
            .Subscribe(x => Debug.WriteLine($"MainForm DragDrop:Data {x.Data?.GetData(DataFormats.Text)}"));
    }

    private void ButtonClick()
    {
        comboBox1.SelectedIndex = 2;
        Debug.WriteLine($"button1 Click!");
    }

    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {

    }

}
