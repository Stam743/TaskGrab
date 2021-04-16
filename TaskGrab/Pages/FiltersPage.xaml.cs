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
        private int numCommunities = 0;
        private int numCategories = 0;
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
            Panel.SetZIndex(ChooseCommunitiesBox, -1);
        }

        private void ReturnToDefaultValues(object sender, RoutedEventArgs e)
        {
            returnDefaultVals();
        }

        private void ShowLocationTab(object sender, RoutedEventArgs e)
        {
            returnDefaultVals();
            locationButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3c206"));
            categoryButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            priceButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
        }

        private void ShowPriceTab(object sender, RoutedEventArgs e)
        {
            returnDefaultVals();
            PricePanel.Visibility = Visibility.Visible;
            priceButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3c206"));
            categoryButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            locationButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
        }

        private void ShowCommunityFilter(object sender, RoutedEventArgs e)
        {
            returnDefaultVals();
            ChooseCommunityPanel.Visibility = Visibility.Visible;
        }

        private void HideCommunityFilter(object sender, RoutedEventArgs e)
        {
            ChooseCommunityPanel.Visibility = Visibility.Hidden;
        }

        private void ShowCategoryFilter(object sender, RoutedEventArgs e)
        {
            returnDefaultVals();
            CategoryPanel.Visibility = Visibility.Visible;
            categoryButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f3c206"));
            locationButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            priceButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
        }

        private void HideCategoryFilter(object sender, RoutedEventArgs e)
        {
            CategoryPanel.Visibility = Visibility.Hidden;
        }

        private void ShowLowerBoundHidden(object sender, RoutedEventArgs e)
        {
            PriceFromButtonHiddenContent.Visibility = Visibility.Visible;
            LowerBoundShow.Visibility = Visibility.Hidden;
        }

        private void SetPriceValue(object sender, MouseButtonEventArgs e)
        {
            FilterPriceValue.Text = "Set";
        }

        private void PriceFromButtonUnhide(object sender, MouseButtonEventArgs e)
        {
            PriceFromButtonHiddenContent.Visibility = Visibility.Hidden;
            LowerBoundShow.Visibility = Visibility.Visible;
            FilterPriceValue.Text = "?";
        }

        private void PriceToButtonUnhide(object sender, MouseButtonEventArgs e)
        {
            PriceToButtonHiddenContent.Visibility = Visibility.Hidden;
            UpperBoundShow.Visibility = Visibility.Visible;
            FilterPriceValue.Text = "?";
        }

        private void ShowUpperBoundHidden(object sender, RoutedEventArgs e)
        {
            PriceToButtonHiddenContent.Visibility = Visibility.Visible;
            UpperBoundShow.Visibility = Visibility.Hidden;
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

            ChooseCommunityPanel.Visibility = Visibility.Hidden;
            CategoryPanel.Visibility = Visibility.Hidden;
            PricePanel.Visibility = Visibility.Hidden;

            // Default visible items
            DistanceLabel.Visibility = Visibility.Visible;

            // choose community stuff
            Panel.SetZIndex(ChooseCommunitiesBox, 1);
            ChooseCommunityButton.Visibility = Visibility.Visible;
            ChooseCommunityImage.Visibility = Visibility.Visible;
            ChooseCommunityLabel.Visibility = Visibility.Visible;
        
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
            numCommunities = 0;
            FilterCommunityValue.Text = "?";

            returnDefaultVals();
        }
        private void ClearCategoryResult(object sender, RoutedEventArgs e)
        {
            numCategories = 0;
            FilterCategoryValue.Text = "?";

            returnDefaultVals();
        }
        
        private void IncrementCommunityCount(object sender, RoutedEventArgs e)
        {
            numCommunities += 1;
            FilterCommunityValue.Text = numCommunities.ToString();
        }

        private void DecrementCommunityCount(object sender, RoutedEventArgs e)
        {
            numCommunities -= 1;
            FilterCommunityValue.Text = numCommunities.ToString();
        }

        private void IncrementCategoryCount(object sender, RoutedEventArgs e)
        {
            numCategories += 1;
            FilterCategoryValue.Text = numCategories.ToString();
        }

        private void DecrementCategoryCount(object sender, RoutedEventArgs e)
        {
            numCategories -= 1;
            FilterCategoryValue.Text = numCategories.ToString();
        }
    }
}
