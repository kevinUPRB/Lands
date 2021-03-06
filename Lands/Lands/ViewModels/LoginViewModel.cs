﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Lands.ViewModels
{
	using GalaSoft.MvvmLight.Command;
	using System.ComponentModel;
	using Views;
	using System.Windows.Input;
	using Xamarin.Forms;

	public class LoginViewModel : BaseViewModel
	{
		#region Eventos
		
		#endregion

		#region Atributos

		private string email;
		private bool isEnabled;
		private bool isRunning;
		private string password;

		#endregion

		#region Propiedades

		public string Email
		{
			get { return this.email; }
			set { SetValue(ref this.email, value); }
		}

		public string Password
		{
			get { return this.password; }
			set { SetValue(ref this.password, value); }
		}

		public bool IsRunning
		{
			get { return this.isRunning; }
			set { SetValue(ref this.isRunning, value); }
		}

		public bool IsRemembered
		{
			get;
			set;
		}

		public bool IsEnabled
		{
			get { return this.isEnabled; }
			set { SetValue(ref this.isEnabled, value); }
		}

		#endregion

		#region Contrusctores

		public LoginViewModel()
		{
			this.IsRemembered = true;
			this.IsEnabled = true;

			this.Email = "k@k.com";
			this.Password = "1234";
		}

		#endregion
		#region Comandos

		public ICommand LoginCommand
		{
			get
			{
				return new RelayCommand(Login);
			}
		}



		private async void Login()
		{
			if (string.IsNullOrEmpty(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					"You must enter an email.",
					"Accept");
				return;
			}

			if (string.IsNullOrEmpty(this.Password))
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					"You must enter an Password.",
					"Accept");
				return;
			}

			this.IsRunning = true;
			this.IsEnabled = false;
			if (this.Email != "k@k.com" || this.Password != "1234")
			{
				this.IsRunning = false;
				this.IsEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					"Email or Password is incorrect.",
					"Accept");
				this.Password = string.Empty;
				return;
			}

			this.IsRunning = false;
			this.IsEnabled = true;

			this.Email = string.Empty;
			this.Password = string.Empty;

			MainViewModel.GetInstance().Lands = new LandsViewModel();

			await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

		}

		#endregion
	}
}
