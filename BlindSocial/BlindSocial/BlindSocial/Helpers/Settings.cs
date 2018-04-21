// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BlindSocial.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

        private const string baseApiUrlKey = "baseApiUrlKey";
        private static readonly string baseApiUrlDefault = "https://blindsocial.azurewebsites.net/";

        #endregion


        //      public static string GeneralSettings
        //{
        //	get
        //	{
        //		return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
        //	}
        //	set
        //	{
        //		AppSettings.AddOrUpdateValue(SettingsKey, value);
        //	}
        //}

        public static string BaseAPIUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(baseApiUrlKey, baseApiUrlDefault);
            }
        }

        /*
        public static string TranslateAPIUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(translateApiUrlKey, translateApiUrlDefault);
            }
        }
        */
    }
}