using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteDock
{
	public partial class NoteDock : Form
	{

		string path = @"";
		bool saved = true;
		public NoteDock()
		{
			InitializeComponent();
		}
		private void newToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 100;
			SaveFileDialog save = new SaveFileDialog();
			if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = save.FileName;
				StreamWriter write = new StreamWriter(File.Create(path));
				Note.Text = "";
				write.Close();
				FilePath.Text = path;
				saved = true;
			}
			loading.Value = 0;
		}

		private void openToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 100;
			OpenFileDialog open = new OpenFileDialog();
			if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = open.FileName;
				StreamReader reader = new StreamReader(path);
				string data = reader.ReadToEnd();
				Note.Text = data;
				reader.Close();
				FilePath.Text = path;
			}
			loading.Value = 0;
		}

		private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 100;
			SaveFileDialog save = new SaveFileDialog();
			if (path == "")
			{
				if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					path = save.FileName;
					StreamWriter writer = new StreamWriter(File.Create(path));
					writer.Write(Note.Text);
					writer.Flush();
					writer.Close();
					FilePath.Text = path;
					saved = true;
				}
			}
			else
			{
				StreamWriter writer = new StreamWriter(File.Create(path));
				writer.Write(Note.Text);
				writer.Flush();
				writer.Close();
				saved = true;
			}
			loading.Value = 0;
		}

		private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			SaveFileDialog save = new SaveFileDialog();
			if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = save.FileName;
				StreamWriter writer = new StreamWriter(File.Create(path));
				writer.Write(Note.Text);
				writer.Flush();
				writer.Close();
				FilePath.Text = path;
				saved = true;
			}
		}

		private void Note_TextChanged(object sender, EventArgs e)
		{
			saved = false;
		}

		private void NoteDock_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!saved)
			{
				string message = "Save All Changes?";
				string title = "NoteDock";
				MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show(message, title, buttons);
				if(result == DialogResult.Yes)
				{
					SaveFileDialog save = new SaveFileDialog();
					if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						path = save.FileName;
						StreamWriter writer = new StreamWriter(File.Create(path));
						writer.Write(Note.Text);
						writer.Flush();
						writer.Close();
						FilePath.Text = path;
						saved = true;
					}
				}
			}
		}
	}
}
