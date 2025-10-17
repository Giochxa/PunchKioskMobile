// Views/Pages/EmployeesPage.xaml.cs
using Microsoft.Maui.Controls;
using PunchKioskMobile.ViewModels;

namespace PunchKioskMobile.Views.Pages
{
    public partial class EmployeesPage : ContentPage
    {
        private readonly EmployeesViewModel _vm;

        public EmployeesPage(EmployeesViewModel vm)
        {
            _vm = vm;
            BindingContext = _vm;

            BuildUI();
        }

        private void BuildUI()
        {
            // Create the CollectionView
            var collectionView = new CollectionView();
            collectionView.SetBinding(CollectionView.ItemsSourceProperty, "Employees");

            // Create the DataTemplate
            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                // Create the Grid with 2 columns
                var grid = new Grid
                {
                    Padding = new Thickness(10),
                    ColumnSpacing = 10
                };

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                // Create the Image
                var image = new Image
                {
                    WidthRequest = 48,
                    HeightRequest = 48,
                    Aspect = Aspect.AspectFill
                };
                image.SetBinding(Image.SourceProperty, "PhotoUrl");

                // Create the labels
                var nameLabel = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    VerticalOptions = LayoutOptions.Center
                };
                nameLabel.SetBinding(Label.TextProperty, "FullName");

                var positionLabel = new Label
                {
                    FontSize = 12,
                    TextColor = Colors.Gray
                };
                positionLabel.SetBinding(Label.TextProperty, "Position");

                var codeLabel = new Label
                {
                    FontSize = 12,
                    TextColor = Colors.Gray
                };
                codeLabel.SetBinding(Label.TextProperty, "EmployeeCode");

                // Create the vertical stack for text
                var textStack = new VerticalStackLayout
                {
                    Spacing = 2,
                    VerticalOptions = LayoutOptions.Center
                };
                textStack.Children.Add(nameLabel);
                textStack.Children.Add(positionLabel);
                textStack.Children.Add(codeLabel);

                // Add elements to grid
                grid.Children.Add(image);
                grid.Children.Add(textStack);
                Grid.SetColumn(image, 0);
                Grid.SetColumn(textStack, 1);

                return grid;
            });

            // Set the content
            Content = collectionView;

            // Add a refresh view for pull-to-refresh functionality
            var refreshView = new RefreshView
            {
                Content = collectionView
            };
            refreshView.SetBinding(RefreshView.IsRefreshingProperty, "IsRefreshing");
            refreshView.SetBinding(RefreshView.CommandProperty, "RefreshCommand");

            Content = refreshView;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_vm != null)
            {
                await _vm.RefreshAsync();
            }
        }
    }
}