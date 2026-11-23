using System.ComponentModel;
using System.Net.Sockets;

namespace EasySave;

public partial class Form1 : Form
{
    private readonly ListBox selectedFilesListBox = new ListBox
    {
        Location = new(20, 60),
        Size = new(740, 180)
    };

    private readonly Label destinationLabel = new()
    {
        Text = "",
        Location = new(20, 300),
        AutoSize = true
    };

    public string[] selectedFilePaths { get; private set; } = [];

    public string? destinationDirectory { get; private set; }


    public Form1()
    {
        InitializeComponent();

        var chooseFilesButton = new Button
        {
            Text = "Choose save files",
            Location = new(20, 30),
            AutoSize = true
        };

        var chooseDirectoryButton = new Button
        {
            Text = "Choose the directory",
            Location = new(20, 270),
            AutoSize = true
        };

        var backupButton = new Button
        {
            Text = "Backup",
            Location = new(20, 320),
            AutoSize = true
        };

        chooseFilesButton.Click += ChooseFilesButton_Click;
        chooseDirectoryButton.Click += ChooseDirectoryButton_Click;
        backupButton.Click += BackupButton_Click;

        Controls.Add(chooseFilesButton);
        Controls.Add(selectedFilesListBox);
        Controls.Add(chooseDirectoryButton);
        Controls.Add(destinationLabel);
        Controls.Add(backupButton);
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

        selectedFilePaths = dialog.FileNames;
        selectedFilesListBox.Items.Clear();

        foreach (string path in dialog.FileNames)
        {
            selectedFilesListBox.Items.Add(path);
        }
    }

    private void ChooseDirectoryButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog();

        if  (dialog.ShowDialog() == DialogResult.OK)
        {
            destinationDirectory = dialog.SelectedPath;
            destinationLabel.Text = destinationDirectory;
        }
    }

    private void BackupButton_Click(object? sender, EventArgs e)
    {
        if (selectedFilePaths.Length == 0 || destinationDirectory is null)
        {
            MessageBox.Show("Choose file(s) and destination first");
            return;
        }

        try
        {
            var service = new BackupService();
            string folder = service.CreateBackup(selectedFilePaths, destinationDirectory);
            MessageBox.Show($"Backup created: {folder}");
        } 
        catch (Exception ex)
        {
            MessageBox.Show($"Backup failed: {ex.Message}");
        }
    }
}
