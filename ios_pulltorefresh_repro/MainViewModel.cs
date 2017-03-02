using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ios_pulltorefresh_repro
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private Command loadItemsCommand;

		private bool isBusy;
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				if (isBusy == value)
					return;

				isBusy = value;
				OnPropertyChanged("IsBusy");
			}
		}

		public ObservableCollection<string> Items { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public MainViewModel()
		{
			Items = new ObservableCollection<string> { "Item 1", "Item 2", "Item 3", "Item 4" };
		}

		public Command LoadItemsCommand
		{
			get
			{
				return loadItemsCommand ?? (loadItemsCommand = new Command(ExecuteLoadItemsCommand, () =>
			   {
				   return !IsBusy;
			   }));
			}
		}

		private async void ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;
			LoadItemsCommand.ChangeCanExecute();

			//DoStuff
			await Task.Run(async () =>
			{
				await Task.Delay(TimeSpan.FromSeconds(3));
				Items.Add($"Item {Items.Count + 1}");
			});

			IsBusy = false;
			LoadItemsCommand.ChangeCanExecute();
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
