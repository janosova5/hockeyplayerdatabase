using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;


namespace HockeyPlayerDatabase.MainApp
{

    public partial class MainWindow
    {
        public PlayerSearchViewModel SearchViewModel { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeSearchView();
        }

        private void InitializeSearchView()
        {
            SearchViewModel = new PlayerSearchViewModel(); 
            PlayersData.ItemsSource = SearchViewModel.DbContext.FilteredPlayers;
            PlayersData.DataContext = SearchViewModel.DbContext.FilteredPlayers;
            NumberOfFilteredPlayers.DataContext = SearchViewModel.DbContext.FilteredPlayers;
            NumberOfAllPlayers.DataContext = SearchViewModel.DbContext.Players.ToList();
            if (PlayersData.SelectedItem == null)
            {
                SetBtnsEnabledToBoolValue(false);
            }
        }

        private void MenuBtnClicked(object sender, RoutedEventArgs e)
        {
            var contextMenu = ((Button) sender).ContextMenu;
            if (contextMenu != null)
            {
                contextMenu.IsEnabled = true;
                contextMenu.PlacementTarget = (Button) sender;
                contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                contextMenu.IsOpen = true;
            }


        }

        private void SaveToXmlClicked(object sender, RoutedEventArgs e)
        {
            SearchViewModel.DbContext.SaveToXml("ClubsAndPlayers.xml");
        }

        private void CloseWindowClicked(object sender, RoutedEventArgs e)
        {
            SearchViewModel.DbContext.Dispose();
            Close();
        }

        private void YearToWasTyped(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                if (((TextBox) sender).Text != "")
                {
                    try //nesmie zadat string
                    {
                        SearchViewModel.SearchModel.YearTo = Int32.Parse(((TextBox) sender).Text);
                    }
                    catch (Exception exception) //nefunguje
                    {
                        Console.WriteLine(exception);
                        throw;
                    }

                }

            }

        }

        private void KrpWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox) sender).Text != "")
            {
                SearchViewModel.SearchModel.Krp = Int32.Parse(((TextBox) sender).Text);
            }

        }

        private void YearFromWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox) sender).Text != "")
            {
                SearchViewModel.SearchModel.YearFrom = Int32.Parse(((TextBox) sender).Text);
            }

        }

        private void FirstNameWasTyped(object sender, RoutedEventArgs e)
        {
            SearchViewModel.SearchModel.FirstName = ((TextBox) sender).Text;
        }

        private void LastNameWasTyped(object sender, RoutedEventArgs e)
        {
            SearchViewModel.SearchModel.LastName = ((TextBox) sender).Text;
        }

        private void ClubNameWasTyped(object sender, RoutedEventArgs e)
        {
            SearchViewModel.SearchModel.Club = ((TextBox) sender).Text;
        }

        private void CadetWasClicked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox) sender).IsChecked == true)
            {
                SearchViewModel.SearchModel.AgeCategory = AgeCategory.Cadet;
            }
            else
            {
                SearchViewModel.SearchModel.AgeCategory = null;
            }
        }

        private void JuniorWasClicked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox) sender).IsChecked == true)
            {
                SearchViewModel.SearchModel.AgeCategory = AgeCategory.Junior;
            }
            else
            {
                SearchViewModel.SearchModel.AgeCategory = null;
            }
        }

        private void MidgestWasClicked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox) sender).IsChecked == true)
            {
                SearchViewModel.SearchModel.AgeCategory = AgeCategory.Midgest;
            }
            else
            {
                SearchViewModel.SearchModel.AgeCategory = null;
            }
        }

        private void SeniorWasClicked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox) sender).IsChecked == true)
            {
                SearchViewModel.SearchModel.AgeCategory = AgeCategory.Senior;
            }
            else
            {
                SearchViewModel.SearchModel.AgeCategory = null;
            }

        }

        private void ApplyFilterClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            SearchViewModel.DbContext.FilterPlayers(SearchViewModel.SearchModel);
            PlayersData.ItemsSource = null;
            PlayersData.ItemsSource = SearchViewModel.DbContext.FilteredPlayers;
            NumberOfFilteredPlayers.DataContext = null;
            NumberOfFilteredPlayers.DataContext = SearchViewModel.DbContext.FilteredPlayers;
            SetBtnsEnabledToBoolValue(false);
        }

        private void OpenClubUrlClicked(object sender, RoutedEventArgs routedEventArgs)
        {
          
            if (!(PlayersData.SelectedItem is Player selectedPlayer)) return;
            foreach (var club in SearchViewModel.DbContext.Clubs)
            {
                if (club.Name == selectedPlayer.ClubName && club.Url != null)
                {
                    Process.Start(club.Url);
                }
            }
        }

        private void PlayerWasSelected(object sender, RoutedEventArgs routedEventArgs)
        {
            SetBtnsEnabledToBoolValue(true);
        }

        private void AddNewPlayerClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            AddDialogWindow dialog = new AddDialogWindow(this, sender);
            dialog.ShowDialog();
        }

        private void EditPlayerClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            AddDialogWindow dialog = new AddDialogWindow(this, sender);
            dialog.ShowDialog();
        }

        private void RemovePlayerClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            if (MessageBox.Show("Are you sure to remove this player?", "Remove?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!(PlayersData.SelectedItem is Player selectedPlayer)) return;
                SearchViewModel.DbContext.Players.Remove(selectedPlayer);
                SearchViewModel.DbContext.SaveChanges();
            }
            UpdateTable();
        }

        private void SetBtnsEnabledToBoolValue(Boolean value)
        {
            Remove.IsEnabled = value;
            Edit.IsEnabled = value;
            Url.IsEnabled = value;
        }

        public void UpdateTable()
        {
            PlayersData.ItemsSource = null;
            PlayersData.ItemsSource = SearchViewModel.DbContext.Players.ToList();
            NumberOfFilteredPlayers.DataContext = null;
            NumberOfFilteredPlayers.DataContext = SearchViewModel.DbContext.Players.ToList();
            NumberOfAllPlayers.DataContext = null;
            NumberOfAllPlayers.DataContext = SearchViewModel.DbContext.Players.ToList();
            SetBtnsEnabledToBoolValue(false);
        }

    }
}
