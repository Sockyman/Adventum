using Microsoft.Xna.Framework;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Adventum.Properties;

namespace Adventum.UI
{
	class OptionsScreen : Screen
	{
		public OptionsScreen() : base(PanelSkin.Simple)
		{
			AddHeader("Options");

			AddBoolSetting("Audio", "enableAudio");
			AddBoolSetting("Limit Particles", "limitParticles", "Decreases the lifespan of particles.");
			AddBoolSetting("Tutorial", "showTutorial", "Displays the tutorial each playthrough.");
			AddBoolSetting("Start Fullscreen", "startFullscreen");
			AddBoolSetting("Show FPS", "showFps");
			AddBoolSetting("Desaturate Colours", "desaturate", "Slightly desaturates colours.");
			AddBoolSetting("Lighting Effects", "lightingEffects", "Enables lighting effects.");

			AddButton("Reset To Default", OnResetButtonPress);

			Entity line = AddChild(new HorizontalLine(Anchor.AutoCenter));
			line.Opacity = 0;
			AddButton("Back", OnBackButtonClick);
		}

		private Button AddBoolSetting(string buttonName, string settingTitle, string description = "")
		{
			void SetText(Entity entity)
			{
				((Button)entity).ButtonParagraph = new Paragraph(buttonName + " : " + Settings.Default[settingTitle].ToString());
			}
			void UpdateSetting(Entity entity)
			{
				Settings.Default[settingTitle] = !(bool)Settings.Default[settingTitle];

				System.Console.WriteLine("Setting: " + settingTitle + ",  Set to: " + Settings.Default[settingTitle].ToString());

				Settings.Default.Save();
			}

			Button button = AddButton(buttonName, SetText);
			button.OnClick += UpdateSetting;
			SetText(button);
			button.ToggleMode = true;
			button.Checked = (bool)Settings.Default[settingTitle];

			if (description != "")
			{
				button.ToolTipText = description;
			}

			return button;
		}


		private void OnResetButtonPress(Entity entity)
		{
			Settings.Default.Reset();
			Settings.Default.Save();

			Entity par = Parent;

			RemoveFromParent();

			par.AddChild(new OptionsScreen());
		}


		private void OnBackButtonClick(Entity entity)
		{
			RemoveFromParent();
			Dispose();
		}
	}
}
