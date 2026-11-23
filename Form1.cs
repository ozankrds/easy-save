using System.ComponentModel;
using System.Net.Sockets;

namespace EasySave;

public partial class Form1 : Form
{
    private readonly ListBox selectedFilesListBox = new ListBox
    {
        Location = new(20, 60),
        Size = new(740, 360)
    };

    public Form1()
    {
        InitializeComponent();

        var chooseFilesButton = new Button
        {
            Text = "Choose save files",
            Location = new(20, 30),
            AutoSize = true
        };

        chooseFilesButton.Click += ChooseFilesButton_Click;

        Controls.Add(chooseFilesButton);
        Controls.Add(selectedFilesListBox);
    }

    private void ChooseFilesButton_Click (object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Select the files",
            Multiselect = true
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        selectedFilesListBox.Items.Clear();

        foreach (string path in dialog.FileNames)
        {
            selectedFilesListBox.Items.Add(path);
        }
    }
}
