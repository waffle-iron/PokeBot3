using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PokemonGo.RocketAPI.Console
{

	public partial class Stats : Form
	{
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		const int SW_HIDE = 0;
		const int SW_SHOW = 5;
		bool console = true;

		bool list = false;
		Pokemons pokemonList = new Pokemons();
		public Stats()
		{
			InitializeComponent();
			Task.Run(update);
			if (Globals.pokeList)
			{
				list = true;
				Task.Run(() =>
				{
					pokemonList.ShowDialog();
				});
			}
			else
				list = false;
		}

		async Task update()
		{
			while (true)
			{
				await Task.Delay(10000);
				this.username.Text = "User: " + Vars.username;
				this.xprate.Text = "XP/Hour: " + Vars.xprate;
				this.catchrate.Text = "Pokemon/Hour: " + Vars.catchrate;
				this.evolvecount.Text = "Pokemon to Evolve: " + Vars.evolvecount;
				this.pokemon.Text = "Pokemon: " + Vars.pokemon;
				this.items.Text = "Items: " + Vars.items;
				this.stardust.Text = "Stardust: " + Vars.stardust;
				this.progress.Text = Vars.level;
				this.levelbar.Value = int.Parse(Vars.percentage);
			}
		}

		private void togglelist_Click(object sender, EventArgs e)
		{
			if (list)
			{
				Task.Run(() =>
				{
					list = false;
					pokemonList.Hide();
				});
			}
			else
			{
				Task.Run(() =>
				{
					list = true;
					pokemonList.ShowDialog();
				});
			}
		}

		public void toggleconsole_Click(object sender, EventArgs e)
		{
			if (console)
			{
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_HIDE);
				console = false;
			}
			else
			{
				var handle = GetConsoleWindow();
				ShowWindow(handle, SW_SHOW);
				console = true;
			}
		}

		private void Stats_FormClosed(object sender, FormClosedEventArgs e)
		{
			System.Environment.Exit(0);
		}
	}
}
