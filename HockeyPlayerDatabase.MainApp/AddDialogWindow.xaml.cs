using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HockeyPlayerDatabase.Interfaces;
using HockeyPlayerDatabase.Model;

namespace HockeyPlayerDatabase.MainApp
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class AddDialogWindow
    {
        public List<string> Categories = new List<string> { "Cadet", "Junior", "Midgest", "Senior" };
        public HockeyContext DbContext;
        public MainWindow MainView;
        public Player NewPlayer;
        public Player SelectedPlayer;

        public AddDialogWindow(MainWindow window, object sender)
        {
            InitializeComponent();
            MainView = window;
            DbContext = window.SearchViewModel.DbContext;
            ClubCombobox.ItemsSource = DbContext.Clubs.ToList();
            ClubCombobox.DataContext = DbContext.Clubs.ToList();
            CategoryCombobox.ItemsSource = Categories;
            CategoryCombobox.DataContext = Categories;

            var button = sender as Button;
            if (button != null && button.Content.ToString() == "Add...")
            {
                NewPlayer = new Player();
                OkButton.IsEnabled = false;

            } else if (button != null && button.Content.ToString() == "Edit...")
            {
                if (window.PlayersData.SelectedItem is Player player)
                {
                    var id = player.Id;
                    SelectedPlayer = DbContext.GetPlayerById(id);
                    
                }
            }
        }

        private void KrpWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                var krpId = Int32.Parse(((TextBox)sender).Text);
                if (!DbContext.CheckKrpId(krpId))
                {
                    if (NewPlayer != null)
                    {
                        NewPlayer.KrpId = krpId;
                        CheckAllFields();
                    }
                    else if (SelectedPlayer != null)
                    {
                        SelectedPlayer.KrpId = krpId;
                    }
                   
                }
                else
                {
                    MessageBox.Show("Player with this krp id already exists, please type unique value.");
                }
            }
        }

        private void YearWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                var yearOfBirth = Int32.Parse(((TextBox)sender).Text);
                if (NewPlayer != null)
                {
                    NewPlayer.YearOfBirth = yearOfBirth;
                    CheckAllFields();
                }
                else if (SelectedPlayer != null)
                {
                    SelectedPlayer.YearOfBirth = yearOfBirth;
                }
            }
        }

        private void FirstNameWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                var firstName = ((TextBox)sender).Text;
                if (NewPlayer != null)
                {
                    NewPlayer.FirstName = firstName;
                    CheckAllFields();
                }
                else if (SelectedPlayer != null)
                {
                    SelectedPlayer.FirstName = firstName;
                }
            }
        }

        private void LastNameWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                var lastName = ((TextBox)sender).Text;
                if (NewPlayer != null)
                {
                    NewPlayer.LastName = lastName;
                    CheckAllFields();
                }
                else if (SelectedPlayer != null)
                {
                    SelectedPlayer.LastName = lastName;
                }
            }
        }

        private void TitleWasTyped(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
            {
                var title = ((TextBox)sender).Text;
                if (NewPlayer != null)
                {
                    NewPlayer.TitleBefore = title;
                    CheckAllFields();
                }
                else if (SelectedPlayer != null)
                {
                    SelectedPlayer.TitleBefore = title;
                }
            }
        }

        private void CategoryWasSelected(object sender, EventArgs eventArgs)
        {
            var category = CategoryCombobox.SelectedItem.ToString();
            if (NewPlayer != null)
            {
                SetPlayerCategory(category, NewPlayer);
                CheckAllFields();
            }
            else if (SelectedPlayer != null)
            {
                SetPlayerCategory(category, SelectedPlayer);
            }
        }

        private void ClubWasSelected(object sender, EventArgs eventArgs)
        {
            if (ClubCombobox.SelectedItem is Club selectedItem)
            {
                if (NewPlayer != null)
                {
                    NewPlayer.ClubName = selectedItem.Name;
                    NewPlayer.ClubId = selectedItem.Id;
                    CheckAllFields();
                }
                else if (SelectedPlayer != null)
                {
                    SelectedPlayer.ClubName = selectedItem.Name;
                    SelectedPlayer.ClubId = selectedItem.Id;
                }
            }
        }

        private void OkButtonWasClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            if (NewPlayer != null)
            {
                DbContext.Players.Add(NewPlayer);
                DbContext.SaveChanges();
                MessageBox.Show("Player: " + NewPlayer + " was added to database.");
            }
            else if (SelectedPlayer != null)
            {
                DbContext.SaveChanges();
                MessageBox.Show("Player: " + SelectedPlayer + " was changed.");
            }
            MainView.UpdateTable();
            Close();
        }

        private void CancelButtonWasClicked(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }

        private void CheckAllFields()
        {
            if (NewPlayer.KrpId != 0 && NewPlayer.YearOfBirth != 0 &&
                NewPlayer.FirstName != "" &&
                NewPlayer.LastName != "" &&
                NewPlayer.AgeCategory.HasValue &&
                NewPlayer.ClubName != "")
            {
                OkButton.IsEnabled = true;
            }
        }

        private void SetPlayerCategory(string category, Player player)
        {
            switch (category)
            {
                case "Cadet":
                    player.AgeCategory = AgeCategory.Cadet;
                    break;
                case "Junior":
                    player.AgeCategory = AgeCategory.Junior;
                    break;
                case "Midgest":
                    player.AgeCategory = AgeCategory.Midgest;
                    break;
                case "Senior":
                    player.AgeCategory = AgeCategory.Senior;
                    break;
            }
        }

    }
}
