using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonGo.RocketAPI.Console
{
	public partial class Stats : Form
	{
		bool list = false;
		Pokemons pokemonList = new Pokemons();
		public Stats()
		{
			InitializeComponent();
			Task.Run(update);
			if (Globals.pokeList)
			{
				Task.Run(() =>
				{
					list = true;
					pokemonList.ShowDialog();
				});
			}
		}

		async Task update()
		{
			while (true)
			{
				this.username.Text = "User: " + Vars.username;
				this.xprate.Text = "XP/Hour:" + Vars.xprate;
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
	}
}
