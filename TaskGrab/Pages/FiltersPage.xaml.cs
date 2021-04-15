using Microsoft.QueryStringDotNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskGrab.Navigation;

namespace TaskGrab.Pages
{
    /// <summary>
    /// Interaction logic for FiltersPage.xaml
    /// </summary>
    public partial class FiltersPage : Page
    {
        private MainWindow main;
        private History history;
        private QueryString query_string;
        private String distanceType = "NONE";
        private String distanceValue = "NONE";
        public FiltersPage()
        {
            InitializeComponent();
            main = (MainWindow)Application.Current.MainWindow;
            history = main.GetHistory();

            string request_url = main.GetHistory().current.OriginalString;
            int query_start = request_url.IndexOf('?');

            if (query_start < 0)
                return;

            query_string = QueryString.Parse(request_url.Substring(query_start + 1));
        }

        private void SetDistanceButton_Click(object sender, RoutedEventArgs e)
        {
            DistanceTypeTextBlock.Visibility = Visibility.Visible;
            DistanceTextBox.Visibility = Visibility.Visible;
            DistanceLabel.Visibility = Visibility.Hidden;
        }

        private void ReturnToDefaultValues(object sender, RoutedEventArgs e)
        {
            returnDefaultVals();
        }

        private void ShowCommunityFilter(object sender, RoutedEventArgs e)
        {
            ChooseCommunityButton.Visibility = Visibility.Hidden;
            ChooseCommunityImage.Visibility = Visibility.Hidden;
            ChooseCommunityLabel.Visibility = Visibility.Hidden;

            ChooseCommunitiesBox.Height = 810;
            Panel.SetZIndex(ChooseCommunitiesBox, 1);
        }

        private void returnDefaultVals()
        {
             // everything else gets hidden

            // set distance stuff
            DistanceTypeTextBlock.Visibility = Visibility.Hidden;
            DistanceBlocksTextBlock.Visibility = Visibility.Hidden;
            DistanceKmTextBlock.Visibility = Visibility.Hidden;
            DistanceMilesTextBlock.Visibility = Visibility.Hidden;
            DistanceTextBox.Visibility = Visibility.Hidden;

            // choose community stuff
            ChooseCommunityButton.Visibility = Visibility.Visible;
            ChooseCommunityImage.Visibility = Visibility.Visible;
            ChooseCommunityLabel.Visibility = Visibility.Visible;

            // Default visible items
            DistanceLabel.Visibility = Visibility.Visible;
        }

        private void DistanceTypeTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DistanceTypeTextBlock.Visibility = Visibility.Hidden;
            DistanceBlocksTextBlock.Visibility = Visibility.Visible;
            DistanceKmTextBlock.Visibility = Visibility.Visible;
            DistanceMilesTextBlock.Visibility = Visibility.Visible;
        }

        private void DistanceBlocksTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            distanceValue = DistanceTextBox.Text;
            distanceType = "Blocks";
            FilterDistanceValue.Text = distanceValue + " " + distanceType;

            DistanceLabel.Text = FilterDistanceValue.Text = distanceValue + " " + distanceType;
            returnDefaultVals();
        }

        private void DistanceMilesTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            distanceValue = DistanceTextBox.Text;
            distanceType = "Miles";
            FilterDistanceValue.Text = distanceValue + " " + distanceType;

            DistanceLabel.Text = FilterDistanceValue.Text = distanceValue + " " + distanceType;
            returnDefaultVals();
        }

        private void DistanceKmTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            distanceValue = DistanceTextBox.Text;
            distanceType = "KM";
            FilterDistanceValue.Text = distanceValue + " " + distanceType;

            DistanceLabel.Text = FilterDistanceValue.Text = distanceValue + " " + distanceType;
            returnDefaultVals();

        }

        private void ClearDistanceResult(object sender, RoutedEventArgs e)
        {
            distanceValue = "NONE";
            distanceType = "NONE";

            DistanceLabel.Text = "Set Distance";
            FilterDistanceValue.Text = "?";
        }

        private void ClearPriceResult(object sender, RoutedEventArgs e)
        {
            distanceValue = "NONE";
            distanceType = "NONE";

            DistanceLabel.Text = "Set Distance";
            FilterDistanceValue.Text = "?";
        }

        private void ClearCommunityResult(object sender, RoutedEventArgs e)
        {
            distanceValue = "NONE";
            distanceType = "NONE";

            DistanceLabel.Text = "Set Distance";
            FilterDistanceValue.Text = "?";
        }
        private void ClearCategoryResult(object sender, RoutedEventArgs e)
        {
            distanceValue = "NONE";
            distanceType = "NONE";

            DistanceLabel.Text = "Set Distance";
            FilterDistanceValue.Text = "?";
        }

    }
}
