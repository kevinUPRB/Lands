

namespace Lands.ViewModels
{
	using Services;
	using Models;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Xamarin.Forms;

	public class LandsViewModel : BaseViewModel
	{
		#region Servicios

		private ApiService apiService;

		#endregion

		#region Atributos

		private ObservableCollection<Land> lands;

		#endregion

		#region Propiedades

		public ObservableCollection<Land> Lands
		{
			get { return this.lands; }
			set { SetValue(ref this.lands, value); }
		}

		#endregion

		#region Constructores

		public LandsViewModel()
		{
			this.apiService = new ApiService();
			this.loadLands();
		}

		#endregion

		#region Methods

		private async void loadLands()
		{

			var connection = await this.apiService.CheckConnection();
			if (!connection.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(
					"Alert",
					connection.Message,
					"Accept");

				await Application.Current.MainPage.Navigation.PopAsync();
				return;
			}
			var response = await this.apiService.GetList<Land>("https://restcountries.eu", "/rest", "/v2/all");

			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(
					"Alert",
					response.Message,
					"Accept");

				await Application.Current.MainPage.Navigation.PopAsync();
				return;
			}

			var list = (List<Land>)response.Result;
			this.Lands = new ObservableCollection<Land>(list);

		}

		#endregion
	}
}
